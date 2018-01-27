using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeklyActionController : MonoBehaviour {
	public void EndTurn()
	{
		GameState.NextTurn ();
	}
}
