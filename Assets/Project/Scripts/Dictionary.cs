using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary", menuName = "Scriptable Objects/Dictionary", order = 0)]
public class Dictionary : ScriptableObject
{
	public List<Item> Items = new List<Item>();
}

[System.Serializable]
public class Item
{
	public string Name;
	public Sprite Image;
}