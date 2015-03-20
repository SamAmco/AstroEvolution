using UnityEngine;
using System.Collections;

public class Config
{
	public const float ENGINE_POWER = 50;//50;
	public const float BLOCK_MASS = 0.1f;//0.3f;
	public const float BLOCK_DRAG = 0.6f;//0.4f;
	public const float BLOCK_ANGULAR_DRAG = 0.5f;//0.4f;
	public const float CHANCE_OF_CHILD_NODE = 0.3f;
	public const int MAX_SHIP_DEPTH = 5;
	public const float MAX_CHILD_ROTATION = 5f;
	public const int STANDARD_GENERATION_TIME = 120;
	public const float SIMULATION_TIME_SCALE = 1.0f;
	public const float FUEL_COST = 0f;
	public const float MUTATION_PROBABILITY = 0.15f;
	public const float STILLNESS_COST =	2000000f;
	public const float SLOW_COST = 100000f;
	public const float ANGULAR_VELOCITY_COST_MULTIPLIER = 1000000;
	public const float BLOAT_MULTIPLIER = 1.3f;
	public const float ORB_MULTIPLIER = 0.25f;//0.45f;
	public const int TIME_TO_DEACTIVATION = 3;

	public const string ENGINE_PREFAB_LOCATION = "Prefabs/EngineBlock";
	public const string HEAVY_BLOCK_PREFAB_LOCATION = "Prefabs/HeavyBlock";

	public const float PLAYER_SPEED = 125f;//250f;
	public const int PLAYERS_START_LIVES = 3;
	public const float PLAYER_HIT_IMMUNE_TIME = 3;
}
