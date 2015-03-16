using UnityEngine;
using System.Collections;

public class OrbGeneratorScript : MonoBehaviour 
{
	private GameObject[] orbs;
	private Vector3[] orbPositions;

	void Start()
	{
		orbs = GameObject.FindGameObjectsWithTag("Orb");
		orbPositions = new Vector3[orbs.Length];
		for (int i =0; i < orbs.Length; i++)
		{
			orbPositions[i] = orbs[i].transform.position;
		}
	}

	public void resetOrbs()
	{
		foreach (GameObject o in orbs)
		{
			o.SetActive(true);
		}
	}
}
