using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetList : MonoBehaviour, IList {
	public GameObject listEntryPrefab;
	
	void OnEnable() {
		B.DestroyChildren(transform);
		foreach (var target in Targetable.list) {
			Instantiate(listEntryPrefab,transform.position,transform.rotation,transform).GetComponent<ListEntry>().Init(this,((target.type == Targetable.Type.Ship) ? target.owner.label + " - " : "") + target.label,target);
		}
	}
	
	public void Select(object target) {
		GameManager.instance.player.target = (Targetable)target;
	}
	
}
