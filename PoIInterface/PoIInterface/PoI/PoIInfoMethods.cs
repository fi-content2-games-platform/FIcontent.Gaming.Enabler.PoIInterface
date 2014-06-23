using System;
using System.Collections.Generic;

namespace PoIInterface.PoI
{
    public partial struct PoIInfo
    {
        #region Private Members

        private static IHttpRequest _httpRequest = new HttpRequestUnity();

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a PoI
        /// </summary>        
        /// <returns>true on success</returns>
        public bool Add()
        {            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a PoI
        /// </summary>
        public bool Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a PoI
        /// </summary>        
        /// <returns>true on success</returns>
        public bool Remove()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Retrieves a PoI by its Id
        /// </summary>
        /// <returns>the PoI</returns>
        /// <param name="id">PoI Identifier</param>
        public static PoIInfo GetByID(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Radial search of PoIs around a location coordinates
        /// </summary>
        /// <returns>The search results as a list of PoI</returns>
        /// <param name="l">Location (lat, lon)</param>
        /// <param name="radius">Radius of the search</param>
        public static List<PoIInfo> RadialSearch(Location l, float radius)
        {
            return RadialSearch(l.Latitude, l.Longitude, radius);
        }

        /// <summary>
        /// Radial search of PoIs around a location coordinates
        /// </summary>
        /// <returns>The search results as a list of PoI</returns>
        /// <param name="lon">Longitude</param>
        /// <param name="lat">Latitude</param>
        /// <param name="radius">Radius of the search</param>
        public static List<PoIInfo> RadialSearch(float lon, float lat, float radius)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
