using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameMoment))]
[CanEditMultipleObjects]
public class CustomMoment : Editor {
	SerializedProperty Info;
	void OnEnable()
	{
		Info = serializedObject.FindProperty("MomentInfo2");
	}
	public override void OnInspectorGUI()
	{
		///DrawDefaultInspector();
		///

		DrawDefaultInspector();


		var t = (target as  GameMoment	);
		
		if (GUILayout.Button("Init"))
		{
			t.MomentInfo2.Clear();
			foreach (var temp in Parameters.Names)
			{
				t.MomentInfo2.Add(new MomentInfo(temp));
			}
		}


		this.Repaint();

	}
}
