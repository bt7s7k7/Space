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
	public bool canPlayerControll = true;
	[SyncVar]
	public Vector3 fireDirection;
	[SyncVar]
	public Vector3 moveDirection;
	[SyncVar]
	public bool fire;
	[SyncVar]
	public bool secondaryFire;
	[SyncVar]
	public bool menu;
	[SyncVar]
	public int secondaryAmount;
}
