using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Project Data",menuName = "Project Data",order = 3)]
public class ProjectData : ScriptableObject {
	public enum State {Finished,WorkInProgress,Priority,Planned,Considered,Canceled}
	public string localizedName = "New Project";
	public string version = "0.0.1";
	public static ProjectData instance;
	
	[HideInInspector]
	public List<TodoListEntry> todoList = new List<TodoListEntry>();
	
	void Awake() {
		instance = this;
		
	}
	
	[System.Serializable]
	public class TodoListEntry {
		public int state;
		public string label;
		public TodoListEntry() {
			state = (int)State.Planned;
			label = "New Feature";
		}
	} 
	
	#if UNITY_EDITOR
	[CustomEditor(typeof(ProjectData))]
	class ProjectDataEditor : Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			
			ProjectData pData = (ProjectData)target;
			float fieldWidth = (EditorGUIUtility.labelWidth + EditorGUIUtility.fieldWidth);
			int i = 0;
			foreach (TodoListEntry entry in pData.todoList.ToArray()) {
				GUILayout.BeginVertical("box");
				GUILayout.BeginHorizontal();
				entry.state = (int)(State)EditorGUILayout.EnumPopup((State)entry.state,GUILayout.Width(fieldWidth));
				if (GUILayout.Button("X",GUILayout.Width(20))) {
					pData.todoList.Remove(entry);
				}
				if (i > 0) {
					if (GUILayout.Button("^",GUILayout.Width(20))) {
						pData.todoList.Remove(entry);
						pData.todoList.Insert(i - 1,entry);
					}
				}
				if (i < pData.todoList.Count - 1) {
					if (GUILayout.Button("V",GUILayout.Width(20))) {
						pData.todoList.Remove(entry);
						pData.todoList.Insert(i + 1,entry);
					}
				}
				GUILayout.EndHorizontal();
				entry.label = GUILayout.TextField(entry.label);
				GUILayout.EndVertical();
				i++;
			}
			
			if (GUILayout.Button("+")) {
				pData.todoList.Add(new TodoListEntry());
			}
		}
	}
	#endif
}
