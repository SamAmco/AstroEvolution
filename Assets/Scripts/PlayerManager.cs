using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour 
{
	List<PlayerScript> players;

	// Use this for initialization
	void Start () 
	{
		players = new List<PlayerScript>(GetComponentsInChildren<PlayerScript>());
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
}





























