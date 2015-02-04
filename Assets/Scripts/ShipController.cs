using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour 
{
	private double fitness;
	public ShipChromosomeNode rootNode;

	void Start()
	{
		fitness = 0;
	}

	public void addToFitness(double d)
	{
		fitness += d;
	}

	public double getFitness()
	{
		return fitness;
	}

	public Vector3 getForward()
	{
		return transform.rotation * new Vector3(0, 1f, 0);
	}
	public Vector3 getTarget()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
