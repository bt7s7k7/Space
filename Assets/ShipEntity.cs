using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEntity : MonoBehaviour {
	public ParticleSystem particles;
	public SpriteRenderer logo;
	public ShipSO.ShipEntityPrototype.Type type;
	public Ship parent;
	
	public void Start() {
		if (type == ShipSO.ShipEntityPrototype.Type.Logo) {
			logo.sprite = GameManager.instance.playerIcons[parent.owner.iconType];
			logo.color = GameManager.instance.colorPallete[parent.owner.iconColor];
		}
	}
}
