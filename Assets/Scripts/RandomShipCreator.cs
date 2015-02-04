using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomShipCreator : MonoBehaviour 
{ 
	private List<ShipArchive> shipArchives;
	private ShipController lastShip;

	void Start()
	{
		shipArchives = new List<ShipArchive>();
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(0,0, 200, 50), "GENERATE RANDOM SHIP"))
		{
			ShipChromosomeNode n = generateRandomShipChromosome();
			Debug.Log(n.getString());
			generatePhysicalShip(n);
		}
	}

	private ShipChromosomeNode generateRandomShipChromosome()
	{
		return ShipChromosomeNode.generateRandomShip();
	}

	private void generatePhysicalShip(ShipChromosomeNode root)
	{
		if (lastShip != null)
		{
			ShipArchive shipArchive = new ShipArchive(lastShip.rootNode, lastShip.getFitness());
			shipArchives.Add(shipArchive);

			GameObject.Destroy(lastShip.gameObject);
		}

		GameObject g = (GameObject)GameObject.Instantiate(Resources.Load(Config.HEAVY_BLOCK_PREFAB_LOCATION),
		                       Vector3.zero,
		                       Quaternion.identity);

		BlockScript b = g.GetComponent<BlockScript>();
		ShipController s = g.AddComponent<ShipController>();
		s.rootNode = root;

		lastShip = s;

		b.initialize(root, s);
	}
}
