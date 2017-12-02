using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMap : MonoBehaviour {
	public Sector[] sectors;
	
	public class Sector {
		string name;
		int levelRequired;
		// FactionManager.Faction owner;
		Transform entryPoint;
		Color turbolentColor = Color.white;
		Color smoothColor = Color.white;
	}
	
}
