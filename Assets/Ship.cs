using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ship : NetworkBehaviour {
	[SyncVar]
	public Faction owner;
	public ShipSO prototype;
	[SyncVar]
	public int hull;
	[SyncVar]
	public int shields;
	[SyncVar]
	public int secondaryAmount;
	[SyncVar]
	public int lastShot;
	[SyncVar]
	public int lastSecondary;
	
	public SpriteRenderer detail;
	public SpriteRenderer overlay;
	public SpriteRenderer color;
	public Focusable focus;
	
	[Command]
	public void CmdInit(Faction faction,string source) {
		Debug.Log("Server init");
		owner = faction;
		ShipSO p_ = ShipSO.shipTypes[source];
		hull = p_.health;
		shields = 0;
		secondaryAmount = p_.maxSecondary;
		RpcInit(faction,source);
	}
	
	void Update() {
		focus.secondaryAmount = secondaryAmount;
	}
	
	[ClientRpc]
	public void RpcInit(Faction faction,string source) {
		Debug.Log("ClientInit");
		prototype = ShipSO.shipTypes[source];
		detail.sprite = prototype.detail;
		overlay.sprite = prototype.overlay;
		color.sprite = prototype.color;
		color.color = GameManager.instance.colorPallete[faction.bgColor];
		focus.canFire = prototype.hasWeapon;
		focus.canSecondary = prototype.secondaryType >= 0;
	}
}
