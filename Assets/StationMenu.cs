using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationMenu : MonoBehaviour, IList {
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
	public object selected;
	public Text subMenuSelInfo;
	
	void OnEnable() {
		UpdateList();
	}
	
	void Update() {
		stationName.text = station.label;
		subMenuSelInfo.text = "";
		if (selected != null) {
			if (subMenuId == 0) {
				subMenuSelInfo.text = ShipSO.shipTypes[(string)selected].GetInfo();
			}
		}
	}
	
	public void SetHome() {
		PlayerPrefs.SetString("playerHome",station.label);
	}
	
	public void Select(object entry) {
		selected = entry;
	}
	
	public void SubmenuAction() {
		if ((string)selected != "") {
			if (subMenuId == 0) {
				GameManager.PlayerStats stats = GameManager.GetPlayerStats();
				string[] newShips = new string[stats.shipsOwned.Length - 1];
				bool removed = false;
				int i = 0;
				foreach (string ship in stats.shipsOwned) {
					if (removed || ship != (string)selected) {
						newShips[i] = ship;
						i++;
					} else {
						removed = true;
					}
				}
				stats.shipsOwned = newShips;
				GameManager.SetPlayerStats(stats);
				GameManager.instance.player.focused = ShipSO.shipTypes[(string)selected].Spawn(GameManager.instance.player.faction,station.shipSpawnpoint.position).GetComponent<Focusable>();
				GameManager.instance.UIBG.GoToScreen(6);
			}
		}
	}
	
	public void Back() {
		subMenuId = -1;
		anim.GoToScreen(0);
		selected = null;
	}
	
	public void GotoSubmenu(int id) {
		subMenuId = id;
		subMenuHeader.text = subMenuNames[subMenuId];
		subMenuButtonText.text = subMenuButtons[subMenuId];
		UpdateList();
		anim.GoToScreen(1);
	}
	
	public void UpdateList() {
		B.DestroyChildren(myShipsList);
		if (subMenuId == 0) {
			foreach (string ship in GameManager.GetPlayerStats().shipsOwned) {
				Instantiate(listEntryPrefab,myShipsList.position,myShipsList.rotation,myShipsList).GetComponent<ListEntry>().Init(this,ship,ship);
			}
		}
	}
}
