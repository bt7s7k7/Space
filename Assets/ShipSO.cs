using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Ship",menuName = "ShipSO",order = 4)]
public class ShipSO : ScriptableObject {
	public string label = "New Ship";
	public int value;
	public int level;
	public int health;
	public int shields;
	public int shieldRechargeCooldown;
	public int secondaryType = -1;
	public int maxSecondary;
	public int secondaryCooldown;
	public bool hasWeapon;
	public int shotCooldown;
	public int shotDamage;
	
	public Sprite color;
	public Sprite overlay;
	public Sprite detail;
	public Sprite meta;
	
	public static Dictionary<string,ShipSO> shipTypes = new Dictionary<string,ShipSO>();
	
	public void OnEnable() {
		shipTypes.Add(label,this);
		Debug.Log("Registering ship: " + label);
	}
	
	public GameObject Spawn(Faction faction,Vector3 position) {
		GameObject newShip = MonoBehaviour.Instantiate(GameManager.instance.shipPrefab,position,Quaternion.identity);
		if (NetworkManager.singleton.isNetworkActive) {
			NetworkServer.Spawn(newShip);
		}
		Ship ship = newShip.GetComponent<Ship>();
		ship.CmdInit(faction,this.label);
		return newShip;
	}
	
	#if UNITY_EDITOR
	[CustomEditor(typeof(ShipSO))]
	class ShipSOEditor : Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			ShipSO so = (ShipSO)target;
			if (Application.isPlaying) {
				if (GUILayout.Button("Spawn at camera")) {
					so.Spawn(GameManager.instance.factions[0],Vector3.Scale(GameManager.instance.cam.transform.position,new Vector3(1,1,0)));
				}
			}
		}
	}
	#endif
}
