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
			faction.InitLocal();
			GameManager.instance.SetGameMode();
			GameManager.instance.player = this;
		}
	}
	
	void Update() {
		if (isLocalPlayer) {
			GameManager.instance.movementJoystick.SetActive(focused.canMove);
			GameManager.instance.fireJoystick.SetActive(focused.canFire);
			GameManager.instance.secondaryDisp.SetActive(focused.canSecondary);
			GameManager.instance.secondaryAmount.text = focused.secondaryAmount.ToString();
		}
	}
	
}
