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
	public class FwTime : MarshallableDataAbstract
	{
		public const string TypeOpen = "open";
		public const string TypeShowTimes = "show_times";

		public static FwTime Open = new FwTime() { Type = TypeOpen, LastUpdate = LastUpdate.Now };

		#region Properties
		public string Type {
			get;
			set;
		}

		public List<object> Schedule {
			get;
			set;
		}

		public LastUpdate LastUpdate {
			get;
			set;
		}

		#endregion 

		public FwTime ()
		{
			this.LastUpdate = new LastUpdate();
			this.Schedule = new List<object>();
		}

		#region MarshallableDataAbstract

		public FwTime (object data) : base(data)
		{
		}

		public override object ToDictionary ()
		{
			Dictionary<string, object> returnFwCore = new Dictionary<string, object> ();
			
			returnFwCore.Add ("type", this.Type);
			
			returnFwCore.Add ("schedule", this.Schedule);
			
			if (this.LastUpdate != null) {
				var lastupdate = this.LastUpdate.ToDictionary ();
				returnFwCore.Add ("last_update", lastupdate);
			}

			return returnFwCore;
		}
		
		public override void GetData (object data)
		{
			var fwcoreDic = data as Dictionary<string, object>;
			
			this.Type = (string)fwcoreDic ["type"];

			this.Schedule = fwcoreDic["schedule"] as List<object>;
		
			if (fwcoreDic.ContainsKey ("last_update"))
				this.LastUpdate = new LastUpdate ((Dictionary<string, object>)fwcoreDic ["last_update"]);
		}
		
		#endregion

        #region Overrides

	public override string ToString ()
		{
			return string.Format ("[FwTime: Type={0}, Schedule={1}, LastUpdate={2}]", Type, Schedule.Count, LastUpdate);
		}

		public override int GetHashCode ()
		{
			return this.ToString ().GetHashCode ();
		}

		public override bool Equals (object obj)
		{
			if (obj is FwTime) {
				FwTime other = (FwTime)obj;
				
				return 	other.Type.Equals (this.Type) &&
					other.LastUpdate.Equals (this.LastUpdate);
			} else
				return false;
		}

        #endregion
	}


}


