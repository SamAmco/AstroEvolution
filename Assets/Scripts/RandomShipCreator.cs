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

	private void generatePhysicalShip(ShipChromosomeNode root)
	{
		if (lastShip != null)
		{
			ShipArchive shipArchive = new ShipArchive(lastShip.rootNode, lastShip.getFitness());
			PopulationManager.instance.shipArchives.Add(shipArchive);

			GameObject.Destroy(lastShip.gameObject);
		}

		GameObject g = (GameObject)GameObject.Instantiate(Resources.Load(Config.HEAVY_BLOCK_PREFAB_LOCATION),
		                       transform.position,
		                       Quaternion.identity);

		BlockScript b = g.GetComponent<BlockScript>();
		ShipController s = g.AddComponent<ShipController>();
		s.rootNode = root;

		lastShip = s;

		b.initialize(root, s);
	}
}
