using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BDebug : MonoBehaviour {
	public UnityEvent debugCall;
	public bool autodebug = true;
	public bool draw = false;
	public List<string> lines = new List<string>();
	public delegate void DebugCall();
	public DebugCall debugCallback;
	
	static public BDebug instance;
	
	void Awake() {
		instance = this;
	}
	
	void Update() {
		if (Input.GetKeyDown("f3")) {
			draw = !draw;
		}
	}
	
	void OnGUI() {
		if (draw) {
			if (ProjectData.instance != null) {
				lines.Add(ProjectData.instance.localizedName + " " + ProjectData.instance.version);
				lines.Add("");
			}
			
			if (autodebug) {
				if (UnityEngine.Networking.NetworkManager.singleton != null) {
					lines.Add("Address:" + UnityEngine.Networking.NetworkManager.singleton.networkAddress + 
					" Players:" + UnityEngine.Networking.NetworkManager.singleton.numPlayers);
				}
				if (BNetwork.instance != null) {
					lines.Add("Listening:" + BNetwork.instance.listening + " Connected:" + BNetwork.instance.connected);
				}
			}
			debugCallback();
			debugCall.Invoke();
			GUILayout.BeginVertical("box");
			foreach (string line in lines) {
				GUILayout.Label(line);
			}
			GUILayout.EndVertical();
			}
		lines.Clear();
	}
	
	public void SetDraw(bool newDraw) {
		draw = newDraw;
	}
	
	public static void Write(string text) {
		instance.lines.Add(text);
	}
}
