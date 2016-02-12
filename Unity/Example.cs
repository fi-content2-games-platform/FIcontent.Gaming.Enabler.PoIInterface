using UnityEngine;
using System.Collections;
using System;
using PoI;
using PoI.Data;

public class Example : MonoBehaviour
{

	public string poiURL = "http://195.220.224.207";
	
	private PoIInterface poiInterface;
	private string poiID = string.Empty;
	
	void Start ()
	{
		poiInterface = new PoIInterface (poiURL);			
	}
	
	void OnGUI ()
	{
		
		if (GUILayout.Button ("Radial Search"))
			RadialSearch ();
		if (GUILayout.Button ("Add POI"))
			poiID = AddPoI ();
		if (GUILayout.Button ("Delete POI"))
			DeletePoI ();				
		if (GUILayout.Button ("Update POI"))
			UpdatePoI ();
		
		GUILayout.Label ("POI ID:");
		poiID = GUILayout.TextField (poiID);
		
		
	}
	
	public void RadialSearch ()
	{
		foreach (PoIInfo poi in poiInterface.RadialSearch(new Location(1, 1), 1))
			Debug.Log (poi.Id + " " + poi.FwCore);
	}
	
	public void DeletePoI ()
	{
		var p = new PoIInfo ();
		p.Id = poiID;
		if (poiInterface.Delete (p))
			poiID = string.Empty;
	}
	
	public string AddPoI ()
	{
		PoIInfo poi = new PoIInfo ();
		poi.FwCore = new FwCore ();
		poi.FwCore.Location = new Location (1, 1);
		poi.FwCore.Category = "test";
		poi.FwCore.Name = "test poi";
		var ret = poiInterface.Add (poi);
		Debug.Log (ret.FwCore);
		
		return ret.Id;
	}
	
	public void UpdatePoI ()
	{
		var poiList = poiInterface.GetByID (poiID, true);
		
		if (poiList.Count == 0) {
			Debug.LogWarning ("no pois found");
			return;
		}
		
		var poi = poiList [0];
		Debug.Log ("updating " + poi.FwCore);
		
		poi.FwCore.Description = Guid.NewGuid ().ToString ();
		poi.FwCore.LastUpdate = LastUpdate.Now;
		Debug.Log (poiInterface.Update (poi));			
	}
}
