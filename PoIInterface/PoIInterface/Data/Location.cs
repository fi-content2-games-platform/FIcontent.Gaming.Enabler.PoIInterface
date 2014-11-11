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
	public class Location : MarshallableDataAbstract
	{
		public double Latitude { get; set; }

		public double Longitude  { get; set; }

		public double Height  { get; set; }

        #region Ctors

		public Location (object data) : base(data)
		{
		}
            
		public Location (double lat, double lon) : this(lat, lon, 0f)
		{
		}

		public Location (double lat, double lon, double height)
		{
			Latitude = lat;
			Longitude = lon;
			Height = height;
		}

        #endregion

		#region implemented abstract members of MarshallableDataAbstract

		public override object ToDictionary ()
		{
			Dictionary<string, object> retDic = new Dictionary<string, object> ();
			
			Dictionary<string, double> wgs84 = new Dictionary<string, double> ();
			wgs84.Add ("latitude", this.Latitude);
			wgs84.Add ("longitude", this.Longitude);
			
			retDic.Add ("wgs84", wgs84);
			
			return retDic;
		}

		public override void GetData (object data)
		{
			var wgs84 = ((Dictionary<string, object>)data) ["wgs84"] as Dictionary<string, object>;
			
			this.Latitude = (double)wgs84 ["latitude"];
			this.Longitude = (double)wgs84 ["longitude"];
		}

		#endregion

        #region Overrides

		public override bool Equals (object obj)
		{
			 
			if (obj is Location) {
				Location other = (Location)obj;

				return 	other.Latitude.Equals (this.Latitude) &&
					other.Longitude.Equals (this.Longitude) &&
					other.Height.Equals (this.Height);
			} else
				return false;
		}

		public override string ToString ()
		{
			return string.Format ("[Location: Latitude={0}, Longitude={1}, Height={2:0.00}]", Latitude, Longitude, Height);
		}

		public override int GetHashCode ()
		{
			return this.ToString ().GetHashCode ();
		}

        #endregion

		#region Static Methods
		
		
		/// <summary>
		/// Gets the distance in Kilometers between two Locations
		/// </summary>
		/// <param name="p1">Location 1</param>
		/// <param name="p2">Location 2</param>
		public static double Distance (Location p1, Location p2)
		{
			double R = 6371.0; // earth mean radius
			double t1 = Degree2Radian (p1.Latitude);
			double t2 = Degree2Radian (p2.Latitude);
			double dt = Degree2Radian (p2.Latitude - p1.Latitude);
			double ds = Degree2Radian (p2.Longitude - p1.Longitude);
			double a = Math.Sin (dt / 2.0) * Math.Sin (dt / 2.0) + 
				Math.Cos (t1) * Math.Cos (t2) *
				Math.Sin (ds / 2.0) * Math.Sin (ds / 2.0);
			double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

			return R * c;
		}

		private static double Degree2Radian (double d)
		{
			return (d * Math.PI / 180.0);
		}

		private static double Radian2Degree (double r)
		{
			return (r / Math.PI * 180.0);
		}

		
		#endregion
	}
}
