using UnityEngine;
using System.Collections;

public class EngineScript : MonoBehaviour 
{
	public ShipController shipController;
	float startEngageAngle;
	float rangeEngageAngle;

	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void initialize(ShipController shipController,
	                       float startEngageAngle,
	                       float rangeEngageAngle)
	{
		this.shipController = shipController;
		this.startEngageAngle = startEngageAngle;
		this.rangeEngageAngle = rangeEngageAngle;
	}

	// Update is called once per frame
	void Update () 
	{
		spriteRenderer.color = Color.white;
		Vector3 forward = shipController.getForward();
		Vector3 target = shipController.getTarget();

		Vector2 from = new Vector2(forward.x, forward.y);
		Vector2 to = new Vector2(target.x, target.y);

		float angle = Vector2.Angle(from, to);

		if (Vector3.Cross(from, to).z > 0)
			angle = 360f - angle;

		if (angle > startEngageAngle && angle < startEngageAngle + rangeEngageAngle
		    || startEngageAngle + rangeEngageAngle > 360 && angle < (startEngageAngle + rangeEngageAngle) % 360)
		{
			rigidbody.AddForce(transform.rotation 
			                   	* new Vector3(0, Config.ENGINE_POWER, 0),
			                   ForceMode.Force);
			spriteRenderer.color = Color.red;
		}
	}
}
