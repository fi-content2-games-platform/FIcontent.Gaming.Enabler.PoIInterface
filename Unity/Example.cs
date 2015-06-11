using UnityEngine;
using System.Collections;
using PoI;
using PoI.Data;

public class Example : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		PoIInterface pInterface = new PoIInterface ("http://195.220.224.207");
		var results = pInterface.RadialSearch (new Location (1f, 1f), 10000f);

		foreach (var r in results)
			Debug.Log (r);
	}
}
