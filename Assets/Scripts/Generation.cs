using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generation 
{
	public List<ShipArchive> shipArchives;

	public Generation(List<ShipArchive> shipArchives)
	{
		this.shipArchives = shipArchives.Sort();
	}

	public List<ShipArchive> SUSSelection(int numberToSelect)
	{
		if (numberToSelect > shipArchives.Count || numberToSelect < 0)
			return null;

		double sumOfFitness = 0;
		foreach (ShipArchive s in shipArchives)
		{
			sumOfFitness += s.fitness;
		}



	}

}
