using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OctanGames
{
	[CreateAssetMenu(fileName = "New Library", menuName = "Scriptable Objects/Library", order = 1)]
	public class Library : ScriptableObject
	{
		public List<ItemSet> Sets = new List<ItemSet>();
	}
}