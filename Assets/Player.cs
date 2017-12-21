using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	public Faction faction;
	public Focusable focused;
	
	void Start() {
		if (isLocalPlayer) {
			GameManager.instance.player = this;
			faction.InitLocal();
			GameManager.instance.SetGameMode();
		}
	}
	
	void Update() {
		if (isLocalPlayer) {
			if (focused != null && PlayerPrefs.GetInt("keyboardMode",0) == 0) {
				GameManager.instance.movementJoystick.SetActive(focused.canMove);
				GameManager.instance.fireJoystick.SetActive(focused.canFire);
				GameManager.instance.secondaryDisp.SetActive(focused.canSecondary);
				GameManager.instance.secondaryAmount.text = focused.secondaryAmount.ToString();
				GameManager.instance.secondaryCooldown.fillAmount = focused.secondaryCooldown;
			} else {
				GameManager.instance.movementJoystick.SetActive(false);
				GameManager.instance.fireJoystick.SetActive(false);
				GameManager.instance.secondaryDisp.SetActive(false);
			}
		}
	}
}
