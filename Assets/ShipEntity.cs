using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEntity : MonoBehaviour {
	public ParticleSystem particles;
	public SpriteRenderer logo;
	public ShipSO.ShipEntityPrototype.Type type;
	public Ship parent;
	public float baseEmisMul;
	
	public void Start() {
		if (type == ShipSO.ShipEntityPrototype.Type.Logo) {
			logo.sprite = GameManager.instance.playerIcons[parent.owner.iconType];
			logo.color = GameManager.instance.colorPallete[parent.owner.iconColor];
		}
		if ((int)type <= 3) {
			baseEmisMul = particles.emission.rateOverTimeMultiplier;
		}
	}
	
	public void SetThrust(Vector2 dir) {
		if ((int)type <= 3) {
			if (dir.magnitude != 0) {
				particles.Play();
				Vector2 myDir = Vector2.zero;
				if (type == ShipSO.ShipEntityPrototype.Type.ThrusterForward) {
					myDir = (transform.TransformPoint(new Vector2(0,1)) - transform.position).normalized;
				}
				if (type == ShipSO.ShipEntityPrototype.Type.ThrusterBack) {
					myDir = (transform.TransformPoint(new Vector2(0,-1)) - transform.position).normalized;
				}
				if (type == ShipSO.ShipEntityPrototype.Type.ThrusterRight) {
					myDir = (transform.TransformPoint(new Vector2(1,0)) - transform.position).normalized;
				}
				if (type == ShipSO.ShipEntityPrototype.Type.ThrusterLeft) {
					myDir = (transform.TransformPoint(new Vector2(-1,0)) - transform.position).normalized;
				}
				var emis = particles.emission;
				emis.rateOverTimeMultiplier = Mathf.Clamp(Vector2.Dot(dir,myDir) * 2 + 0.1f,0,2) * baseEmisMul;
			} else {
				particles.Stop();
			}
		}
	}
}
