using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulationManager 
{
	private PopulationManager(){}
	private static List<ShipArchive> _shipArchives;
	public static List<ShipArchive> shipArchives
	{
		get
		{
			if (_shipArchives == null)
				_shipArchives = new List<ShipArchive>();

			return _shipArchives;
		}
		set
		{
			shipArchives = value;
		}
	}
}
