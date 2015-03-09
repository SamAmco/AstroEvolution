using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generation 
{
	private List<ShipArchive> shipArchives;

	public Generation() 
	{
		shipArchives = new List<ShipArchive>();
	}

	public int size()
	{
		return shipArchives.Count;
	}

	public void addShipArchive(ShipArchive shipArchive)
	{
		shipArchives.Add(shipArchive);
	}

	public List<ShipChromosomeNode> SUS(uint numberToSelect)
	{
		shipArchives.Sort();
		double sumOfFitness = 0;
		foreach (ShipArchive s in shipArchives)
		{
			sumOfFitness += s.fitness;
			Debug.Log(s.fitness);
		}
		double dist = 1d / (double)numberToSelect;
		double startingPoint = (double)Random.Range(0f, 1f) * dist;

		int numSelected = 1;
		List<ShipChromosomeNode> selectedChromosomes = new List<ShipChromosomeNode>();
		selectedChromosomes.Add(shipArchives[0].root.copyTree());
		while (numSelected < numberToSelect)
		{
			double point = startingPoint + (dist * numSelected);

			double fitnessCount = 0;
			foreach (ShipArchive s in shipArchives)
			{
				fitnessCount += getProportionateFitness(s.fitness, sumOfFitness, shipArchives.Count);
				if (point < fitnessCount)
				{
					selectedChromosomes.Add(s.root.copyTree());
					break;
				}
			}
			numSelected++;
		}
		return selectedChromosomes;
	}

	private static double getProportionateFitness(double fitness, double sumOfFitness, double numItems)
	{
		return (1d - (fitness / sumOfFitness)) / (numItems - 1d);
	}

}
