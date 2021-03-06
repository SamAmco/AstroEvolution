﻿using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour 
{
	public ShipController shipController;

	void Update()
	{
		shipController.addToCumulativeDistance((shipController.getTarget() - transform.position).sqrMagnitude);
	}

	public void initialize(ShipChromosomeNode n, ShipController s)
	{
		shipController = s;
		s.limbCount++;

		if (n.top != null)
			createChild(n.top, ChildNode.BOTTOM, s);
		if (n.bottom != null)
			createChild(n.bottom, ChildNode.TOP, s);
		if (n.left != null)
			createChild(n.left, ChildNode.RIGHT, s);
		if (n.right != null)
			createChild(n.right, ChildNode.LEFT, s);
	}

	public void combineSubMeshes()
	{
		MeshCollider[] meshColliders = GetComponentsInChildren<MeshCollider>();
		CombineInstance[] combine = new CombineInstance[meshColliders.Length];
		int i = 0;
		while (i < meshColliders.Length) 
		{
			combine[i].mesh = meshColliders[i].sharedMesh;
			//Vector3 tempScale = meshColliders[i].transform.localScale;
			//meshColliders[i].transform.localScale = new Vector3(1,1,1);
			combine[i].transform = meshColliders[i].transform.localToWorldMatrix;
			//meshFilters[i].transform.localScale = tempScale;
			meshColliders[i].GetComponent<MeshCollider>().enabled = false;
			++i;
		}
		transform.GetComponent<MeshCollider>().sharedMesh = new Mesh();
		transform.GetComponent<MeshCollider>().sharedMesh.CombineMeshes(combine);
		transform.GetComponent<MeshCollider>().enabled = true;
		transform.gameObject.SetActive(true);
	}
	
	void CollectedOrb()
	{
		shipController.collectedOrb();
	}
	
	//c indicates where the child's parent is in relation to it
	private void createChild(ShipChromosomeNode n, ChildNode c, ShipController s)
	{
		GameObject g = (GameObject)GameObject.Instantiate(
			Resources.Load(n.isEngine ? Config.ENGINE_PREFAB_LOCATION : Config.HEAVY_BLOCK_PREFAB_LOCATION),
		    Vector3.zero,
		    Quaternion.identity);

		if (n.isEngine)
		{
			EngineScript engine = g.GetComponent<EngineScript>();
			engine.initialize(s, n.startEngageAngle, n.rangeEngageAngle);
		}

		g.transform.parent = transform;
		g.transform.localPosition = Vector3.zero;
		g.transform.localRotation = Quaternion.Euler(0, 0, n.relativeRotation);


		Vector3 colliderSize = g.GetComponent<MeshCollider>().bounds.size;

		switch (c)
		{
		case ChildNode.TOP :
			g.transform.localPosition = g.transform.localPosition 
				+ rotateByThisRotation(new Vector3(0, -colliderSize.y, 0));
			break;
		case ChildNode.BOTTOM :
			g.transform.localPosition = g.transform.localPosition 
				+ rotateByThisRotation(new Vector3(0, colliderSize.y, 0));
			break;
		case ChildNode.LEFT :
			g.transform.localPosition = g.transform.localPosition 
				+ rotateByThisRotation(new Vector3(colliderSize.x, 0, 0));
			break;
		case ChildNode.RIGHT :
			g.transform.localPosition = g.transform.localPosition 
				+ rotateByThisRotation(new Vector3(-colliderSize.x, 0, 0));
			break;
		}

		if (Physics.OverlapSphere(g.transform.position, 
		                        (colliderSize.y < colliderSize.x 
		                        ? colliderSize.y : colliderSize.x) / 2.1f).Length > 1)
		{
			GameObject.Destroy(g);
			return;
		}

		//FixedJoint fixedJoint = g.AddComponent<FixedJoint>();
		//fixedJoint.enableCollision = false;
		//fixedJoint.connectedBody = rigidbody;


		BlockScript b = g.GetComponent<BlockScript>();
		
		b.initialize(n, s);
	}


	private Vector3 rotateByThisRotation(Vector3 v)
	{
		return v;//transform.rotation * v;
	}
}






















