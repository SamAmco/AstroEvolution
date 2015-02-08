﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CrossoverAndMutationManager
{
	private static System.Random rnd = new System.Random();

	public static List<ShipChromosomeNode> TreeCrossover(List<ShipArchive> selectionList)
	{
		List<ShipChromosomeNode> outputPopulation = new List<ShipChromosomeNode>();
		//string debugOutput = "";

		while (selectionList.Count >= 2)
		{
			//SELECT TWO PARENTS AT RANDOM
			ShipChromosomeNode p1 = selectRandomElement(selectionList, true).root;
			ShipChromosomeNode p2 = selectRandomElement(selectionList, true).root;

			//RETRIEVE A LIST OF THEIR NODES
			List<ShipChromosomeNode> p1Nodes = p1.getListOfNodes();
			List<ShipChromosomeNode> p2Nodes = p2.getListOfNodes();

			//SELECT ONE AT RANDOM FROM EACH PARENT
			ShipChromosomeNode p1CutNode = selectRandomElement(p1Nodes, false);
			ShipChromosomeNode p2CutNode = selectRandomElement(p2Nodes, false);
			//debugOutput += "P1 cut point: \n" + p1CutNode.getString() + "\n";
			//debugOutput += "P2 cut point: \n" + p2CutNode.getString() + "\n";

			//ADD THE OFFSPRING CREATED TO THE OUTPUT POPULATION
			outputPopulation.Add(createOffspring(p1, p1CutNode, p2CutNode));
			outputPopulation.Add(createOffspring(p2, p2CutNode, p1CutNode));
		}

		//Debug.Log(debugOutput);
		return outputPopulation;
	}

	private static ShipChromosomeNode createOffspring(ShipChromosomeNode p1,
	                                                  ShipChromosomeNode p1CutNode,
	                                                  ShipChromosomeNode p2CutNode)
	{
		return createOffspring(p1, p1CutNode, p2CutNode, 0, null, ChildNode.NONE);
	}

	private static ShipChromosomeNode createOffspring(ShipChromosomeNode p1,
	                                                  ShipChromosomeNode p1CutNode,
	                                                  ShipChromosomeNode p2CutNode, 
	                                                  int depth,
	                                                  ShipChromosomeNode parent,
	                                                  ChildNode parentPos)
	{
		if (p1 == p1CutNode)
			p1 = p2CutNode;

		ShipChromosomeNode o = p1.copyNode();
		o.depth = depth;
		o.parentPos = parentPos;
		switch (parentPos)
		{
		case ChildNode.BOTTOM:
			o.bottom = parent;
			break;
		case ChildNode.LEFT:
			o.left = parent;
			break;
		case ChildNode.RIGHT:
			o.right = parent;
			break;
		case ChildNode.TOP:
			o.top = parent;
			break;
		}

		if (p1.top != null && p1.parentPos != ChildNode.TOP)
			o.top = createOffspring(p1.top, p1CutNode, p2CutNode, depth + 1, o, ChildNode.BOTTOM);
		if (p1.left != null && p1.parentPos != ChildNode.LEFT)
			o.left = createOffspring(p1.left, p1CutNode, p2CutNode, depth + 1, o, ChildNode.RIGHT);
		if (p1.right != null && p1.parentPos != ChildNode.RIGHT)
			o.right = createOffspring(p1.right, p1CutNode, p2CutNode, depth + 1, o, ChildNode.LEFT);
		if (p1.bottom != null && p1.parentPos != ChildNode.BOTTOM)
			o.bottom = createOffspring(p1.bottom, p1CutNode, p2CutNode, depth + 1, o, ChildNode.TOP);

		return o;
	}

	private static T selectRandomElement<T>(List<T> inputList, bool removeFromList)
	{
		int i1 = rnd.Next(inputList.Count);

		T r = inputList[i1];

		if (removeFromList)
			inputList.RemoveAt(i1);

		return r;
	}

}









