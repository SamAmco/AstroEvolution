using UnityEngine;
using System.Collections;

public class OrbGeneratorScript : MonoBehaviour 
{
	public OrbScript[] orbs;
	public Object orbPrefab;
	private Vector3[] orbPositions;

	void Start()
	{
		orbPositions = new Vector3[orbs.Length];
		for (int i =0; i < orbs.Length; i++)
		{
			orbPositions[i] = orbs[i].transform.position;
		}
	}

	public void resetOrbs()
	{
		foreach (OrbScript o in orbs)
		{
			if (o != null)
				GameObject.Destroy(o.gameObject);
		}

		int i = 0;
		foreach (Vector3 v in orbPositions)
		{
			orbs[i] = ((GameObject)Instantiate(orbPrefab, v, Quaternion.identity)).GetComponent<OrbScript>();
			++i;
		}
	}
}
