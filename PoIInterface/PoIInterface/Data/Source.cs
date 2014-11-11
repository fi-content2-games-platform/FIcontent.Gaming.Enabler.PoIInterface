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
	public class Source : MarshallableDataAbstract
	{
		#region Proprieties
		public string Name
		{ get; set; }
		
		public string WebSite
		{ get; set; }
		
		public string Licence
		{ get; set; }

		#endregion

		public Source ()
		{
		}

		public Source (string name, string website, string licence) : this()
		{
			this.Name = name;
			this.Licence = licence;
			this.WebSite = website;
		}
	


		#region implemented abstract members of MarshallableDataAbstract

		public Source (object data) : base(data)
		{
		}

		public override object ToDictionary ()
		{
			Dictionary<string, object> returnSource = new Dictionary<string, object> ();
			
			returnSource.Add ("name", this.Name);
			returnSource.Add ("website", this.WebSite);
			returnSource.Add ("license", this.Licence);
			
			return returnSource;
		}

		public override void GetData (object data)
		{
			Dictionary<string, object> sourceDic = data as Dictionary<string, object>;
			this.Name = (string)sourceDic ["name"];
			this.WebSite = (string)sourceDic ["website"];
			if (sourceDic.ContainsKey ("licence"))
				this.Licence = (string)sourceDic ["licence"];
			else if (sourceDic.ContainsKey ("license"))
				this.Licence = (string)sourceDic ["license"];
		}

		#endregion

		#region Overrides

		public override bool Equals (object obj)
		{
			if (obj is Source) {
				Source other = (Source)obj;
					
				return other.Name.Equals (this.Name) &&
					other.WebSite.Equals (this.WebSite) &&
					other.Licence.Equals (this.Licence);
			} else
				return false;
		}

		public override string ToString ()
		{
			return string.Format ("[Source: Name={0}, WebSite={1}, Licence={2}]", Name, WebSite, Licence);
		}

		public override int GetHashCode ()
		{
			return this.ToString ().GetHashCode ();
		}

		#endregion

	}
}