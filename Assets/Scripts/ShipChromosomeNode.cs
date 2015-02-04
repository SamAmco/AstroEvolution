﻿using UnityEngine;
using System.Collections;

public enum ChildNode
{
	NONE,
	TOP,
	BOTTOM,
	LEFT,
	RIGHT
}

public class ShipChromosomeNode 
{
	public ShipChromosomeNode top;
	public ShipChromosomeNode bottom;
	public ShipChromosomeNode left;
	public ShipChromosomeNode right;

	public bool isEngine;

	public float relativeRotation;
	public float startEngageAngle;
	public float rangeEngageAngle;

	public ChildNode parentPos;

	int depth;

	public string getString()
	{
		string r = "";

		for (int i = 0; i < depth; i++)
			r += "\t";

		r += "isEngine:" + isEngine + ", relativeRotation:" 
			+ relativeRotation + ", ParentNode:" + parentPos.ToString() 
			+ ", startEngageAngle:" + startEngageAngle + ", rangeEngageAngle"
			+ rangeEngageAngle + "\n";

		if (parentPos != ChildNode.TOP && top != null)
			r += top.getString();
		if (parentPos != ChildNode.BOTTOM && bottom != null)
			r += bottom.getString();
		if (parentPos != ChildNode.LEFT && left != null)
			r += left.getString();
		if (parentPos != ChildNode.RIGHT && right != null)
			r += right.getString();

		return r;
	}

	public static ShipChromosomeNode generateRandomShip()
	{
		return generateRandomShip(0, ChildNode.NONE);
	}

	//c indicates where the child's parent is in relation to it
	public static ShipChromosomeNode generateRandomShip(int d, ChildNode c)
	{
		ShipChromosomeNode root = new ShipChromosomeNode();
		root.depth = d;
		root.parentPos = c;

		root.isEngine = Random.Range(0, 2) == 1;
		root.startEngageAngle = Random.Range(0, 360);
		root.rangeEngageAngle = Random.Range(0, 360);
		root.relativeRotation = Random.Range(-Config.MAX_CHILD_ROTATION, Config.MAX_CHILD_ROTATION);


		float f;

		if (root.depth < Config.MAX_SHIP_DEPTH)
		{
			//top
			if (c != ChildNode.TOP)
			{
				f = Random.Range(0.0f, 1.0f);
				if (f <= Config.CHANCE_OF_CHILD_NODE)
				{
					root.top = ShipChromosomeNode.generateRandomShip(root.depth + 1, ChildNode.BOTTOM);
				}
			}

			//bottom
			if (c != ChildNode.BOTTOM)
			{
				f = Random.Range(0.0f, 1.0f);
				if (f <= Config.CHANCE_OF_CHILD_NODE)
				{
					root.bottom = ShipChromosomeNode.generateRandomShip(root.depth + 1, ChildNode.TOP);
				}
			}

			//left
			if (c != ChildNode.LEFT)
			{
				f = Random.Range(0.0f, 1.0f);
				if (f <= Config.CHANCE_OF_CHILD_NODE)
				{
					root.left = ShipChromosomeNode.generateRandomShip(root.depth + 1, ChildNode.RIGHT);
				}
			}

			//right
			if (c != ChildNode.RIGHT)
			{
				f = Random.Range(0.0f, 1.0f);
				if (f <= Config.CHANCE_OF_CHILD_NODE)
				{
					root.right = ShipChromosomeNode.generateRandomShip(root.depth + 1, ChildNode.LEFT);
				}
			}
		}

		return root;
	}
}