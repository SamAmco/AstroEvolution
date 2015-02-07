﻿using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour 
{
	public int limbCount;
	private double cumulativeDistance = 0;
	public ShipChromosomeNode rootNode;
	public TargetableScript currentTarget;
	private double lifetime;
	private int orbsCollected = 0;
	private double fuelUsed = 0;
	
	void Update()
	{
		lifetime += Time.deltaTime;
	}

	public void collectedOrb()
	{
		++orbsCollected;
	}

	public void addToCumulativeDistance(double d)
	{
		cumulativeDistance += d;
	}

	public void fuelUnitUsed()
	{
		fuelUsed += Config.FUEL_COST;
	}

	public double getFitness()
	{
		return (((cumulativeDistance + fuelUsed) / (double)limbCount) / (double)lifetime) 
			* (double)Mathf.Pow(0.75f, (float)orbsCollected);
	}

	public Vector3 getForward()
	{
		return transform.rotation * new Vector3(0, 1f, 0);
	}
	public Vector3 getTarget()
	{
		if (currentTarget == null)
		{
			if (!findNewTarget())
				return Vector3.zero;
		}
		Debug.DrawLine(transform.position, currentTarget.transform.position);
		return currentTarget.transform.position;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private bool findNewTarget()
	{
		float minDistance = float.MaxValue;
		TargetableScript[] targetList = GameObject.FindObjectsOfType<TargetableScript>();

		if (targetList.Length < 1)
			return false;

		foreach (TargetableScript ts in targetList)
		{
			float dist = (ts.transform.position - transform.position).sqrMagnitude;
			if (dist < minDistance)
			{
				currentTarget = ts;
				minDistance = dist;
			}
		}
		return true;
	}
}




















