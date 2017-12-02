using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// @class UIInvertColor
// @desc UI helper class for dynamic contrast.
public class UIInvertColor : MonoBehaviour {
	// @prop #:source
	// @type Graphic
	// @desc &b[;Graphic&b]; with the source color
	public Graphic source;
	// @prop #:target
	// @type Graphic
	// @desc Traget &b[;Graphic&b]; to set the inverted color
	public Graphic target;
	
	void Reset() {
		target = GetComponent<Graphic>();
	}
	
	void Update() {
		var usedColor = source.color;
		target.color = new Color(1 - usedColor.r,1 - usedColor.g,1 - usedColor.b,target.color.a);
	}
}
