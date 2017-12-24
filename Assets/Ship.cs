using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ship : NetworkBehaviour {
	[SyncVar]
	public Faction owner;
	public ShipSO prototype;
	public float health;
	public float shield;
	public int secondaryAmount;
	public int lastShot;
	public int lastSecondary;
	public float desiredAngle;
	[Space]
	public SpriteRenderer detail;
	public SpriteRenderer overlay;
	public SpriteRenderer color;
	public Focusable focus;
	public Rigidbody2D rig;
	public float rotOffset;
	
	public ShipEntity[] entities;
	
	[Command]
	public void CmdInit(Faction faction,string source) {
		owner = faction;
		ShipSO p_ = ShipSO.shipTypes[source];
		health = p_.health;
		shield = 0;
		secondaryAmount = p_.maxSecondary;
		RpcInit(faction,source);
	}
	
	void Update() {
		if (isServer) {
			focus.secondaryAmount = secondaryAmount;
			if (prototype != null) {
				focus.secondaryCooldown = (Time.time * 1000 - (float)lastSecondary) / (float)prototype.secondaryCooldown;
				shield += prototype.shieldRecharge * (Time.deltaTime * 1000);
				if (shield > prototype.shield) shield = (float)prototype.shield;
				focus.health = health / (float)prototype.health;
				focus.shield = shield / (float)prototype.shield;
				
				float angle = transform.rotation.eulerAngles.z / 180 * Mathf.PI;
				Vector2 facing = new Vector2(Mathf.Sin(angle),Mathf.Cos(angle));
				float diff = Vector2.Dot(facing,focus.moveDirection);
				Vector2 apply = Vector2.Lerp(focus.moveDirection * prototype.lateralSpeed,focus.moveDirection * prototype.forwardSpeed,Mathf.Clamp(diff,0,1));
				if (Vector2.Dot(apply.normalized,rig.velocity.normalized) < 0.99) {
					rig.AddForce(rig.velocity.normalized * -Mathf.Min(prototype.slowdownSpeed,rig.velocity.magnitude) * Time.deltaTime * 60);
				}
				if (apply.magnitude != 0) {
					desiredAngle = -Mathf.Atan2(apply.normalized.x,apply.normalized.y) + rotOffset;
					rig.AddForce(apply * Time.deltaTime * 60);
				}
				Vector2 desiredFacing = new Vector2(Mathf.Sin(desiredAngle),Mathf.Cos(desiredAngle));
				Vector3 newFacing = Vector3.Slerp(new Vector3(facing.x,facing.y,0),new Vector3(desiredFacing.x,desiredFacing.y,0),prototype.rotationSpeed / ((Vector2.Dot(facing,desiredFacing) + 1) / 2));
				transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(newFacing.x,newFacing.y) / Mathf.PI * 180));
				
				foreach (ShipEntity entity in entities) {
					entity.SetThrust(apply.normalized);
				}
				
				rig.velocity = new Vector2(
					Mathf.Clamp(rig.velocity.x,-prototype.maxSpeed,prototype.maxSpeed),
					Mathf.Clamp(rig.velocity.y,-prototype.maxSpeed,prototype.maxSpeed)
				);
				
			}
			if (focus.menu) {
				RpcOpenMenu();
				focus.menu = false;
			}
		}
	}
	
	[ClientRpc]
	public void RpcOpenMenu() {
		if (GameManager.instance.player.focused == focus && !GameManager.instance.player.spectating) {
			GameManager.instance.OpenShipMenu(this);
		}
	}
	
	[ClientRpc]
	public void RpcInit(Faction faction,string source) {
		prototype = ShipSO.shipTypes[source];
		detail.sprite = prototype.detail;
		overlay.sprite = prototype.overlay;
		color.sprite = prototype.color;
		color.color = GameManager.instance.colorPallete[faction.bgColor];
		focus.canFire = prototype.hasWeapon;
		focus.canSecondary = prototype.secondaryType >= 0;
		
		prototype.SpawnEntities(transform,this);
		entities = GetComponentsInChildren<ShipEntity>();
		this.enabled = true;
	}
}
