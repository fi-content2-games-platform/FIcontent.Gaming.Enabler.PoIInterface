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


using System.Collections.Generic;
using System.Text;

namespace PoI.Requests
{
	public abstract class GetRequestAbstract : IRequest
	{
		protected const string FormatFloat = "0.000000000000000";
		protected const string RequestComponents = "fw_core,fw_xml3d";
		protected Dictionary<string, string> Parameters = new Dictionary<string, string> ();

		#region IRequest implementation

		public string Url {
			get;
			set;
		}

		public string Command {
			get ;
			set;
		}

		#endregion

		public GetRequestAbstract (string url) : this(url, string.Empty)
		{

		}

		public GetRequestAbstract (string url, string cmd)
		{
			this.Url = url;
			this.Command = cmd;
		}

		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder ();

			bool found = false;
			foreach (var p in Parameters) {
				if (!string.IsNullOrEmpty (p.Key) && !string.IsNullOrEmpty (p.Value)) {
					sb.AppendFormat ("{0}={1}&", p.Key, p.Value);
					found = true; 
				}

			}

			if (found)
			{
				sb.Insert(0, "?");
				sb.Remove (sb.Length - 1, 1);
			}

			sb.Insert(0, string.Format("{0}/{1}", Url, Command));

			return sb.ToString ();
		}

		public static implicit operator string (GetRequestAbstract r)
		{
			return r.ToString ();
		}
	}
}