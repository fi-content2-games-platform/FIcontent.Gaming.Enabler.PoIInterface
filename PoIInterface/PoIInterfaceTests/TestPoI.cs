using System;
using NUnit.Framework;
using PoI;
using PoI.Data;

namespace PoIInterfaceTest
{
	[TestFixture()]
	public class TestPoI
	{
		[Test()]
		public void TestFromString()
		{
			string poiStr = System.IO.File.ReadAllText(@"Data/test_pois.json");
			//file is found
			Assert.IsNotEmpty(poiStr);
		

			var list = PoIInterface.GetFromString(poiStr);
			Assert.IsNotNull(list);
			Assert.Greater(list.Count, 0);
		}

		[Test()]
		public void TestDeleteNotExists ()
		{
			PoIInfo pInfo = new PoIInfo ("00000000-acd2-4cdf-a65e-cded7bc7833e");
			PoIInterface pInterface = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");

			try {
				pInterface.Delete (pInfo);
			} catch (System.Net.WebException ex) {
				Assert.AreEqual ("The specified UUID was not found from the database!", ex.Message);
			}

		}

		[Test()]
		public void TestAddDelete ()
		{
			PoIInterface pInterface = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");
			PoIInfo pInfo = new PoIInfo ();
			FwCore fwCore = new FwCore ();
			fwCore.Name = "asd";
			fwCore.Category = "restaurant";
			fwCore.Location = new Location (0.345, 0.66577);
			fwCore.Source = new Source ("OpenStreetMap", @"http://www.openstreetmap.org", @"http://www.openstreetmap.org/copyright");

			pInfo.FwCore = fwCore;
			pInfo.FwTime = FwTime.Open;

			bool ret = pInterface.Add (ref pInfo);
			Console.WriteLine (pInfo.Id);

			Assert.IsTrue (ret);

			bool retDelete = pInterface.Delete (pInfo);

			Assert.IsTrue (retDelete);

		}

		[Test()]
		public void TestUpdate ()
		{
			PoIInterface pInterface = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");
			var pInfo = pInterface.GetByID ("0ac396ab-acd2-4cdf-a65e-cded7bc7833e", true);
			Assert.AreEqual (1, pInfo.Count);

			pInfo[0].FwCore.Source.Licence = @"http://www.sorrata.com";

			bool ret = pInterface.Update (pInfo [0]);
			
			Assert.IsTrue (ret);
		}

		[Test()]
		public void TestGetByID ()
		{
			PoIInterface pInterface = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");

			var pInfo = pInterface.GetByID ("0ac396ab-acd2-4cdf-a65e-cded7bc7833e", false);

			Assert.IsNotNull (pInfo);
			Assert.AreEqual (pInfo [0].FwCore.Name, "Mundt&Morczinek");
		}

		[Test()]
		public void TestBBoxSearch ()
		{
			PoIInterface pInterface = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");
			
			var list = pInterface.BBoxSearch (
				new Location (52.399033639398, 9.6893684005723),
				new Location (52.364874973784, 9.8541633224465),
				100);
			                                 
			Assert.Greater (list.Count, 0);
		}

		[Test()]
		public void TestRadialSearch ()
		{
			PoIInterface pInterface = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");
			
			var list = pInterface.RadialSearch (new Location (52.399033639398, 9.6893684005723), 1000f);

			Assert.Greater (list.Count, 0);

			var list2 = pInterface.RadialSearch (new Location (52.399033639398, 9.6893684005723), 1000f, 1);

			Assert.AreEqual (1, list2.Count);
		}
	}
}