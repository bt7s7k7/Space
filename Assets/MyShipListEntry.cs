using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyShipListEntry : MonoBehaviour {
	public StationMenu parent;
	public Text labelEl;
	public string label;
	
	public void Init(StationMenu parent_,string label_) {
		parent = parent_;
		label = label_;
	}
	
	public void Select() {
		parent.Select(this.label);
	}
}
