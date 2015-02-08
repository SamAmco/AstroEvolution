using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public int depth;

	/*public int limbCount()
	{
		int r = 1 + (top == null ? 0 : (parentPos == ChildNode.TOP ? 0 : top.limbCount()))
			+ (bottom == null ? 0 : (parentPos == ChildNode.BOTTOM ? 0 : bottom.limbCount()))
				+ (left == null ? 0 : (parentPos == ChildNode.LEFT ? 0 : left.limbCount()))
				+ (right == null ? 0 : (parentPos == ChildNode.RIGHT ? 0 : right.limbCount()));

		return r;
	}*/

	public ShipChromosomeNode copyNode()
	{
		ShipChromosomeNode s = new ShipChromosomeNode();
		s.isEngine = isEngine;
		s.relativeRotation = relativeRotation;
		s.startEngageAngle = startEngageAngle;
		s.rangeEngageAngle = rangeEngageAngle;
		s.parentPos = parentPos;
		s.depth = depth;

		return s;
	}

	public List<ShipChromosomeNode> getListOfNodes()
	{
		List<ShipChromosomeNode> allNodes = new List<ShipChromosomeNode>();

		allNodes.Add(this);

		if (top != null && parentPos != ChildNode.TOP)
			allNodes.AddRange(top.getListOfNodes());
		if (bottom != null && parentPos != ChildNode.BOTTOM)
			allNodes.AddRange(bottom.getListOfNodes());
		if (left != null && parentPos != ChildNode.LEFT)
			allNodes.AddRange(left.getListOfNodes());
		if (right != null && parentPos != ChildNode.RIGHT)
			allNodes.AddRange(right.getListOfNodes());

		return allNodes;
	}

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
