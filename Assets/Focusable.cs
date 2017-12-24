using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Focusable : NetworkBehaviour {
	public bool multipleUsers;
	[SyncVar]
	public bool used;
	public bool canMove;
	public bool canFire;
	public bool canSecondary;
	public bool canPlayerFocus = true;
	public float camSize = 5;
	[SyncVar]
	public Vector2 fireDirection;
	[SyncVar]
	public Vector2 moveDirection;
	[SyncVar]
	public bool fire;
	[SyncVar]
	public bool secondaryFire;
	[SyncVar]
	public bool menu;
	[SyncVar]
	public int secondaryAmount;
	[SyncVar]
	public float secondaryCooldown;
	[SyncVar]
	public float shield;
	[SyncVar]
	public float health;
	
	[Command]
	public void CmdControl(Vector2 moveDir,Vector2 fireDir,bool secondary,bool menu_) {
		menu = menu_;
		moveDirection = moveDir;
		fireDirection = fireDir;
		secondaryFire = secondary || secondaryFire;
	}
}
