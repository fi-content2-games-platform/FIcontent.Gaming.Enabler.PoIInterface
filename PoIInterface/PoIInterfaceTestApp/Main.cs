using System;
using PoI;
using PoI.Data;
using System.Collections.Generic;

namespace PoIInterfaceTestApp
{
	class MainClass
	{
		static string POI_DP_URL = "http://195.220.224.207";

		static void Search ()
		{
			PoIInterface poi = new PoIInterface (POI_DP_URL);
			var list = poi.RadialSearch (new Location (1, 1), 20000, 2);
			Console.WriteLine (list [0].FwCore);
		}

		static void Update ()
		{
			PoIInterface pInterface = new PoIInterface (POI_DP_URL);
			var pInfo = pInterface.GetByID ("ae01d34a-d0c1-4134-9107-71814b4805af", true);
			
			pInfo [0].FwCore.Description = Guid.NewGuid ().ToString ();			
            pInfo[0].FwCore.LastUpdate.Responsible = "x";
			
			bool ret = pInterface.Update (pInfo [0]);

            Console.WriteLine(ret);
		}

		public static void Main (string[] args)
		{
			Update ();

			Console.ReadKey ();

		}
	}
}
