using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Station : NetworkBehaviour {
	public string label;
	public TradeItem[] items;
	public Focusable focusable;
	public Transform shipSpawnpoint;
	public Targetable target;
	
	public static Dictionary<string,Station> stations = new Dictionary<string,Station>();
	
	void OnEnable() {
		stations.Add(label,this);
	}
	
	void OnDisable() {
		stations.Remove(label);
	}
	
	void Start() {
		if (isServer) {
			target.label = label;
		}
	}
	
	void Update() {
		if (isServer) {
			if (focusable.menu) {
				RpcOpenMenu();
			}
		}
	}
	
	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) {
			Ship incoming = other.GetComponentInParent<Ship>();
			if (incoming.focus == GameManager.instance.player.focused) {
				var stats = GameManager.GetPlayerStats();
				var list  = new List<string>(stats.shipsOwned);
				list.Add(incoming.prototype.label);
				stats.shipsOwned = list.ToArray();
				GameManager.SetPlayerStats(stats);
				Destroy(incoming.gameObject);
				GameManager.instance.player.focused = focusable;
			}
		}
	}
	
	[ClientRpc]
	public void RpcOpenMenu() {
		if (GameManager.instance.player.focused == focusable && !GameManager.instance.player.spectating) {
			GameManager.instance.OpenStationMenu(this);
		}
	}
}
