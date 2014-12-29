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
using System.Collections;
using PoI.Data;

namespace PoI.Serialization
{
	/// <summary>
	/// Helper methods for serializing PoIs
	/// </summary>
	public class PoISerializationHelper
	{
		#region Deserialization


		/// <summary>
		/// Deserializes the PoI list.
		/// </summary>
		/// <returns>The PoI list.</returns>
		/// <param name="json">Json string</param>
		public static PoIInfoList DeserializePoIList (string json)
		{
			var poiResults = MiniJSON.Json.Deserialize (json) as Dictionary<string, object>;
			var pois = poiResults ["pois"] as Dictionary<string, object>;
			var retList = new PoIInfoList ();

			if (pois != null && pois.Count > 0) {
				foreach (var poi in pois) {
					PoIInfo pInfo = new PoIInfo (poi);
					retList.Add (pInfo);
				}
			}

			return retList;
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Serializes the PoI.
		/// </summary>
		/// <returns>The POI in json string</returns>
		/// <param name="pInfo">the PoI</param>
		public static string SerializePOI (PoIInfo pInfo)
		{
			var poiDic = pInfo.ToDictionary ();
			string json = MiniJSON.Json.Serialize (poiDic);
			return json;
		}

		#endregion
	}
}
