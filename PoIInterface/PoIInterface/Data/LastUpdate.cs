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
	public class LastUpdate : MarshallableDataAbstract
	{
		public long TimeStamp { get; set; }

		public string Responsible { get; set; }

        #region Ctors

		public LastUpdate ()
		{

		}

		public LastUpdate (string responsible, long timestamp)
		{
			Responsible = responsible;
			TimeStamp = timestamp;
		}

		public LastUpdate (string responsible, DateTime timestamp)
		{
			TimeStamp = (long)(timestamp.Subtract (new DateTime (1970, 1, 1))).TotalSeconds;
			Responsible = responsible;
		}

        #endregion

		#region implemented abstract members of MarshallableDataAbstract

		public LastUpdate (object data) : base(data)
		{
		}

		public override object ToDictionary ()
		{
			Dictionary<string, object> retDic = new Dictionary<string, object> ();
			
			retDic.Add ("timestamp", this.TimeStamp);
			if (!string.IsNullOrEmpty (this.Responsible))
				retDic.Add ("responsible", this.Responsible);
			
			return retDic;
		}

		public override void GetData (object data)
		{
			var lu = data as Dictionary<string, object>;
			
			this.TimeStamp = (long)lu ["timestamp"];
			if (lu.ContainsKey ("responsible"))
				this.Responsible = (string)lu ["responsible"];
		}

		#endregion

        #region Overrides

		public override bool Equals (object obj)
		{
			 
			if (obj is LastUpdate) {
				LastUpdate other = (LastUpdate)obj;

				return 	other.Responsible.Equals (this.Responsible) &&
					other.TimeStamp.Equals (this.TimeStamp);
			} else
				return false;
		}

		public override string ToString ()
		{
			return string.Format ("[LastUpdate: TimeStamp={0}, Responsible={1}]", TimeStamp, Responsible);
		}

		public override int GetHashCode ()
		{
			return this.ToString ().GetHashCode ();
		}

        #endregion

		public static LastUpdate Now
		{ get {
				return new LastUpdate () { 
					TimeStamp = (long)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds,
					Responsible = "x"
				};
			} }
	}
}
