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


using PoI.Data;

namespace PoI.Requests
{
	public class RadialSearchRequest : GetRequestAbstract
	{
		public RadialSearchRequest(string url, float radius, Location location, int maxResults) : this(url, radius, location)
		{
			this.Parameters.Add("max_results", maxResults.ToString());
		}
		public RadialSearchRequest(string url, float radius, Location location, string category) : this(url, radius, location)
		{
			this.Parameters.Add("category", category);
		}
		public RadialSearchRequest(string url, float radius, Location location) : base(url, "radial_search")
		{
			this.Parameters.Add("component", GetRequestAbstract.RequestComponents);
			this.Parameters.Add("lat", location.Latitude.ToString(FormatFloat));
			this.Parameters.Add("lon", location.Longitude.ToString(FormatFloat));
			this.Parameters.Add("radius", radius.ToString());
		}
	}
}