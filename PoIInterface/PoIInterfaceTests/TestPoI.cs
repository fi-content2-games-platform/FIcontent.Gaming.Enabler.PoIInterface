using System;
using NUnit.Framework;
using PoI;
using PoI.Data;

namespace PoIInterfaceTest
{
	[TestFixture()]
	public class TestPoI
	{
		public static string POI_DP_URL = "http://195.220.224.207";

		[Test()]
		public void TestFromString ()
		{
			string poiStr = System.IO.File.ReadAllText (@"Data/test_pois.json");
			//file is found
			Assert.IsNotEmpty (poiStr);
		

			var list = PoIInterface.GetFromString (poiStr);
			Assert.IsNotNull (list);
			Assert.Greater (list.Count, 0);
		}

		[Test()]
		public void TestDeleteNotExists ()
		{
			PoIInfo pInfo = new PoIInfo ("00000000-acd2-4cdf-a65e-cded7bc7833e");
			PoIInterface pInterface = new PoIInterface (POI_DP_URL);

			try {
				pInterface.Delete (pInfo);
			} catch (System.Net.WebException ex) {
				Assert.AreEqual ("The specified UUID was not found from the database!", ex.Message);
			}

		}

		[Test()]
		public void TestAddDelete ()
		{
			PoIInterface pInterface = new PoIInterface (POI_DP_URL);
			PoIInfo pInfo = new PoIInfo ();
			FwCore fwCore = new FwCore ();
			fwCore.Name = "asd";
			fwCore.Category = "restaurant";
			fwCore.Location = new Location (0.345, 0.66577);
			fwCore.Source = new Source ("OpenStreetMap", @"http://www.openstreetmap.org", @"http://www.openstreetmap.org/copyright");

			pInfo.FwCore = fwCore;
			pInfo.FwTime = FwTime.Open;

			pInfo = pInterface.Add (pInfo);
			Console.WriteLine (pInfo.Id);

			Assert.IsNotNullOrEmpty (pInfo.Id);

			bool retDelete = pInterface.Delete (pInfo);

			Assert.IsTrue (retDelete);

		}

		[Test()]
		public void TestUpdate ()
		{
			PoIInterface pInterface = new PoIInterface (POI_DP_URL);
			var pInfo = pInterface.GetByID ("ae01d34a-d0c1-4134-9107-71814b4805af", true);
			Assert.AreEqual (1, pInfo.Count);

			pInfo [0].FwCore.Description = Guid.NewGuid ().ToString ();
			pInfo [0].FwCore.LastUpdate = LastUpdate.Now;

			bool ret = pInterface.Update (pInfo [0]);
			
			Assert.IsTrue (ret);
		}

		[Test()]
		public void TestGetByID ()
		{
			PoIInterface pInterface = new PoIInterface (POI_DP_URL);

			var pInfo = pInterface.GetByID ("ae01d34a-d0c1-4134-9107-71814b4805af", false);

			Assert.IsNotNull (pInfo);
			Assert.AreEqual (pInfo [0].FwCore.Name, "Test POI 1");
		}

		[Test()]
		public void TestBBoxSearch ()
		{
			PoIInterface pInterface = new PoIInterface (POI_DP_URL);
			
			var list = pInterface.BBoxSearch (
				new Location (1, 1),
				new Location (1, 1),
				100);
			                                 
			Assert.Greater (list.Count, 0);
		}

		[Test()]
		public void TestRadialSearch ()
		{
			PoIInterface pInterface = new PoIInterface (POI_DP_URL);			
			var list = pInterface.RadialSearch (new Location (1, 1), 1000f);
			Assert.Greater (list.Count, 0);

			var list2 = pInterface.RadialSearch (new Location (1, 1), 1000f, 1);
			Assert.AreEqual (1, list2.Count);
		}
	}
}