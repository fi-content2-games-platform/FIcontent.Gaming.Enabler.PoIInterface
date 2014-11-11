using System;
using NUnit.Framework;
using PoI.Data;
using PoI.Serialization;

namespace PoIInterfaceTest
{
	[TestFixture()]
	public class TestSerialization
	{
		[Test()]
		public void TestPoIInfo ()
		{

			var fwCore = new FwCore ();
			fwCore.Location = new Location (52.365299224853516, 9.6899404525756836);
			fwCore.Category = "restaurant";
//			var source = new Source ();
//			source.Name = "OpenStreetMap";
//			source.WebSite = @"http://www.openstreetmap.org";
//			source.Licence = @"http://www.openstreetmap.org/copyright";
//			fwCore.Source = source;
			fwCore.Name = "Mi Pueblito";
			fwCore.Description = "no steps, to wheelchair toilets";
			fwCore.LastUpdate = new LastUpdate ("x", 1234);
			PoIInfo pInfo = new PoIInfo ("asdb-asdf-asdf-asdf");
			pInfo.FwCore = fwCore;

			string jsonLocationString = @"""location"":{""wgs84"":{""latitude"":52.365299224853516,""longitude"":9.6899404525756836}}";
			string jsonLastUpdateString = @"""last_update"":{""timestamp"":1234,""responsible"":""x""}";
			string jsonNameString = @"""name"":{"""":""Mi Pueblito""}";
			string jsonDescrString = @"""description"":{"""":""no steps, to wheelchair toilets""}";
			string jsonCatString = @"""category"":""restaurant""";
			//string jsonSourceString = @"""source"":{""name"":""OpenStreetMap"",""website"":""http://www.openstreetmap.org"",""license"":""http://www.openstreetmap.org/copyright""}";
			string jsonFwCore = string.Format ("{0},{1},{2},{3},{4}", jsonLocationString, jsonNameString, jsonCatString, jsonDescrString, jsonLastUpdateString);
			string jsonPoi = @"{""asdb-asdf-asdf-asdf"":{""fw_core"":{" + jsonFwCore + "}}}";
			//string jsonPoiUpdate = @"{""asdb-asdf-asdf-asdf"":{""fw_core"":{" + jsonFwCore + @"},""fw_time"":{""type"":""open""}}}";

			var poiObject = MiniJSON.Json.Deserialize (jsonPoi) as System.Collections.Generic.Dictionary<string, object>;
			foreach (var k in poiObject) {
				var poiDeserialized = new PoIInfo (k);
				Assert.AreEqual (poiDeserialized, pInfo);
			}


			var poiSerialized = pInfo.ToDictionary (true);
			var poiJsonSerialized = MiniJSON.Json.Serialize (poiSerialized);
			Assert.AreEqual (jsonPoi, poiJsonSerialized);
		}

