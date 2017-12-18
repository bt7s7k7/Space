using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Faction {
	public string label;
	public int bgColor;
	public int iconColor;
	public int iconType;
	public int side;
	public bool playerOwned;
	
	public void InitLocal() {
		label = PlayerPrefs.GetString("playerName","");
		bgColor = PlayerPrefs.GetInt("playerBGColor",0);
		iconColor = PlayerPrefs.GetInt("playerIconColor",0);
		iconType = PlayerPrefs.GetInt("playerIconType",0);
		side = PlayerPrefs.GetInt("playerFactionSide",0);
	}
}
