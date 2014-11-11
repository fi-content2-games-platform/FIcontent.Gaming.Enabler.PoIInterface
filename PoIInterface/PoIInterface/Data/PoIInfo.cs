/* Copyright (c) 2013 ETH Zurich
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using PoI.Serialization;
using System.Collections.Generic;

namespace PoI.Data
{
	public class PoIInfo : MarshallableDataAbstract
	{
		#region Properties
		public FwCore FwCore {
			get;
			set ;
		}

		public FwTime FwTime { 
			get;
			set;
		}

		public string Id {
			get;
			set ;
		}
		#endregion

		public PoIInfo ()
		{
		}

		public PoIInfo (string id) : this()
		{
			this.Id = id;
		}

		#region implemented abstract members of MarshallableDataAbstract

		public PoIInfo (object data) : base(data)
		{
		}

		public override object ToDictionary ()
		{
			Dictionary<string, object> retDic = new Dictionary<string, object> ();

			var fwCore = this.FwCore.ToDictionary ();
			retDic.Add ("fw_core", fwCore);

			if (this.FwTime != null) {
				var fwTime = this.FwTime.ToDictionary ();
				retDic.Add ("fw_time", fwTime);
			}

			return retDic;
		}

		/// <summary>
		/// PoIInterface.Add uses a different format than PoIInterface.Update.
		/// The Id of the PoI must be included in the update method.
		/// </summary>
		/// <returns>The dictionary.</returns>
		/// <param name="update">If set to <c>true</c> update.</param>
		public object ToDictionary (bool update)
		{
			var poiDic = ToDictionary ();

			if (!update)
				return poiDic;

			var retDic = new Dictionary<string, object> ();
			retDic.Add (this.Id, poiDic);

			return retDic;
		}

		public override void GetData (object data)
		{
			var k = (KeyValuePair<string, object>)data;
			this.Id = k.Key;
			var poi = k.Value as Dictionary<string, object>;

			
			var fwcore = poi ["fw_core"] as Dictionary<string, object>;
			this.FwCore = new FwCore (fwcore);

			if (poi.ContainsKey ("fw_time")) {
				var fwtime = poi ["fw_time"] as Dictionary<string, object>;
				this.FwTime = new FwTime (fwtime);
			}
		}

		#endregion

        #region Overrides

		public override string ToString ()
		{
			return this.Id;
		}

		public override bool Equals (object obj)
		{
			if (obj is PoIInfo) {
				PoIInfo other = (PoIInfo)obj;
				
				return 	other.Id.Equals (this.Id);
			} else
				return false;
		}

        #endregion

		#region Static Methods


		/// <summary>
		/// Gets the distance in Kilometers between two PoI
		/// </summary>
		/// <param name="p1">PoI 1</param>
		/// <param name="p2">PoI 2</param>
		public static double Distance(PoIInfo p1, PoIInfo p2)
		{
			return Location.Distance (p1.FwCore.Location, p2.FwCore.Location);
		}

		#endregion
	}


}


