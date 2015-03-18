using UnityEngine;
using System.Collections;

public class Config
{
	public const float ENGINE_POWER = 30;//50;
	public const float BLOCK_MASS = 0.2f;//0.3f;
	public const float BLOCK_DRAG = 0.3f;//0.4f;
	public const float BLOCK_ANGULAR_DRAG = 0.5f;//0.4f;
	public const float CHANCE_OF_CHILD_NODE = 0.3f;
	public const int MAX_SHIP_DEPTH = 5;
	public const float MAX_CHILD_ROTATION = 5f;
	public const int STANDARD_GENERATION_TIME = 50;
	public const float SIMULATION_TIME_SCALE = 1.0f;
	public const float FUEL_COST = 0f;
	public const float MUTATION_PROBABILITY = 0.15f;
	public const float STILLNESS_COST =	1000000f;
	public const float BLOAT_MULTIPLIER = 1.01f;
	public const float ORB_MULTIPLIER = 0.25f;//0.45f;
	public const int TIME_TO_DEACTIVATION = 3;

	public const string ENGINE_PREFAB_LOCATION = "Prefabs/EngineBlock";
	public const string HEAVY_BLOCK_PREFAB_LOCATION = "Prefabs/HeavyBlock";
}
