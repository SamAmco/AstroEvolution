using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomShipCreator : MonoBehaviour 
{ 
	public OrbGeneratorScript orbGenerator;
	public bool guiEnabled = false;
	private ShipController lastShip;
	
	float currentTimer;

	void Start()
	{
		currentTimer = Config.STANDARD_GENERATION_TIME_LIMIT;
	}

	void OnGUI()
	{
		if (guiEnabled)
		{
			if (GUI.Button(new Rect(0,0, 200, 50), "GENERATE RANDOM SHIP"))
			{
				ShipChromosomeNode n = generateRandomShipChromosome();
				//Debug.Log(n.getString());
				generatePhysicalShip(n);
			}
			if (GUI.Button(new Rect(200,0, 200, 50), "GENERATE BEST SHIP"))
			{
				PopulationManager.shipArchives.Sort();
				for (int i = 0; i < PopulationManager.shipArchives.Count; ++i)
					Debug.Log(PopulationManager.shipArchives[i].fitness);
				//Debug.Log(n.getString());
				generatePhysicalShip(PopulationManager.shipArchives[0].root);
			}
		}
	}

	void Update()
	{
		currentTimer += Time.deltaTime;
		if (currentTimer >= Config.STANDARD_GENERATION_TIME_LIMIT)
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
			PopulationManager.shipArchives.Add(shipArchive);

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
