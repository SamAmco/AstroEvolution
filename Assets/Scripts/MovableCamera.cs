using UnityEngine;
using System.Collections;

public class MovableCamera : MonoBehaviour 
{
	PlayerManager playerManager;

	// Use this for initialization
	void Start () 
	{
		playerManager = GameObject.FindObjectOfType<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 newPos = playerManager.getAveragePlayerPos();
		transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

		if (Input.GetButton("Cancel"))
			Application.Quit();
	}
}
