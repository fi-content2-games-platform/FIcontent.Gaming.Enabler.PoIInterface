using System.Collections.Generic;

namespace PoI.Data
{
	public class PoIInfoList : List<PoIInfo>
	{
		/// <summary>
		/// Sorts the by distance from a specific location.
		/// </summary>
		/// <param name="location">Location</param>
		public void SortByDistanceFromLocation (Location location)
		{
			this.Sort (delegate(PoIInfo p1, PoIInfo p2) {
				return Location.Distance (p1.FwCore.Location, location).CompareTo (Location.Distance (p2.FwCore.Location, location));
			}
			);
		}
	}
}