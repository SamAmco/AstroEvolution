using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreAndPlayersDisplayScript : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		GameOverInfo info = GameObject.FindObjectOfType<GameOverInfo>();
		GetComponent<Text>().text = "Score: " + info.score + "\n"
			+ "Players: " + info.numPlayers;
	}
}
