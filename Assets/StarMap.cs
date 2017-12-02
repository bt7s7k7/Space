using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMap : MonoBehaviour {
	public Sector[] sectors;
	public int currSector;
	public static StarMap instance;
	
	void Awake() {
		instance = this;
	}
	
	public static Sector GetCurrSector() {
		return instance.sectors[instance.currSector];
	}
	
	[System.Serializable]
	public class Sector {
		public string name;
		public int levelRequired;
		// FactionManager.Faction owner;
		public Transform entryPoint;
		public Color turbolentColor = Color.white;
		public Color smoothColor = Color.white;
		public GameObject gObject;
	}
	
}
