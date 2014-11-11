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
	/// <summary>
	/// Parses the results created from the PoIInterface.Add method.
	/// The results are returned in this format:
	/// {"created_poi":{"uuid":"3c7d08d6-2882-4be5-b4cd-ad9d97440390","timestamp":1415653042}}
	/// </summary>
	public class PoiCreatedResult : MarshallableDataAbstract
	{
		private string _Id;
		public string Id { get 
			{
				return _Id; }
		}

		private bool _Success;
		public bool Success { get { return _Success; } }

		private long _TimeStamp;
		public long TimeStamp { get { return _TimeStamp; } }

		#region implemented abstract members of MarshallableDataAbstract

		public PoiCreatedResult(object data) : base(data) {}

		public override object ToDictionary ()
		{
			throw new NotImplementedException ();
		}
		public override void GetData (object data)
		{
			var dic = data as Dictionary<string, object>;

			if (dic != null && dic.ContainsKey("created_poi"))
			{
				this._Success = true;

				var idDic = dic["created_poi"] as Dictionary<string, object>;

				this._Id = (string) idDic["uuid"];

				this._TimeStamp = (long) idDic["timestamp"];
			}
		}
		#endregion
	}
}