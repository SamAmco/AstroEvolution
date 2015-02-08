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

	public void addShipArchive(ShipArchive shipArchive)
	{
		shipArchives.Add(shipArchive);
	}

	public List<ShipChromosomeNode> SUS(uint numberToSelect)
	{
		double sumOfFitness = 0;
		foreach (ShipArchive s in shipArchives)
		{
			sumOfFitness += s.fitness;
		}
		double dist = 1d / (double)numberToSelect;
		double startingPoint = (double)Random.Range(0f, 1f);

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

	private double getProportionateFitness(double fitness, double sumOfFitness)
	{
		return 1d - (fitness / sumOfFitness);
	}

}
