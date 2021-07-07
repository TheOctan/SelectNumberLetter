using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace OctanGames
{
	[CustomEditor(typeof(ItemSet))]
	public class DictionaryEditor : Editor
	{
		private ItemSet dictionary;

		private void OnEnable()
		{
			dictionary = target as ItemSet;
		}

		public override void OnInspectorGUI()
		{
			if (dictionary.Items.Count > 0)
			{
				foreach (var item in dictionary.Items)
				{
					EditorGUILayout.BeginVertical("Box");

					if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
					{
						dictionary.Items.Remove(item);
						break;
					}

					item.Name = EditorGUILayout.TextField("Name", item.Name);
					item.Rotate = EditorGUILayout.Toggle("Rotate", item.Rotate);
					item.Image = EditorGUILayout.ObjectField("Image", item.Image, typeof(Sprite), false) as Sprite;

					EditorGUILayout.EndVertical();
				}
			}
			else
			{
				EditorGUILayout.LabelField("No Items");
			}

			if (GUILayout.Button("Add", GUILayout.Height(30)))
			{
				dictionary.Items.Add(new Item());
			}

			if (GUI.changed)
			{
				SetObjectDirty(dictionary);
			}
		}

		public static void SetObjectDirty(Object obj)
		{
			EditorUtility.SetDirty(obj);
		}
	}
}