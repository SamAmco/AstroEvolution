using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	List<PlayerScript> players;
	List<PopulationManager> populationManagers;
	List<GameSpawnPoint> gameSpawnPoints;
	int numOfCollectables;
	int lives;
	int score;

	// Use this for initialization
	void Start () 
	{
		gameSpawnPoints = new List<GameSpawnPoint>(FindObjectsOfType<GameSpawnPoint>());
		populationManagers = new List<PopulationManager>(FindObjectsOfType<PopulationManager>());
		players = new List<PlayerScript>(GetComponentsInChildren<PlayerScript>());
		numOfCollectables = GameObject.FindGameObjectsWithTag("Collectable").Length;
		lives = Config.PLAYERS_START_LIVES;
		triggerSpawnPoints();
	}

	public void playerHit()
	{
		lives--;
		if (lives < 0)
		{
			GameObject g = new GameObject();
			GameOverInfo info = g.AddComponent<GameOverInfo>();
			info.numPlayers = 1;
			info.score = score;
			Application.LoadLevel("GameOverScene");
		}
	}

	private void triggerSpawnPoints()
	{
		foreach (GameSpawnPoint g in gameSpawnPoints)
		{
			g.nextGeneration();
		}
	}

	public void collectedCollectable()
	{
		--numOfCollectables;
		++score;
		if (numOfCollectables == 0)
		{
			foreach(PopulationManager p in populationManagers)
			{
				p.initializeNextGeneration();
			}
			foreach(PlayerScript p in players)
			{
				p.nextGeneration();
			}
			triggerSpawnPoints();
			numOfCollectables = GameObject.FindGameObjectsWithTag("Collectable").Length;
		}
	}

	public Vector3 getAveragePlayerPos()
	{
		Vector3 p = Vector3.zero;
		foreach (PlayerScript pl in players)
		{
			p += pl.transform.position;
		}
		return p / (float)players.Count;
	}

	public GameObject nearestPlayerTo(Vector3 point)
	{
		float dist = (point - players[0].transform.position).sqrMagnitude;
		GameObject p = players[0].gameObject;

		foreach (PlayerScript pl in players)
		{
			float d = (point - pl.transform.position).sqrMagnitude;
			if (d < dist)
			{
				dist = d;
				p = pl.gameObject;
			}
		}

		return p;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20), "Lives: " + lives);
		GUI.Label(new Rect(10, 25, 100, 20), "Score: " + score);
	}
}





























