using UnityEngine;
using System.Collections;

public class OrbScript : TargetableScript 
{
	void OnTriggerEnter(Collider other)
	{
		other.SendMessage("CollectedOrb");
		GameObject.Destroy(gameObject);
	}
}
