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


	public List<ShipArchive> shipArchives;
	private List<RandomShipCreator> shipCreators;
	private OrbGeneratorScript orbGenerator;

	float currentTimer;


	void Start()
	{
		shipArchives = new List<ShipArchive>();
		shipCreators = new List<RandomShipCreator>(GameObject.FindObjectsOfType<RandomShipCreator>());
		currentTimer = Config.STANDARD_GENERATION_TIME_LIMIT;
		orbGenerator = GameObject.FindObjectOfType<OrbGeneratorScript>();
	}

	void Update()
	{
		currentTimer += Time.deltaTime;
		if (currentTimer >= Config.STANDARD_GENERATION_TIME_LIMIT)
		{
			orbGenerator.resetOrbs();
			foreach (RandomShipCreator r in shipCreators)
			{
				r.generateRandomShip();
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
