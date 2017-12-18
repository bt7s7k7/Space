using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConnectorListEntry : MonoBehaviour {
	public GameConnector parent;
	public string ip;
	public Text uiIp;
	
	public void Init(GameConnector parent_, string ip_) {
		parent = parent_;
		ip = ip_;
		uiIp.text = ip;
	}
	
	public void Connect() {
		parent.Connect(ip);
	}
}
