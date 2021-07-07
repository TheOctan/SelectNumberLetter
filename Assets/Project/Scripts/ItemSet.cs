using OctanGames.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary", menuName = "Scriptable Objects/Dictionary", order = 0)]
public class ItemSet : ScriptableObject
{
	public List<Item> Items = new List<Item>();

	private Queue<int> _randomIndices;

	public Item GetNextRandomItem()
	{
		int randoIndex = _randomIndices.Dequeue();
		_randomIndices.Enqueue(randoIndex);
		return Items[randoIndex];
	}

	public void RandomizeSet()
	{
		List<int> indices = new List<int>(Items.Count);
		for (int i = 0; i < Items.Count; i++)
		{
			indices[i] = i;
		}

		_randomIndices = new Queue<int>(indices.Shuffle());
	}
}

[System.Serializable]
public class Item
{
	public string Name;
	public Sprite Image;
	public bool Rotate;
}