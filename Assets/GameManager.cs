using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	[Header("Settings")]
	public Color[] colorPallete;
	public Sprite[] playerIcons;
	public Faction[] factions;
	public GameObject[] shipEntityPrefabs;
	[Header("References")]
	public Material turbolentMat;
	public Material smoothMat;
	public Camera cam;
	public UIBGAnimation UIBG;
	public Player player;
	public GameObject shipPrefab;
	public Toggle keyboardModeToggle;
	public GameObject controlObject;
	public GameObject movementJoystick;
	public GameObject fireJoystick;
	public GameObject secondaryDisp;
	public Image secondaryCooldown;
	public Text secondaryAmount;
	[Header("Data")]
	public bool playMode;
	
	void Reset() {
		
	}
	
	void Awake() {
		instance = this;
	}
	
	void Start() {
		UpdateBGColors();
		keyboardModeToggle.isOn = PlayerPrefs.GetInt("keyboardMode",0) == 1;
	}
	
	void Update() {
		
	}
	
	public void UpdateBGColors() {
		turbolentMat.color = StarMap.GetCurrSector().turbolentColor;
		smoothMat.color = StarMap.GetCurrSector().smoothColor;
	}
	
	public void SetGameMode() {
		UIBG.GoToScreen(6);
		playMode = true;
		controlObject.SetActive(PlayerPrefs.GetInt("keyboardMode",0) == 0);
	}
	
	public void SetKeyboardMode(bool mode) {
		PlayerPrefs.SetInt("keyboardMode",(mode) ? 1 : 0);
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
