using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public StarMap starMap;
	public StarMap.Sector currSector;
	
	void Reset() {
		
	}
	
	void Awake() {
		instance = this;
	}
	
	void Start() {
		
	}
	
	void Update() {
		
	}
	
	void FixedUpdate() {
		
	}
	
}
