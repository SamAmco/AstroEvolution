using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulationManager : MonoBehaviour
{
	private List<Generation> generations;
	private List<RandomShipCreator> shipCreators;
	private Generation currentGeneration;
	private OrbGeneratorScript orbGenerator;

	float frameCounter;
	bool evaluationFramePassed = false;

	void Start()
	{
		Time.timeScale = Config.SIMULATION_TIME_SCALE;

		generations = new List<Generation>();
		shipCreators = new List<RandomShipCreator>(GameObject.FindObjectsOfType<RandomShipCreator>());
		frameCounter = 0;
		orbGenerator = GameObject.FindObjectOfType<OrbGeneratorScript>();
		currentGeneration = new Generation();

		foreach (RandomShipCreator r in shipCreators)
		{
			r.generateRandomShip();
		}
		activateGeneration();
		
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
		++frameCounter;
		if (frameCounter >= Config.STANDARD_GENERATION_FRAME_COUNT)
		{
			if (!evaluationFramePassed)
			{
				orbGenerator.resetOrbs();
				
				//EVALUATE SHIPS
				foreach (RandomShipCreator r in shipCreators)
				{
					r.evaluateShip(this);
				}
				evaluationFramePassed = true;
			}
			else
			{
				//PERFORM SELECTION
				List<ShipChromosomeNode> selectionList = currentGeneration.SUS((uint)shipCreators.Count);
				
				//PERFORM CROSSOVER
				List<ShipChromosomeNode> nextGeneration = CrossoverAndMutationManager.TreeCrossover(selectionList);
				
				//PERFORM MUTATION
				CrossoverAndMutationManager.Mutate(nextGeneration);
				
				//RESET THE CURRENT GENERATION AND STORE THE OLD ONE
				generations.Add(currentGeneration);
				currentGeneration = new Generation();
				
				//INITIALIZE THE NEXT GENERATION
				for (int i = 0; i < shipCreators.Count; ++i)
				{
					shipCreators[i].generatePhysicalShip(nextGeneration[i]);
				}
				activateGeneration();
				frameCounter = 0;
				evaluationFramePassed = false;
			}
		}
	}

	private void activateGeneration()
	{
		foreach(RandomShipCreator r in shipCreators)
		{
			r.activate();
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20), frameCounter + "/" + Config.STANDARD_GENERATION_FRAME_COUNT);
	}
}
