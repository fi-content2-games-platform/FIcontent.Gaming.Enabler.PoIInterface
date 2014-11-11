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
	public class FwCore : MarshallableDataAbstract
	{
		#region Properties
		public Location Location {
			get;
			set;
		}
		
		public Source Source {
			get;
			set;
		}

		public string Name {
			get;
			set;
		}

		public string ShortName {
			get;
			set;
		}

		public string Description
		{ get; set; }

		public string Category {
			get;
			set;
		}

		public LastUpdate LastUpdate {
			get;
			set;
		}

		#endregion 

		public FwCore ()
		{
		}

		#region MarshallableDataAbstract

		public FwCore (object data) : base(data)
		{
		}

		public override object ToDictionary ()
		{
			Dictionary<string, object> returnFwCore = new Dictionary<string, object> ();

			var locationDic = this.Location.ToDictionary ();
			returnFwCore.Add ("location", locationDic);

			if (this.Source != null) {
				var sourceDic = this.Source.ToDictionary ();
				returnFwCore.Add ("source", sourceDic);
			}

			var nameDic = new Dictionary<string, object> ();
			nameDic.Add (string.Empty, this.Name);
			returnFwCore.Add ("name", nameDic);
			
			returnFwCore.Add ("category", this.Category);

			if (!string.IsNullOrEmpty (this.Description)) {
				var descDic = new Dictionary<string, object> ();
				descDic.Add (string.Empty, this.Description);
				returnFwCore.Add ("description", descDic);
			}

			if (this.LastUpdate != null) {
				var lastupdate = this.LastUpdate.ToDictionary ();
				returnFwCore.Add ("last_update", lastupdate);
			}

			return returnFwCore;
		}
		
		public override void GetData (object data)
		{
			var fwcoreDic = data as Dictionary<string, object>;
			
			var location = fwcoreDic ["location"] as Dictionary<string, object>;
			this.Location = new Location (location);  
			
			this.Category = (string)fwcoreDic ["category"];
			
			this.Name = ((Dictionary<string, object>)fwcoreDic ["name"]) [string.Empty] as string;
			
			if (fwcoreDic.ContainsKey ("description"))
				this.Description = ((Dictionary<string, object>)fwcoreDic ["description"]) [string.Empty] as string;

			if (fwcoreDic.ContainsKey ("source")) {
				var source = fwcoreDic ["source"] as Dictionary<string, object>;
				this.Source = new Source (source);
			}

			if (fwcoreDic.ContainsKey ("last_update")) {
				this.LastUpdate = new LastUpdate (fwcoreDic ["last_update"]);
			}
		}
		
		#endregion

        #region Overrides

		public override string ToString ()
		{
			return string.Format ("[FwCore: Location={0}, Source={1}, Name={2}, ShortName={3}, Description={4}, Category={5}, LastUpdate={6}]", Location, Source, Name, ShortName, Description, Category, LastUpdate);
		}

		public override int GetHashCode ()
		{
			return this.ToString ().GetHashCode ();
		}

		public override bool Equals (object obj)
		{
			if (obj is FwCore) {
				FwCore other = (FwCore)obj;
				
				return 	other.Location.Equals (this.Location) &&
					other.Name.Equals (this.Name) &&
					other.Category.Equals (this.Category) &&
					other.Source.Equals (this.Source);
			} else
				return false;
		}

        #endregion
	}


}


