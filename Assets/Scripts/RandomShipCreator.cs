using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomShipCreator : MonoBehaviour 
{ 
	public OrbGeneratorScript orbGenerator;
	private List<ShipArchive> shipArchives;
	private ShipController lastShip;


	float currentTimer;
	const float maxTimer = 12;

	void Start()
	{
		shipArchives = new List<ShipArchive>();
		currentTimer = maxTimer;
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(0,0, 200, 50), "GENERATE RANDOM SHIP"))
		{
			ShipChromosomeNode n = generateRandomShipChromosome();
			//Debug.Log(n.getString());
			generatePhysicalShip(n);
		}
		if (GUI.Button(new Rect(200,0, 200, 50), "GENERATE BEST SHIP"))
		{
			shipArchives.Sort();
			for (int i = 0; i < shipArchives.Count; ++i)
				Debug.Log(shipArchives[i].fitness);
			//Debug.Log(n.getString());
			generatePhysicalShip(shipArchives[0].root);
		}
	}

	void Update()
	{
		currentTimer += Time.deltaTime;
		if (currentTimer >= maxTimer)
		{
			orbGenerator.resetOrbs();
			ShipChromosomeNode n = generateRandomShipChromosome();
			//Debug.Log(n.getString());
			generatePhysicalShip(n);
			currentTimer = 0;
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
