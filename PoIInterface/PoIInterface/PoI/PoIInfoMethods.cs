using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoIInterface.PoI
{
    public  partial struct PoIInfo
    {
        public static PoIInfo GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public static List<PoIInfo> RadialSearch(PoIInfo poi, float radius)
        {
            return RadialSearch(poi.Location.Latitude, poi.Location.Longitude, radius);
        }
        public static List<PoIInfo> RadialSearch(float lon, float lat, float radius)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>        
        /// <returns></returns>
        public bool Add()
        {            
            throw new NotImplementedException();
        }

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
    }
}
