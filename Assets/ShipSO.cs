using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "New Ship",menuName = "ShipSO",order = 4)]
public class ShipSO : ScriptableObject {
	public string label = "New Ship";
	public float camSize = 5;
	public int value;
	public int level;
	public int health;
	public int shield;
	public float shieldRecharge;
	public int secondaryType = -1;
	public int maxSecondary;
	public int secondaryCooldown;
	public bool hasWeapon;
	public int shotCooldown;
	public int shotDamage;
	public float forwardSpeed = 50;
	public float lateralSpeed = 10;
	public float maxSpeed = 5;
	public float slowdownSpeed = 10;
	public float rotationSpeed = 0.1f;
	public List<ShipEntityPrototype> entities;
	
	public Sprite color;
	public Sprite overlay;
	public Sprite detail;
	
	public static Dictionary<string,ShipSO> shipTypes = new Dictionary<string,ShipSO>();
	
	public void OnEnable() {
		Register();
	}
	
	public void Register() {
		if (!shipTypes.ContainsKey(label))
			shipTypes.Add(label,this);
	}
	
	public void Awake() {
		Register();
	}
	
	public ShipSO() {
		Register();
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
	
	[System.Serializable]
	public class ShipEntityPrototype {
		public enum Type {ThrusterForward,ThrusterBack,ThrusterLeft,ThrusterRight,Logo,Turret,SpecialLocation}
		public Type type;
		public Vector2 pos;
		public ShipEntityPrototype (Type type_,Vector3 pos_) {
			type = type_;
			pos = pos_;
		}
		
		public ShipEntity Spawn(Transform parent,Ship ship,Vector3 pos) {
			var	ret = Instantiate(GameManager.instance.shipEntityPrefabs[(int)type],parent.TransformPoint(pos),Quaternion.identity,parent).GetComponent<ShipEntity>();
			ret.parent = ship;
			return ret;
		}
	}
	
	public Vector3 ToLocalPos(float x,float y) {
		return new Vector3((x - color.rect.width / 2.0f) / color.pixelsPerUnit,(y - color.rect.height / 2.0f) / color.pixelsPerUnit,0);
	}
	
	public ShipEntity[] SpawnEntities(Transform parent,Ship ship) {
		List<ShipEntity> ret = new List<ShipEntity>();
		foreach (ShipEntityPrototype entity in entities) {
			ret.Add(entity.Spawn(parent,ship,ToLocalPos(entity.pos.x,color.rect.height - entity.pos.y)));
		}
		return ret.ToArray();
	}
	
	public string GetInfo() {
		return "Type: " + label +
			 "\nValue: " + value +
			 "\nLevel: " + level +
			 "\nMax shield: " + shield +
		     " (recharge: " + shieldRecharge + "u/ms)" +
			 "\nMax health: " + health +
			 ((secondaryType > -1) ? "\nSecondary cooldown: " + secondaryCooldown + "ms\nSecondary Type: " + "-- TODO --" : "") +
			 "\n" + ((hasWeapon) ? "Weapon damage:" + shotDamage + "\nWeapon cooldown:" + shotCooldown + "ms" : "No weapon");
	}
	
	#if UNITY_EDITOR
	[CustomEditor(typeof(ShipSO))]
	class ShipSOEditor : Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			ShipSO so = (ShipSO)target;
			if (Application.isPlaying) {
				if (GUILayout.Button("Spawn at camera")) {
					so.Spawn(GameManager.instance.player.faction,Vector3.Scale(GameManager.instance.cam.transform.position,new Vector3(1,1,0)));
				}
			}
			GUILayout.BeginVertical("box");
			GUILayout.Label("Registered ships");
			foreach (KeyValuePair<string,ShipSO> kv in shipTypes) {
				GUILayout.Label("- " + kv.Key);
			}
			GUILayout.EndVertical();
		}
	}
	#endif
}
