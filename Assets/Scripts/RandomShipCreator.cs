using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomShipCreator : MonoBehaviour 
{ 
	public OrbGeneratorScript orbGenerator;
	private ShipController lastShip;
	

	public void generateRandomShip()
	{
		ShipChromosomeNode n = ShipChromosomeNode.generateRandomShip();
		generatePhysicalShip(n);
	}

	public void evaluateShip(PopulationManager populationManager)
	{
		if (lastShip != null)
		{
			ShipArchive shipArchive = new ShipArchive(lastShip.rootNode, lastShip.getFitness());
			populationManager.shipEvaluated(shipArchive);
			GameObject.Destroy(lastShip.gameObject);
		}
	}

	public void generatePhysicalShip(ShipChromosomeNode root)
	{
		//Debug.Log(root.getString());
		GameObject g = (GameObject)GameObject.Instantiate(Resources.Load(Config.HEAVY_BLOCK_PREFAB_LOCATION),
		                       Vector3.zero,
		                       Quaternion.identity);

		Rigidbody r = g.AddComponent<Rigidbody>();
		r.mass = Config.BLOCK_MASS * root.getListOfNodes().Count;
		r.drag = Config.BLOCK_DRAG;
		r.angularDrag = Config.BLOCK_ANGULAR_DRAG;
		//r.constraints = RigidbodyConstraints.FreezeAll;
		r.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

		BlockScript b = g.GetComponent<BlockScript>();
		ShipController s = g.AddComponent<ShipController>();
		s.rootNode = root;

		lastShip = s;

		b.initialize(root, s);
		b.combineSubMeshes();
		g.transform.position = transform.position;
		g.SetActive(false);
	}

	public void activate()
	{
		lastShip.gameObject.SetActive(true);
	}
}





















