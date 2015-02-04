using UnityEngine;
using System.Collections;

public class ShipArchive
{
	public double fitness;
	public ShipChromosomeNode root;

	public ShipArchive(ShipChromosomeNode rootNode, double fitness)
	{
		this.root = rootNode;
		this.fitness = fitness;
	}
}
