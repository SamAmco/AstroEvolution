using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour 
{
	public Vector3 getForward()
	{
		return transform.rotation * new Vector3(0, 1f, 0);
	}
	public Vector3 getTarget()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
