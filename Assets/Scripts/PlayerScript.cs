using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerNum
{
	PLAYER1,
	PLAYER2,
	PLAYER3,
	PLAYER4
}

public class PlayerScript : MonoBehaviour 
{
	public PlayerNum playerNum;

	PlayerManager playerManager;
	List<GameObject> disabledCollectables;
	float immuneTime = 0;
	bool immune = false;

	// Use this for initialization
	void Start () 
	{
		playerManager = GameObject.FindObjectOfType<PlayerManager>();
		disabledCollectables = new List<GameObject>();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!immune && collision.gameObject.layer == LayerMask.NameToLayer("Blocks"))
		{
			playerManager.playerHit();
			immuneTime = Config.PLAYER_HIT_IMMUNE_TIME;
			immune = true;
			renderer.material.color = Color.blue;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Collectable")
		{
			disabledCollectables.Add(other.gameObject);
			other.gameObject.SetActive(false);
			playerManager.collectedCollectable();
		}
	}

	public void nextGeneration()
	{
		foreach(GameObject g in disabledCollectables)
		{
			g.SetActive(true);
		}
		disabledCollectables.Clear();
	}

	// Update is called once per frame
	void Update () 
	{
		if (immune)
		{
			immuneTime -= Time.deltaTime;
			if (immuneTime < 0)
			{
				immune = false;
				renderer.material.color = Color.white;
			}
		}
		Vector3 normIn = new Vector3(Input.GetAxis("Horizontal" + ((int)playerNum + 1)),
		                             Input.GetAxis("Vertical" + ((int)playerNum + 1)), 0).normalized;
		rigidbody.velocity = normIn * Config.PLAYER_SPEED;
	}
}
