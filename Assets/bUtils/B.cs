using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// @class B
// @desc Static helper class with very helpful functions
public static class B {
	
	// @function #:SerializeString
	// @param text string String to serialize
	// @return string Serialized string
	// @desc Serializes the provided string to hex ASCII codes.
	static public string SerializeString(string text) {
		string[] ret = new string[text.Length];
		for (int i = 0;i < text.Length;i++) {
			ret[i] = ((int)text[i]).ToString("X2");
		}
		return string.Join("",ret);
	}
	// @function #:DeserializeString
	// @param serialized string Previously serialized string
	// @return string Deserialized string
	// @desc Deserializes strings in a X2 ASCII format
	static public string DeserializeString(string source) {
		char[] ret = new char[source.Length / 2];
		for (int i = 0;i < source.Length;i += 2) {
			ret[i / 2] = (char)int.Parse(new string(new char[] {source[i],source[i+1]}),System.Globalization.NumberStyles.HexNumber);
		}
		return new string(ret);
	}
	// @function #:SerializeDict
	// @param dictionary Dictionary<string,string> &b[;Dictionary&b]; to serialize
	// @return string Serialized dictionary
	// @desc A function to serialize a string &b[;Dictionary&b];.
	static public string SerializeDict(Dictionary<string,string> dict) {
		string[] pairs = new string[dict.Count];
		int i = 0;
		foreach (KeyValuePair<string,string> kv in dict) {
			pairs[i] = SerializeString(kv.Key) + ":" + SerializeString(kv.Value);
			i++;
		}
		return string.Join(",",pairs);
	}
	// @function #:DeserializeDict
	// @param dict string Previously serialized &b[;Dictionary&b];
	// @return Dictionary<string,string> Deserialized &b[;Dictionary&b];
	// @desc Deserializes a &b[;Dictionary&b]; in a X2 ASCII format.
	static public Dictionary<string,string> DeserializeDict(string text) {
		Dictionary<string,string> ret = new Dictionary<string,string>();
		string[] segment;
		string[] segments = text.Split(',');
		for (var i = 0;i < segments.Length;i++) {
			segment = segments[i].Split(':');
			if (segment.Length != 2) continue;
			ret.Add(DeserializeString(segment[0]),DeserializeString(segment[1]));
		}
		return ret;
	}
	// @function #:SerializeList
	// @param List List<string> &b[;List&b]; to serialize
	// @return string Serialized List
	// @desc A function to serialize a string &b[;List&b];.
	static public string SerializeList(List<string> list) {
		string[] pairs = new string[list.Count];
		int i = 0;
		foreach (string kv in list) {
			pairs[i] = SerializeString(kv);
			i++;
		}
		return string.Join(",",pairs);
	}
	// @function #:Deserializelist
	// @param list string Previously serialized &b[;List&b];
	// @return List<string> Deserialized &b[;List&b];
	// @desc Deserializes a &b[;List&b]; in a X2 ASCII format.
	static public List<string> DeserializeList(string text) {
		List<string> ret = new List<string>();
		string[] segments = text.Split(',');
		for (var i = 0;i < segments.Length;i++) {
			ret.Add(DeserializeString(segments[i]));
		}
		return ret;
	}
	
	// @function #:GetChildren
	// @param target Transform The &b[;Transform&b]; to get children of
	// @return Transform[] Children of the provided &b[;Transform&b];
	// @desc Returns all children of the provided &b[;Transform&b];
	static public Transform[] GetChildren(Transform thing) {
		Transform[] ret = new Transform[thing.childCount];
		for (var i = 0;i < ret.Length;i++) {
			ret[i] = thing.GetChild(i);
		}
		return ret;
	}
 	
	// @function #:DestroyChildren
	// @param target Transform The &b[;Transform&b]; to remove children of
	// @return void
	// @desc Removes all children of the provided &b[;Transform&b];
	static public void DestroyChildren(Transform thing) {
		foreach (Transform child in GetChildren(thing)) {
			MonoBehaviour.Destroy(child.gameObject);
		}
	}
	
	#if UNITY_EDITOR
	[MenuItem("CONTEXT/MeshFilter/Save mesh...")]
	static void SaveMesh(MenuCommand context) {
		MeshFilter filter = (MeshFilter)context.context;
		Mesh mesh = filter.sharedMesh;
		string path = EditorUtility.SaveFilePanel("Save Mesh", "Assets/", mesh.name, "asset");
		
		if (System.String.IsNullOrEmpty(path)) return;
		
		path = FileUtil.GetProjectRelativePath(path);
		
		AssetDatabase.CreateAsset((Mesh)UnityEngine.Object.Instantiate(mesh), path);
		AssetDatabase.SaveAssets();
	}
	#endif
}
