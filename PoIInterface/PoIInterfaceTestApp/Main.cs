using System;
using PoI;
using PoI.Data;
using System.Collections.Generic;

namespace PoIInterfaceTestApp
{
	class MainClass
	{
		static void Example ()
		{
			PoIInterface poi = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");
			var list = poi.RadialSearch (new Location (52, 8), 20000, 2);
			Console.WriteLine (list [0].FwCore);
			Console.WriteLine ();
			Console.WriteLine (list [1].FwCore);
			Console.WriteLine ();
			Console.WriteLine ("Distance between two points is: " + PoIInfo.Distance (list [0], list [1]) + " Km");
		}

		static void ImportFromOverPass ()
		{
			List<PoIInfo> pInfoList = new List<PoIInfo> ();
			PoIInterface poi = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");

			//parse file with the busstops
			string fileInfos = System.IO.File.ReadAllText (@"Data/overpass_bus_stops_barcelona.json");

			//deserialize json
			var db = MiniJSON.Json.Deserialize (fileInfos) as Dictionary<string,object>;
		
			var elements = db ["elements"] as List<object>;

			int withoutName = 0;
			int counter = 0;

			foreach (var e in elements) {
				var eDict = e as Dictionary<string, object>;

				if (eDict.ContainsKey ("type")) {
					string type = (string)eDict ["type"];
					if (type == "node" &&
						eDict.ContainsKey ("lat") &&
						eDict.ContainsKey ("lon")) {

						var pInfo = new PoIInfo ();
						pInfo.FwCore = new FwCore ();
						pInfo.FwCore.Category = "GnomeTrader";

						double lat = (double)eDict ["lat"];
						double lon = (double)eDict ["lon"];
						pInfo.FwCore.Location = new Location (lat, lon);

						if (eDict.ContainsKey ("name"))
							pInfo.FwCore.Name = (string)eDict ["name"];
						else if (eDict.ContainsKey ("uic_name"))
							pInfo.FwCore.Name = (string)eDict ["uic_name"];
						else if (eDict.ContainsKey ("tags")) {
							var tags = eDict ["tags"] as Dictionary<string, object>;
							if (tags.ContainsKey ("name"))
								pInfo.FwCore.Name = (string)tags ["name"];
							else if (tags.ContainsKey ("uic_name"))
								pInfo.FwCore.Name = (string)tags ["uic_name"];
						} else
							withoutName++;

						if (!string.IsNullOrEmpty (pInfo.FwCore.Name)) {
							pInfoList.Add (pInfo);
						}
					} // contains lat and lon
				}// contains type
			} // foreach element

			Console.WriteLine ("total: " + pInfoList.Count + " without name: " + withoutName);

			foreach (var p in pInfoList) {
				float perc = ((float)counter++ / (float)pInfoList.Count) * 100f;
				
				//if (perc < 10f) continue;

				var retFound = poi.RadialSearch (p.FwCore.Location, 5f, "GnomeTrader");



				if (retFound.Count == 0) {
					poi.Add (p);
					Console.WriteLine (perc.ToString("0.000") + "% \t added: " + p.FwCore.Name);
				}
				else
					Console.WriteLine (perc.ToString("0.000") + "% \t skipped: " + p.FwCore.Name);
			}
			
		}

		public static void Main (string[] args)
		{

			//Example ();

			ImportFromOverPass ();

			Console.WriteLine ("Done.");
			Console.ReadKey ();

		}
	}
}
