using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour 
{
	PlayerManager playerManager;
	List<GameObject> disabledCollectables;

	// Use this for initialization
	void Start () 
	{
		playerManager = GameObject.FindObjectOfType<PlayerManager>();
		disabledCollectables = new List<GameObject>();
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
		Vector3 normIn = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		rigidbody.velocity = normIn * Config.PLAYER_SPEED;
	}
}
