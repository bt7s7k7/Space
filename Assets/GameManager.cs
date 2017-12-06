using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	[Header("Settings")]
	public bool animateBG;
	public Color[] colorPallete;
	public Sprite[] playerIcons;
	[Header("References")]
	public Material turbolentMat;
	public Material smoothMat;
	public Camera cam;
	
	void Reset() {
		
	}
	
	void Awake() {
		instance = this;
	}
	
	void Start() {
		
	}
	
	void Update() {
		UpdateBGColors();
		if (animateBG) {
			cam.transform.position += Vector3.right * Time.deltaTime * 5;
		}
	}
	
	public void UpdateBGColors() {
		turbolentMat.color = StarMap.GetCurrSector().turbolentColor;
		smoothMat.color = StarMap.GetCurrSector().smoothColor;
	}
	
	void FixedUpdate() {
		
	}
	
	#if UNITY_EDITOR
	[CustomEditor(typeof(GameManager))]
	class GameManagerEditor : Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			GameManager gm = (GameManager)target;
			if (GUILayout.Button("Update BG Colors")) {
				gm.UpdateBGColors();
			}
		}
	}
	#endif

	
}
