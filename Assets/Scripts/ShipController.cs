using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour 
{
	public int limbCount;
	private double cumulativeDistanceFromTarget = 0;
	public ShipChromosomeNode rootNode;
	public TargetableScript currentTarget;
	private double lifetime;
	private int orbsCollected = 0;
	private double fuelUsed = 0;
	private Vector3 lastPos;
	private double stillnessValue = 0;

	void Start()
	{
		lastPos = transform.position;
	}

	void Update()
	{
		lifetime += Time.deltaTime;

		if ((lastPos - transform.position).sqrMagnitude == 0)
			stillnessValue += Config.STILLNESS_COST;

		lastPos = transform.position;
	}

	public void addForceAtPosition(Vector3 force, Vector3 position, ForceMode forceMode)
	{
		rigidbody.AddForceAtPosition(force, position, forceMode);
	}

	public void collectedOrb()
	{
		++orbsCollected;
	}

	public void addToCumulativeDistance(double d)
	{
		cumulativeDistanceFromTarget += d;
	}

	public void fuelUnitUsed()
	{
		fuelUsed += Config.FUEL_COST;
	}

	public double getFitness()
	{
		return (((cumulativeDistanceFromTarget / (double)limbCount) + fuelUsed + stillnessValue) 
		         / (double)lifetime) 
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




















