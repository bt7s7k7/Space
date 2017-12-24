using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsDisplay : MonoBehaviour {
	public Text display;
	public bool multiline = true;
	public bool showMoney = true;
	public bool showLevel = true;
	
	void Update() {
		string show = "";
		GameManager.PlayerStats stats = GameManager.GetPlayerStats();
		if (showMoney) {
			show += stats.money + "₪";
		}
		if (showLevel) {
			if (multiline) show += "\n";
			show += "Level " + stats.level;
		}
		display.text = show;
	}
	
	
}
