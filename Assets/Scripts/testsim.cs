using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testsim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var model = new GameModel ();
		Debug.Log ("Balance at turn:" + model.turn + " is: $" + model.balance);
		model.NextTurn();
		Debug.Log ("Balance at turn:" + model.turn + " is: $" + model.balance);
		model.NextTurn();
		Debug.Log ("Balance at turn:" + model.turn + " is: $" + model.balance);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
