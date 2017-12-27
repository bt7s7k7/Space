using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
	public Slider healthBar;
	public Slider shieldBar;
	public Text focusMenuButtonText;
	public ShipMenu shipMenu;
	public StationMenu stationMenu;
	public List<ShipSO> ships = new List<ShipSO>();
	public NetworkHelper netHelp;
	public Transform targetPointer;
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
	
	public static PlayerStats GetPlayerStats() {
		string json = PlayerPrefs.GetString("playerStats","{\"money\":1000,\"level\":0,\"shipsOwned\":[\"Beetle\"],\"materials\":[0,0,0,0,0,0,0,0,0,0]}");
		PlayerStats stats = JsonUtility.FromJson<PlayerStats>(json);
		return stats;
	}
	
	public static void SetPlayerStats(PlayerStats newP) {
		PlayerPrefs.SetString("playerStats",JsonUtility.ToJson(newP));
	}
	
	public static bool RemoveMoney(int amount) {
		PlayerStats stats = GetPlayerStats();
		stats.money -= amount;
		if (stats.money >= 0) {
			SetPlayerStats(stats);
			return true;
		}
		return false;
	}
	
	public static void AddMoney(int amount) {
		PlayerStats stats = GetPlayerStats();
		stats.money += amount;
		SetPlayerStats(stats);
	}
	
	public void SetGameMode() {
		UIBG.GoToScreen(6);
		playMode = true;
		controlObject.SetActive(PlayerPrefs.GetInt("keyboardMode",0) == 0);
	}
	
	public void SetKeyboardMode(bool mode) {
		PlayerPrefs.SetInt("keyboardMode",(mode) ? 1 : 0);
	}
	
	public void OpenFocusedMenu() {
		player.openMenu = true;
	}
	
	public void OpenShipMenu(Ship ship) {
		UIBG.GoToScreen(8);
		shipMenu.ship = ship;
	}
	
	public void Unfocus() {
		player.focused = null;
		UIBG.GoToScreen(6);
	}
	
	public void OpenStationMenu(Station station) {
		UIBG.GoToScreen(9);
		stationMenu.station = station;
	}
	
	public void WipePlayerData() {
		PlayerPrefs.DeleteKey("playerStats");
	}
	
	public void Disconnect() {
		UIBG.GoToScreen(0);
		StartCoroutine("Disconnecting");
	}
	
	IEnumerator Disconnecting() {
		yield return new WaitForSeconds(0.35f);
		netHelp.Disconnect();
	}
	
	[System.Serializable]
	public class PlayerStats {
		public int money;
		public int level;
		public string[] shipsOwned;
		public int[] materials;
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
			if (GUILayout.Button("Wipe playerStats")) {
				PlayerPrefs.DeleteKey("playerStats");
			}
		}
	}
	#endif

	
}
