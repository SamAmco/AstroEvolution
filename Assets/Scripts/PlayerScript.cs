using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 normIn = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		rigidbody.velocity = normIn * Config.PLAYER_SPEED;
	}
}
