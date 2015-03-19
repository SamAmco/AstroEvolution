using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSpawnPoint : RandomShipCreator 
{
	PopulationManager populationManager;

	// Use this for initialization
	void Start () 
	{
		this.orbsRoot = this.gameObject;
		populationManager = GameObject.FindObjectOfType<PopulationManager>();
	}

	public void nextGeneration()
	{
		if (lastShip != null)
			GameObject.Destroy(lastShip.gameObject);

		ShipChromosomeNode bestOfAllTime = populationManager.getBestOfAllTime();

		if (bestOfAllTime == null)
		{
			generateRandomShip();
		}
		else
		{
			generatePhysicalShip(bestOfAllTime);//Random.Range(0, shipArchives.Count / 2)].root);
		}
		activate();
	}
}
