using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Targetable : NetworkBehaviour {
	public string label;
	public Faction owner;
	public enum Type {Ship,Station,Planet,Star,Asteroid,Unknown}
	public Type type;
	
	public static List<Targetable> list = new List<Targetable>();
	
	void OnEnable() {
		list.Add(this);
	}
	
	void OnDisable() {
		list.Remove(this);
	}
}
