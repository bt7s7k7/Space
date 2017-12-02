using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BEditorScripter : MonoBehaviour {
	public UnityEvent onAwake;
	public UnityEvent onStart;
	public UnityEvent onUpdate;
	public UnityEvent onFixedUpdate;
	public UnityEvent onRelay;
	
	void Awake() {
		onAwake.Invoke();
	}
	
	void Start() {
		onStart.Invoke();
	}
	
	void Update() {
		onUpdate.Invoke();
	}
	
	void FixedUpdate() {
		onFixedUpdate.Invoke();
	}
	
	public void Relay() {
		onRelay.Invoke();
	}
	
	public void Log(string text) {
		Debug.Log(text);
	}
}
