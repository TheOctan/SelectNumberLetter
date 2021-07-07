using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OctanGames
{
	public class TaskGenerator : MonoBehaviour
	{
		[Header("Task info")]
		[SerializeField] private string _taskTemplate;
		[SerializeField] private bool _randomizeWithRestart;

		public List<ItemSet> Sets = new List<ItemSet>();
		public ItemSet ActiveSet => Sets.Count > 0 ? Sets[_activeSetIndex] : null;

		private System.Random _random = new System.Random(System.DateTime.Now.Second);
		private int _activeSetIndex = 0;

		private void Awake()
		{
			RandomizeSets();
		}

		public void OnGameFinished()
		{
			if (_randomizeWithRestart)
			{
				RandomizeSets();
			}
		}

		public string GetNextTask()
		{
			if (Sets.Count > 1)
			{
				_activeSetIndex = _random.Next(0, Sets.Count);
			}

			var item = Sets[_activeSetIndex].GetNextRandomItem();
			return string.Format(_taskTemplate, item.Name);
		}

		private void RandomizeSets()
		{
			for (int i = 0; i < Sets.Count; i++)
			{
				Sets[i].RandomizeSet();
			}
		}
	}
}