using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulationManager : MonoBehaviour
{
	private List<Generation> generations;
	private List<RandomShipCreator> shipCreators;
	private Generation currentGeneration;
	public GameObject orbs;
	private ShipArchive bestOfAllTime;

	//float generationTimeCounter;
	bool evaluationFramePassed = false;
	bool startNextGeneration = false;
	//string lastFitnessesOutput = "";

	void Start()
	{
		Time.timeScale = Config.SIMULATION_TIME_SCALE;

		generations = new List<Generation>();
		shipCreators = new List<RandomShipCreator>(GetComponentsInChildren<RandomShipCreator>());
		//generationTimeCounter = 0;
		currentGeneration = new Generation();
		bestOfAllTime = new ShipArchive(null, double.MaxValue);

		foreach (RandomShipCreator r in shipCreators)
		{
			r.setOrbsRoot(orbs);
			r.generateRandomShip();
		}
		activateGeneration();
		
	}

	public void shipEvaluated(ShipArchive shipArchive)
	{
		currentGeneration.addShipArchive(shipArchive);
	}

	void Update()
	{
		//generationTimeCounter += Time.deltaTime;
		if (startNextGeneration)//generationTimeCounter >= Config.STANDARD_GENERATION_TIME)
		{
			if (!evaluationFramePassed)
			{
				//EVALUATE SHIPS
				foreach (RandomShipCreator r in shipCreators)
				{
					r.evaluateShip(this);
				}
				evaluationFramePassed = true;
			}
			else
			{
				//ALWAYS KEEP THE BEST OF ALL TIME
				foreach (ShipArchive s in currentGeneration.getShipArchives())
				{
					if (s.fitness < bestOfAllTime.fitness)
						bestOfAllTime = new ShipArchive(s.root.copyTree(), s.fitness);
				}

				//PERFORM SELECTION
				List<ShipChromosomeNode> selectionList = currentGeneration.SUS((uint)shipCreators.Count);
				
				//PERFORM CROSSOVER
				List<ShipChromosomeNode> nextGeneration = 
					CrossoverAndMutationManager.TreeCrossover(selectionList);
				
				//PERFORM MUTATION
				CrossoverAndMutationManager.TreeMutate(nextGeneration);//NodeMutate(nextGeneration);
				
				//RESET THE CURRENT GENERATION AND STORE THE OLD ONE
				generations.Add(currentGeneration);
				currentGeneration = new Generation();
				
				//INITIALIZE THE NEXT GENERATION
				shipCreators[0].generatePhysicalShip(bestOfAllTime.root);
				for (int i = 1; i < shipCreators.Count; ++i)
				{
					shipCreators[i].generatePhysicalShip(nextGeneration[i]);
				}
				activateGeneration();
				//generationTimeCounter = 0;
				evaluationFramePassed = false;
				startNextGeneration = false;
			}
		}
	}

	public ShipChromosomeNode getBestOfAllTime()
	{
		if (bestOfAllTime.root == null)
			return null;

		return bestOfAllTime.root;
	}

	public void initializeNextGeneration()
	{
		startNextGeneration = true;
	}

	private void activateGeneration()
	{
		foreach(RandomShipCreator r in shipCreators)
		{
			r.activate();
		}
	}
}
