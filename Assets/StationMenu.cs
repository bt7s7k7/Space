using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationMenu : MonoBehaviour {
	public Station station;
	public Text stationName;
	public Transform myShipsList;
	public Transform shipMarketList;
	public Transform materialMarketList;
	public GameObject listEntryPrefab;
	public UIBGAnimation anim;
	public Text subMenuHeader;
	public Text subMenuButtonText;
	public int subMenuId;
	public string[] subMenuNames = {
		"My Ships",
		"Sell Ships",
		"Buy Ships",
		"Material Market",
		"My Cargo"
	};
	public string[] subMenuButtons = {
		"Select",
		"Sell",
		"Buy",
		"Buy",
		"Sell"
	};
	public string selected;
	public Text subMenuSelInfo;
	
	void OnEnable() {
		B.DestroyChildren(myShipsList);
		foreach (string ship in GameManager.GetPlayerStats().shipsOwned) {
			Instantiate(listEntryPrefab,myShipsList.position,myShipsList.rotation,myShipsList).GetComponent<MyShipListEntry>().Init(this,ship);
		}
	}
	
	void Update() {
		stationName.text = station.label;
		subMenuSelInfo.text = "";
		if (selected != "") {
			if (subMenuId == 0) {
				subMenuSelInfo.text = ShipSO.shipTypes[selected].GetInfo();
			}
		}
	}
	
	public void SetHome() {
		PlayerPrefs.SetString("playerHome",station.label);
	}
	
	public void Select(string entry) {
		selected = entry;
	}
	
	public void SubmenuAction() {
		if (selected != "") {
			if (subMenuId == 0) {
				GameManager.PlayerStats stats = GameManager.GetPlayerStats();
				string[] newShips = new string[stats.shipsOwned.Length - 1];
				bool removed = false;
				int i = 0;
				foreach (string ship in stats.shipsOwned) {
					if (removed || ship != selected) {
						newShips[i] = ship;
						i++;
					} else {
						removed = true;
					}
				}
				stats.shipsOwned = newShips;
				GameManager.SetPlayerStats(stats);
				GameManager.instance.player.focused = ShipSO.shipTypes[selected].Spawn(GameManager.instance.player.faction,station.shipSpawnpoint.position).GetComponent<Focusable>();
				GameManager.instance.UIBG.GoToScreen(6);
			}
		}
	}
	
	public void Back() {
		subMenuId = -1;
		anim.GoToScreen(0);
		selected = "";
	}
	
	public void GotoSubmenu(int id) {
		subMenuId = id;
		subMenuHeader.text = subMenuNames[subMenuId];
		subMenuButtonText.text = subMenuButtons[subMenuId];
		B.DestroyChildren(myShipsList);
		if (subMenuId == 0) {
			foreach (string ship in GameManager.GetPlayerStats().shipsOwned) {
				Instantiate(listEntryPrefab,myShipsList.position,myShipsList.rotation,myShipsList).GetComponent<MyShipListEntry>().Init(this,ship);
			}
		}
		anim.GoToScreen(1);
	}
}
