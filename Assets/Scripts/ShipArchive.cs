using UnityEngine;
using System.Collections;
using System;

public class ShipArchive : IComparable
{
	public double fitness;
	public ShipChromosomeNode root;

	public ShipArchive(ShipChromosomeNode rootNode, double fitness)
	{
		this.root = rootNode;
		this.fitness = fitness;
	}

	public int CompareTo(object o)
	{
		ShipArchive sa2 = (ShipArchive)o;

		if (fitness < sa2.fitness)
			return -1;
		else if (fitness > sa2.fitness)
			return 1;
		else return 0;
	}
}
