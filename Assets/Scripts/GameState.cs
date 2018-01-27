using System.Collections;
using System.Collections.Generic;

public class GameState  {
    public static GameModel current = new GameModel();
	public static GameModel next = current.Clone();

	public static void NextTurn()
	{
		next.NextTurn ();
		current = next;
		next = current.Clone ();
	}
}
