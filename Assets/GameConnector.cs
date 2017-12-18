using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameConnector : MonoBehaviour {
	public NetworkDiscovery discovery;
	public NetworkHelper helper;
	public Transform list;
	public GameObject listEntryPrefab;
	public GameObject portWarning;
	
	void OnEnable() {
		if (helper.discoveryInit) {
			if (!discovery.isClient && !discovery.isServer) {
				discovery.StartAsClient();
			}
			portWarning.SetActive(false);
		} else {
			portWarning.SetActive(true);
		}
	}
	
	void OnDisable() {
		if (discovery.isClient && !discovery.isServer && helper.discoveryInit) {
			helper.RestartDiscovery();
		}
	}
	
	void Update() {
		B.DestroyChildren(list);
		foreach (NetworkBroadcastResult result in helper.GetDiscoveryResults()) {
			GameObject entry = Instantiate(listEntryPrefab,list.position,list.rotation,list);
			entry.GetComponent<GameConnectorListEntry>().Init(this,result.serverAddress);
		}
	}
	
	public void Connect(string ip) {
		helper.Connect(ip);
	}
	
	public void Host() {
		helper.Host();
	}
	
}
