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
using System.Collections.Generic;
using PoI.Data;
using PoI.Requests;
using PoI.Serialization;
using PoI.Http;

namespace PoI
{
	/// <summary>
	/// PoI interface to the POI Data Provider GE.
	/// Implements the REST API methods to Add, Delete, Update, GetById and search for PoI in the Database
	/// </summary>
	public class PoIInterface
	{
		#region Private Members

		IHttpRequest httpRequest = new HttpRequestNet();
		private string _poiUrl;

        #endregion


		public PoIInterface(string url)
		{
			this._poiUrl = url;
		}

		#region Public Methods

		/// <summary>
		/// Adds a PoI
		/// </summary>        
		/// <param name="p">PoIInfo to add</param>
		/// <returns>the PoI completed with the created UUID</returns>
		public PoIInfo Add (PoIInfo p)
		{            
			string request = new AddPoIRequest(_poiUrl);

			string json = PoISerializationHelper.SerializePOI(p);

			string result = httpRequest.PostRequest(json, request);
			Console.WriteLine(result);

			if (string.IsNullOrEmpty(result))
				return null;

			var createdResult = new PoiCreatedResult(MiniJSON.Json.Deserialize(result));
			if (createdResult.Success)
				p.Id = createdResult.Id;

			return p;
		}

		/// <summary>
		/// Updates a PoI
		/// </summary>
		/// <param name="p">PoIInfo to update</param>
		/// <returns>true on success</returns>
		public bool Update (PoIInfo p)
		{
			string request = new UpdatePoIRequest(_poiUrl);
			
			string json = MiniJSON.Json.Serialize(p.ToDictionary(true));

			string result = httpRequest.PostRequest(json, request);
			Console.WriteLine(result);

			return !string.IsNullOrEmpty(result) && result.Equals("POI data updated succesfully!");
		}

		/// <summary>
		/// Removes a PoI
		/// </summary>        
		/// <param name="p">PoIInfo to remove</param>
		/// <returns>true on success</returns>
		public bool Delete (PoIInfo p)
		{
			string request = new DeletePoIRequest(_poiUrl, p.Id);

			string result = httpRequest.DeleteRequest(request);
			Console.WriteLine(result);

			return !string.IsNullOrEmpty(result) && result.Equals("POI deleted succesfully");
		}

      

		/// <summary>
		/// Retrieves a PoI by its Id
		/// </summary>
		/// <returns>the PoI</returns>
		/// <param name="id">PoI Identifier</param>
		/// <param name="getForUpdate">true if the poi is to be prepared for update</param> 
		public List<PoIInfo> GetByID (string id, bool getForUpdate)
		{
			string request = new GetPoIRequest(_poiUrl, id, getForUpdate);
			return GetPoIList(request);
		}

		/// <summary>
		/// Bounding box search
		/// </summary>
		/// <returns>The list of pois found</returns>
		/// <param name="northWest">North west corner</param>
		/// <param name="southEast">South east corner</param>
		/// <param name="maxResults">Max results</param>
		public List<PoIInfo> BBoxSearch(Location northWest, Location southEast, int maxResults)
		{
			string request = new BBoxSearchRequest(_poiUrl, northWest, southEast, maxResults);
			return GetPoIList(request);
		}

		/// <summary>
		/// Radial search of PoIs around a location coordinates
		/// </summary>
		/// <returns>The search results as a list of PoI</returns>
		/// <param name="l">Location (lat, lon)</param>
		/// <param name="radius">Radius of the search</param>
		public List<PoIInfo> RadialSearch (Location l, float radius)
		{
			string request = new RadialSearchRequest(_poiUrl, radius, l);
			return GetPoIList(request);
		}
		/// <summary>
		/// Radial search of PoIs around a location coordinates
		/// </summary>
		/// <returns>The search results as a list of PoI</returns>
		/// <param name="l">Location (lat, lon)</param>
		/// <param name="radius">Radius of the search</param>
		/// <param name="maxResults">Max results</param>
		public List<PoIInfo> RadialSearch (Location l, float radius, int maxResults)
		{
			string request = new RadialSearchRequest(_poiUrl, radius, l, maxResults);
			return GetPoIList(request);
		}
		/// <summary>
		/// Radial search of PoIs around a location coordinates
		/// </summary>
		/// <returns>The search results as a list of PoI</returns>
		/// <param name="l">Location (lat, lon)</param>
		/// <param name="radius">Radius of the search</param>
		/// <param name="category">category filter</param>
		public List<PoIInfo> RadialSearch (Location l, float radius, string category)
		{
			string request = new RadialSearchRequest(_poiUrl, radius, l, category);
			return GetPoIList(request);
		}



        #endregion

		#region Private Methods

		private List<PoIInfo> GetPoIList(string request)
		{
			string results = httpRequest.GetRequest(request);
			
			return PoISerializationHelper.DeserializePoIList(results);
		}

		#endregion

		#region Static methods

		public static List<PoIInfo> GetFromString (string poiStr)
		{
			return PoISerializationHelper.DeserializePoIList(poiStr);
		}

		#endregion
	}
}
