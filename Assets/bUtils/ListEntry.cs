using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListEntry : MonoBehaviour {
	public IList parent;
	public Text labelEl;
	public string label;
	public object value;
	
	public void Init(IList parent_,string label_,object value_) {
		parent = parent_;
		label = label_;
		labelEl.text = label;
		value = value_;
	}
	
	public void Init(string label_,object value_,IList parent_) {
		Init(parent_,label_,value_);
	}
	
	public void Select() {
		parent.Select(this.value);
	}
}

public interface IList {
	void Select(object value);
}
