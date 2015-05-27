using UnityEngine;
using System.Collections;
using PoI;
using PoI.Data;

public class Example : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PoIInterface pInterface = new PoIInterface ("http://dev.cie.fi/FI-WARE/poi_dp");
		var results = pInterface.RadialSearch(new Location(47f, 8f), 10000f);

		foreach(var r in results)
			Debug.Log(r);
	}
}
