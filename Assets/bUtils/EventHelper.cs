using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHelper : MonoBehaviour {
	public Graphic target;
	public Color[] colors;
	
	public void SetColor(int id) {
		target.color = colors[id];
	}
	
	public void Quit() {
		Application.Quit();
	}
	
	public void LoadScene(int id) {
		
	}
}
