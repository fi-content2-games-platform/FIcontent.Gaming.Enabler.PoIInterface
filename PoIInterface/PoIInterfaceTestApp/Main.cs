using System;
using PoI;
using PoI.Data;

namespace PoIInterfaceTestApp
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			PoIInterface poi = new PoIInterface("http://dev.cie.fi/FI-WARE/poi_dp");
			var list = poi.RadialSearch(new Location(52,8), 20000, 2);

			Console.WriteLine(list[0].FwCore);
			Console.WriteLine();
			Console.WriteLine(list[1].FwCore);
			Console.WriteLine();
			Console.WriteLine("Distance between two points is: " + PoIInfo.Distance(list[0],list[1]) + " Km");

			Console.ReadKey();
		}
	}
}
