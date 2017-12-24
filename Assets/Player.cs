using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	public Faction faction;
	public Focusable focused;
	public bool spectating;
	public bool openMenu;
	
	void Start() {
		if (isLocalPlayer) {
			GameManager.instance.player = this;
			GameManager.instance.SetGameMode();
			focused = Station.stations[PlayerPrefs.GetString("homeStation","Null Base")].GetComponent<Focusable>();
			Faction local = new Faction();
			local.InitLocal();
			CmdInit(local);
		}
	}
	
	[Command]
	public void CmdInit(Faction newF) {
		faction = newF;
	}
	
	void Update() {
		if (isLocalPlayer) {
			if (focused != null && PlayerPrefs.GetInt("keyboardMode",0) == 0 && !spectating) {
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
			GameManager.instance.shieldBar.value = (focused != null) ? focused.shield : 0;
			GameManager.instance.shieldBar.gameObject.SetActive(GameManager.instance.shieldBar.value > 0);
			GameManager.instance.healthBar.value = (focused != null) ? focused.health : 0;
			GameManager.instance.healthBar.gameObject.SetActive(GameManager.instance.healthBar.value > 0);
			GameManager.instance.cam.orthographicSize = (GameManager.instance.cam.orthographicSize + ((focused != null) ? focused.camSize : 11)) / 2;
			GameManager.instance.focusMenuButtonText.color = (focused != null && !spectating) ? Color.white : Color.gray;
			if (focused != null) {
				GameManager.instance.secondaryDisp.SetActive(focused.canSecondary);
				GameManager.instance.secondaryAmount.text = focused.secondaryAmount.ToString();
				GameManager.instance.secondaryCooldown.fillAmount = focused.secondaryCooldown;
				GameManager.instance.cam.transform.position = (GameManager.instance.cam.transform.position + (Vector3.Scale(focused.transform.position,new Vector3(1,1,0)) + Vector3.Scale(GameManager.instance.cam.transform.position,new Vector3(0,0,1)))) / 2;
				
				Vector2 movement = (new Vector2(CnInputManager.GetAxis("Horizontal"),CnInputManager.GetAxis("Vertical"))).normalized;
				Vector2 fireDir = (new Vector2(CnInputManager.GetAxis("HorizontalFire"),CnInputManager.GetAxis("VerticalFire"))).normalized;
				
				focused.CmdControl(movement,fireDir,CnInputManager.GetButtonDown("Fire2"),openMenu);
				openMenu = false;
			}
			
			
		}
	}
}
