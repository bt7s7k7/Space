using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkHelper : MonoBehaviour {
	public NetworkDiscovery discovery;
	public bool discoveryInit = false;
	public int discoveryResults = 0;
	
	void Start() {
		RestartDiscovery();
		BDebug.instance.debugCallback += DebugCall;
	}
	
	public void RestartDiscovery() {
		if (discovery.running) discovery.StopBroadcast();
		discoveryInit = discovery.Initialize();
	}
	
	public List<NetworkBroadcastResult> GetDiscoveryResults() {
		List<NetworkBroadcastResult> ret = new List<NetworkBroadcastResult>();
		if (discoveryInit) {
			discoveryResults = 0;
			foreach (KeyValuePair<string,NetworkBroadcastResult> kv in discovery.broadcastsReceived) {
				ret.Add(kv.Value);
				discoveryResults += 1;
			}
		}
		return ret;
	}
	
	void DebugCall() {
		BDebug.Write("NetDiscovery:" + ((discoveryInit) ? ((discovery.isServer) ? "Server" : ((discovery.isClient) ? "Client Found: " + discoveryResults : "Ready")) : "Offline"));
	}
	
	public void Connect(string ip) {
		RestartDiscovery();
		NetworkManager.singleton.networkAddress = ip;
		NetworkManager.singleton.StartClient();
	}
	
	public void Host() {
		RestartDiscovery();
		if (discoveryInit) discovery.StartAsServer();
		NetworkManager.singleton.StartHost();
	}
	
	public void Disconnect() {
		RestartDiscovery();
		NetworkManager.singleton.StopHost();
	}
}
