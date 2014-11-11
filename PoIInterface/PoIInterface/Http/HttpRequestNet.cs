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
using System.IO;
using System.Net;

namespace PoI.Http
{
	public class HttpRequestNet : IHttpRequest
	{
		private const string ContentTypeApplicationJSON = "application/json";
		private const string MethodPOST = "POST";
		private const string MethodDELETE = "DELETE";
		private const string strHttpErrorFormat = "Server error (HTTP {0}: {1}).";

		public string GetRequest (string url)
		{
			HttpWebRequest request = WebRequest.Create (url) as HttpWebRequest;
		
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse) {
				if (response.StatusCode != HttpStatusCode.OK)
					throw new System.Exception (string.Format (
                        strHttpErrorFormat,
                        response.StatusCode,
                        response.StatusDescription));
				else {
					using (var sr = new StreamReader(response.GetResponseStream())) {
						string result = sr.ReadToEnd ();
						return result;
					}
				}
			}
		}

		public string PostRequest (string jsonRequest, string url)
		{
			return Request (jsonRequest, url, MethodPOST);
		}

		private string Request (string jsonRequest, string url, string method)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create (url);
			httpWebRequest.ContentType = ContentTypeApplicationJSON;
			httpWebRequest.Method = method;

			string result = null;
			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
				streamWriter.Write (jsonRequest);
				streamWriter.Flush ();
				streamWriter.Close ();

				try {
					var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse ();

					using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
						result = streamReader.ReadToEnd ();
						return result;
					}
				} catch (WebException ex) {
					if (ex.Response != null) {
						if (ex.Response.ContentLength != 0) {
							using (var stream = ex.Response.GetResponseStream()) {
								using (var reader = new StreamReader(stream)) {
									result = reader.ReadToEnd ();
									throw new WebException(result);
								}
							}
						}
					}    
				} //catch
			}

			return result;
		}

		public string DeleteRequest (string url)
		{
			return Request (string.Empty, url, MethodDELETE);
		}

	}
}

