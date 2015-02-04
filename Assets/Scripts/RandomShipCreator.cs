using UnityEngine;
using System.Collections;

public class RandomShipCreator : MonoBehaviour 
{
	private GameObject lastShip;

	void OnGUI()
	{
		if (GUI.Button(new Rect(0,0, 200, 50), "GENERATE RANDOM SHIP"))
		{
			ShipChromosomeNode n = generateRandomShipDNA();
			Debug.Log(n.getString());
			generatePhysicalShip(n);
		}
	}

	private ShipChromosomeNode generateRandomShipDNA()
	{
		return ShipChromosomeNode.generateRandomShip();
	}

	private void generatePhysicalShip(ShipChromosomeNode root)
	{
		if (lastShip != null)
			GameObject.Destroy(lastShip);

		GameObject g = (GameObject)GameObject.Instantiate(Resources.Load(Config.HEAVY_BLOCK_PREFAB_LOCATION),
		                       Vector3.zero,
		                       Quaternion.identity);

		lastShip = g;

		BlockScript b = g.GetComponent<BlockScript>();
		ShipController s = g.AddComponent<ShipController>();

		b.initialize(root, s);
	}
}
