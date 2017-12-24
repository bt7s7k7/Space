using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BEditorScripter : MonoBehaviour {
	public UnityEvent onAwake;
	public UnityEvent onStart;
	public UnityEvent onUpdate;
	public UnityEvent onFixedUpdate;
	public UnityEvent onDisable;
	public UnityEvent onEnable;
	public UnityEvent onEvent;
	
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
	
	void OnEnable() {
		onEnable.Invoke();
	}
	void OnDisable() {
		onDisable.Invoke();
	}
	
	public void Relay() {
		onEvent.Invoke();
	}
	
	public void Quit() {
		Application.Quit();
	}
	
	public void Log(string text) {
		Debug.Log(text);
	}
	
	public void Wait(float seconds) {
		StartCoroutine(CWait(seconds));
	}
	
	IEnumerator CWait(float seconds) {
		yield return new WaitForSeconds(seconds);
		onEvent.Invoke();
	}
}
