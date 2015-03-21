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
	public Transform playerOrbsRoot;

	private PlayerManager playerManager;
	private List<GameObject> collectables;
	private float immuneTime = 0;
	private bool immune = false;

	private Color defaultColor;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
	{
		playerManager = GameObject.FindObjectOfType<PlayerManager>();
		collectables = new List<GameObject>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultColor = spriteRenderer.color;
		foreach (Transform t in playerOrbsRoot)
		{
			if (t.tag == "Collectable")
				collectables.Add(t.gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!immune && collision.gameObject.layer == LayerMask.NameToLayer("Blocks"))
		{
			playerManager.playerHit();
			immuneTime = Config.PLAYER_HIT_IMMUNE_TIME;
			immune = true;
			spriteRenderer.color = Color.blue;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Collectable" && other.transform.parent == playerOrbsRoot)
		{
			other.gameObject.SetActive(false);
			playerManager.collectedCollectable();
		}
	}

	public void nextGeneration()
	{
		foreach(GameObject g in collectables)
		{
			g.SetActive(true);
		}
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
				spriteRenderer.color = defaultColor;
			}
		}
		Vector3 normIn = new Vector3(Input.GetAxis("Horizontal" + ((int)playerNum + 1)),
		                             Input.GetAxis("Vertical" + ((int)playerNum + 1)), 0);

		if (playerNum == PlayerNum.PLAYER1 && normIn.sqrMagnitude < 0.1f)
		{
			normIn = new Vector3(Input.GetAxis("HorizontalKeys"),
			                     Input.GetAxis("VerticalKeys"), 0);
		}
		normIn.Normalize();
		rigidbody.velocity = normIn * Config.PLAYER_SPEED;
	}
}
