using UnityEngine;
using System.Collections;

public class GameOverInfo : MonoBehaviour 
{
	public int score;
	public int numPlayers;	

	void Start()
	{
		GameObject.DontDestroyOnLoad(this);
	}
}
