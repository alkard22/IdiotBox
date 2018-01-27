using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekCounter : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		this.GetComponent<UnityEngine.UI.Text>().text = "" + GameState.current.turn;
	}
}
