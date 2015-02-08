using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulationManager : MonoBehaviour
{
	private static PopulationManager _instance;
	public static PopulationManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<PopulationManager>();

				if (_instance == null)
				{
					GameObject g = new GameObject();
					_instance = g.AddComponent<PopulationManager>();
				}
			}

			return _instance;
		}
		private set {_instance = value;}
	}


	private List<Generation> generations;
	private List<RandomShipCreator> shipCreators;
	private Generation currentGeneration;
	private OrbGeneratorScript orbGenerator;

	float currentTimer;


	void Start()
	{
		Time.timeScale = Config.SIMULATION_TIME_SCALE;

		generations = new List<Generation>();
		shipCreators = new List<RandomShipCreator>(GameObject.FindObjectsOfType<RandomShipCreator>());
		currentTimer = 0;
		orbGenerator = GameObject.FindObjectOfType<OrbGeneratorScript>();
		currentGeneration = new Generation();

		foreach (RandomShipCreator r in shipCreators)
		{
			r.generateRandomShip();
		}

		/*ShipChromosomeNode s1 = ShipChromosomeNode.generateRandomShip();
		ShipChromosomeNode s2 = ShipChromosomeNode.generateRandomShip();
		Debug.Log(s1.getString());
		Debug.Log(s2.getString());

		List<ShipArchive> tempArchives = new List<ShipArchive>();
		tempArchives.Add(new ShipArchive(s1, 0));
		tempArchives.Add(new ShipArchive(s2, 0));

		List<ShipChromosomeNode> scnList = CrossoverAndMutationManager.TreeCrossover(tempArchives);
		Debug.Log(scnList[0].getString());
		Debug.Log(scnList[1].getString());

		shipCreators[0].generatePhysicalShip(s1);
		shipCreators[1].generatePhysicalShip(s2);
		shipCreators[2].generatePhysicalShip(scnList[0]);
		shipCreators[3].generatePhysicalShip(scnList[1]);*/
		
	}

	public void shipEvaluated(ShipArchive shipArchive)
	{
		currentGeneration.addShipArchive(shipArchive);
	}

	void Update()
	{
		currentTimer += Time.deltaTime;
		if (currentTimer >= Config.STANDARD_GENERATION_TIME_LIMIT)
		{
			orbGenerator.resetOrbs();

			foreach (RandomShipCreator r in shipCreators)
			{
				r.evaluateShip(this);
			}

			List<ShipChromosomeNode> nextGeneration = 
				CrossoverAndMutationManager.TreeCrossover(currentGeneration.SUS((uint)shipCreators.Count));

			generations.Add(currentGeneration);
			currentGeneration = new Generation();

			for (int i = 0; i < shipCreators.Count; ++i)
			{
				shipCreators[i].generatePhysicalShip(nextGeneration[i]);
			}
			currentTimer = 0;
		}
	}

	void OnGUI()
	{
		/*if (guiEnabled)
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
		}*/
	}
}
