using UnityEngine;
using System.Collections;

public class Config
{
	public const float ENGINE_POWER = 20;
	public const float CHANCE_OF_CHILD_NODE = 0.3f;
	public const int MAX_SHIP_DEPTH = 5;
	public const float MAX_CHILD_ROTATION = 1f;
	public const int STANDARD_GENERATION_FRAME_COUNT = 750;
	public const float SIMULATION_TIME_SCALE = 8.0f;
	public const float FUEL_COST = 0f;
	public const float MUTATION_PROBABILITY = 0;//0.02f;
	public const float STILLNESS_COST =	1000000f;

	public const string ENGINE_PREFAB_LOCATION = "Prefabs/EngineBlock";
	public const string HEAVY_BLOCK_PREFAB_LOCATION = "Prefabs/HeavyBlock";
}
