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

	public List<ShipArchive> getShipArchives()
	{
		return shipArchives;
	}

	public List<ShipChromosomeNode> SUS(uint numberToSelect)
	{
		shipArchives.Sort();
		double sumOfFitness = 0;
		foreach (ShipArchive s in shipArchives)
		{
			sumOfFitness += 1d / s.fitness;
		}
		double dist = 1d / (double)numberToSelect;
		double startingPoint = (double)Random.Range(0f, 1f) * dist;

		int numSelected = 0;
		List<ShipChromosomeNode> selectedChromosomes = new List<ShipChromosomeNode>();
		while (numSelected < numberToSelect)
		{
			double point = startingPoint + (dist * numSelected);

			double fitnessCount = 0;
			foreach (ShipArchive s in shipArchives)
			{
				fitnessCount += getProportionateFitness(s.fitness, sumOfFitness);
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

	private static double getProportionateFitness(double fitness, double sumOfFitness)
	{
		return 1d / (fitness * sumOfFitness);
	}
}