		[Test()]
		public void TestFwCore ()
		{
			var fwCore = new FwCore ();
			fwCore.Location = new Location (52.365299224853516, 9.6899404525756836);
			fwCore.Category = "restaurant";
			var source = new Source ();
			source.Name = "OpenStreetMap";
			source.WebSite = @"http://www.openstreetmap.org";
			source.Licence = @"http://www.openstreetmap.org/copyright";
			fwCore.Source = source;
			fwCore.Name = "Mi Pueblito";
			fwCore.Description = "no steps, to wheelchair toilets";
			fwCore.LastUpdate = new LastUpdate ("x", 1234);

			string jsonLocationString = @"""location"":{""wgs84"":{""latitude"":52.365299224853516,""longitude"":9.6899404525756836}}";
			string jsonLastUpdateString = @"""last_update"":{""timestamp"":1234,""responsible"":""x""}";
			string jsonNameString = @"""name"":{"""":""Mi Pueblito""}";
			string jsonDescrString = @"""description"":{"""":""no steps, to wheelchair toilets""}";
			string jsonCatString = @"""category"":""restaurant""";
			string jsonSourceString = @"""source"":{""name"":""OpenStreetMap"",""website"":""http://www.openstreetmap.org"",""license"":""http://www.openstreetmap.org/copyright""}";
			string jsonFwCore = string.Format ("{0},{1},{2},{3},{4},{5}", jsonLocationString, jsonSourceString, jsonNameString, jsonCatString, jsonDescrString, jsonLastUpdateString);
			jsonFwCore = "{" + jsonFwCore + "}";

			var fwCoreObject = MiniJSON.Json.Deserialize (jsonFwCore);
			var fwCoreDeserialized = new FwCore (fwCoreObject);
			Assert.AreEqual (fwCoreDeserialized, fwCore);

			var fwCoreSerialized = fwCore.ToDictionary ();
			var fwCoreJsonSerialized = MiniJSON.Json.Serialize (fwCoreSerialized);
			Assert.AreEqual (jsonFwCore, fwCoreJsonSerialized);
		}

		[Test()]
		public void TestFwTime ()
		{
			var fwTime = new FwTime ();
			fwTime.LastUpdate = new LastUpdate ("x", 1234);
			fwTime.Type = "open";

			string jsonLastUpdateString = @"""last_update"":{""timestamp"":1234,""responsible"":""x""}";
			string jsonTypeString = @"""type"":""open""";
			string jsonSchedString = @"""schedule"":[]";
			string jsonFwTime = string.Format ("{0},{1},{2}", jsonTypeString, jsonSchedString, jsonLastUpdateString);
			jsonFwTime = "{" + jsonFwTime + "}";

			var fwTimeObject = MiniJSON.Json.Deserialize (jsonFwTime);
			var fwTimeDeserialized = new FwTime (fwTimeObject);
			Assert.AreEqual (fwTimeDeserialized, fwTime);

			var fwTimeMarshalled = fwTime.ToDictionary ();
			var fwTimeJsonSerialized = MiniJSON.Json.Serialize (fwTimeMarshalled);
			Assert.AreEqual (jsonFwTime, fwTimeJsonSerialized);
		}

		[Test()]
		public void TestLastUpdate ()
		{
			var lastupdate = new LastUpdate ();
			lastupdate.TimeStamp = 1234;
			lastupdate.Responsible = "x";

			string lastUpdateJson = @"{""timestamp"":1234,""responsible"":""x""}";

			var lastupdateObject = MiniJSON.Json.Deserialize (lastUpdateJson);
			var lastupdateDeserialized = new LastUpdate (lastupdateObject);
			Assert.AreEqual (lastupdateDeserialized, lastupdate);

			var lastupdateMarshalled = lastupdate.ToDictionary ();
			var lastupdateJsonSerialized = MiniJSON.Json.Serialize (lastupdateMarshalled);
			Assert.AreEqual (lastUpdateJson, lastupdateJsonSerialized);
		}

		[Test()]
		public void TestLocation ()
		{
			var location = new Location (52.365299224853516, 9.6899404525756836, 0);

			string jsonLocation = @"{""wgs84"":{""latitude"":52.365299224853516,""longitude"":9.6899404525756836}}";

			var locationObject = MiniJSON.Json.Deserialize (jsonLocation);
			var locationDeserialized = new Location (locationObject);
			Assert.AreEqual (locationDeserialized, location);

			var locationSerialized = location.ToDictionary ();
			var locationJsonSerialized = MiniJSON.Json.Serialize (locationSerialized);
			Assert.AreEqual (jsonLocation, locationJsonSerialized);
		}

		[Test()]
		public void TestSource ()
		{
			var source = new Source ("OpenStreetMap", @"http://www.openstreetmap.org", @"http://www.openstreetmap.org/copyright");
			string jsonSource = @"{""name"":""OpenStreetMap"",""website"":""http://www.openstreetmap.org"",""license"":""http://www.openstreetmap.org/copyright""}";
		
			var sourceObject = MiniJSON.Json.Deserialize (jsonSource);
			var sourceDeserialized = new Source (sourceObject);
			Assert.AreEqual (source, sourceDeserialized);

			var sourceSerialized = source.ToDictionary ();
			var sourceJsonSerialized = MiniJSON.Json.Serialize (sourceSerialized);
			Assert.AreEqual (jsonSource, sourceJsonSerialized);


		}
	}
}

