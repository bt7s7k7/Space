using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// @class MeshWorks
// @desc A static class to help with mesh creation.
static public class MeshWorks {
	
	// @class MeshWorks.Triangle
	// @desc Class describing a single triangle
	public class Triangle {
		// @prop #:a
		// @type Vector3
		// @desc First point
		public Vector3 a;
		// @prop #:b
		// @type Vector3
		// @desc Second point
		public Vector3 b;
		// @prop #:c
		// @type Vector3
		// @desc Third point
		public Vector3 c;
		
		// @function #:Triangle
		// @constructor 
		// @param a Vector3 First point
		// @param b Vector3 Second point
		// @param c Vector3 Third point
		Triangle(Vector3 a_,Vector3 b_,Vector3 c_) {
			
		} 
	}
}
