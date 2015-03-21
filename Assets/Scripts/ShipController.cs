using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShipController : MonoBehaviour 
{
	public int limbCount;
	public bool initialized = false;
	public ShipChromosomeNode rootNode;
	public GameObject currentTarget;

	private List<GameObject> orbs;
	private double cumulativeDistanceFromTarget = 0;
	private double lifetime;
	private int orbsCollected = 0;
	private double fuelUsed = 0;
	private Vector3 lastPos;
	private double stillnessValue = 0;
	private double angularVelValue = 0;
	private double slownessValue = 0;
	private bool isAtRest = false;
	private float restTime = 0;
	private PlayerManager playerManager;
	private bool targetingPlayer = false;
	private float retargetTime = 0;

	void Start()
	{
		lastPos = transform.position;
		playerManager = FindObjectOfType<PlayerManager>();
		initialized = true;
	}

	public void setOrbs(GameObject root)
	{
		orbs = new List<GameObject>();
		foreach (Transform t in root.transform)
			orbs.Add(t.gameObject);
	}

	void FixedUpdate()
	{
		lifetime += Time.deltaTime;
		if (retargetTime > 0)
			retargetTime -= Time.deltaTime;

		if (!targetingPlayer)
		{
			slownessValue += Config.SLOW_COST;
			angularVelValue += Math.Abs((Config.ANGULAR_VELOCITY_COST_MULTIPLIER * rigidbody.angularVelocity).z);

			if ((isAtRest || (lastPos - transform.position).sqrMagnitude <= 0.08f))
			{
				stillnessValue += Config.STILLNESS_COST;
				restTime += Time.deltaTime;
				if (restTime > Config.TIME_TO_DEACTIVATION)
					isAtRest = true;
			}
			else
				restTime = 0;
		}

		lastPos = transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Orb")
		{
			for (int i = 0; i < orbs.Count; ++i)
			{
				if (other.gameObject == orbs[i])
				{
					//orbs[i].SetActive(false);
					orbs.RemoveAt(i);
					currentTarget = null;
					++orbsCollected;
					break;
				}
			}
		}
	}

	public bool addForceAtPosition(Vector3 force, Vector3 position, ForceMode forceMode)
	{
		if (!isAtRest)
			rigidbody.AddForceAtPosition(force, position, forceMode);
		return !isAtRest;
	}

	public void collectedOrb()
	{
		++orbsCollected;
	}

	public void addToCumulativeDistance(double d)
	{
		if (!targetingPlayer)
			cumulativeDistanceFromTarget += d;
	}

	public void fuelUnitUsed()
	{
		if (!targetingPlayer)
			fuelUsed += Config.FUEL_COST;
	}

	public double getFitness()
	{
		double d = (((cumulativeDistanceFromTarget / (double)limbCount) 
		             + fuelUsed + stillnessValue + slownessValue + angularVelValue) 
		            / (double)lifetime) 
			* (double)Mathf.Pow(Config.ORB_MULTIPLIER, (float)orbsCollected)
				* (double)Mathf.Pow(Config.BLOAT_MULTIPLIER, rootNode.engineCount());

		if (d < 0)
		{
			Debug.Log((cumulativeDistanceFromTarget / (double)limbCount));
			Debug.Log(fuelUsed);
			Debug.Log(stillnessValue);
			Debug.Log(slownessValue);
			Debug.Log(angularVelValue);
		}
		return d;
	}

	public Vector3 getForward()
	{
		/*Debug.DrawLine(transform.position,
		               transform.position + (transform.rotation * new Vector3(0, 1f, 0)),
		               Color.red);*/
		return transform.rotation * new Vector3(0, 1f, 0);
	}

	public Vector3 getTarget()
	{
		if (currentTarget == null || currentTarget.activeSelf == false 
		    || (retargetTime <= 0 
		    	&& (currentTarget.transform.position - transform.position).sqrMagnitude 
		    		> Config.MAX_DISTANCE_TO_CHANGE_TARGET))
		{
			retargetTime = Config.MAX_RETARGET_INTERVAL;
			if (!findNewTarget())
				return Vector3.zero;
		}
		Debug.DrawLine(transform.position, currentTarget.transform.position);
		return currentTarget.transform.position;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private bool findNewTarget()
	{
		float minDistance = float.MaxValue;

		bool found = false;
		foreach (GameObject g in orbs)
		{
			if (g == null || g.activeSelf == false)
				continue;

			float dist = (g.transform.position - transform.position).sqrMagnitude;
			if (dist < minDistance)
			{
				found = true;
				currentTarget = g;
				minDistance = dist;
			}
		}
		if (!found)
		{
			targetingPlayer = true;
			currentTarget = playerManager.nearestPlayerTo(transform.position);
			if (currentTarget != null)
				found = true;
		}
		return found;
	}
}




















