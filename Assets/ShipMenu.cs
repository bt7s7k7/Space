using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMenu : MonoBehaviour {
	public Ship ship;
	public Text health;
	public Text shield;
	public Text secondary;
	public Text info;
	
	void Update() {
		if (ship != null) {
			health.text = "Health: " + ship.health + "/" + ship.prototype.health;
			shield.text = "Shield: " + ship.shield + "/" + ship.prototype.shield;
			secondary.text = "Secondary: " + ship.secondaryAmount + "/" + ship.prototype.maxSecondary;
			info.text = ship.prototype.GetInfo();
		}
	}
}
