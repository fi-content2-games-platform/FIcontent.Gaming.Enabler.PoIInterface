using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoIInterface.PoI
{
    public partial struct Location
    {
        private float _latitude;
        public float Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        private float _longitude;
        public float Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private float _height;
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        #region Ctors
            
        public Location(float lat, float lon)
        {
            _latitude = lat;
            _longitude = lon;
            _height = 0f;
        }
        public Location(float lat, float lon, float height)
        {
            _latitude = lat;
            _longitude = lon;
            _height = height;
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Format("{0:0.0000000}:{1:0.0000000} ({2:0.00})", this._latitude, this._longitude, this._height);
        }

        #endregion
    }
}
