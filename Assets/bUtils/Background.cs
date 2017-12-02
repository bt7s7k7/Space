using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	public Transform target;
	public BGLayer[] layers;
	public float globalEffStrength = 1;
	public Axis axis = Axis.Normal;
	
	public enum Axis {Normal,TopDown}
	
	void Reset() {
		
	}
	
	void Awake() {
		
	}
	
	void Start() {
		
	}
	
	void Update() {
		transform.position = target.position;
		
		foreach (BGLayer layer in layers) {
			layer.material.mainTextureOffset = GetPosInAxis() * layer.paralax * globalEffStrength;
		}
	}
	
	public Vector2 GetPosInAxis() {
		Vector2 ret = Vector2.zero;
		if (axis == Axis.Normal) {
			ret = new Vector2(transform.position.x,transform.position.y);
		} else if (axis == Axis.TopDown) {
			ret = new Vector2(transform.position.x,transform.position.z);
		}
		return ret;
	}
	
	void FixedUpdate() {
		
	}
	
	[System.Serializable]
	public class BGLayer {
		public float paralax = 1;
		public Material material;
	}
	
}
