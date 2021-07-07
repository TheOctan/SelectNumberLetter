using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OctanGames.Managers
{
	public class TaskGenerator : MonoBehaviour, ITaskGenerator, IItemProvider
	{
		public ItemSet ActiveSet => _itemLibrary.Sets.Count > 0 ? _itemLibrary.Sets[_activeSetIndex] : null;
		public Item CurrentItem { get; private set; }

		[Header("Task info")]
		[SerializeField] private string _taskTemplate;
		[SerializeField] private bool _randomizeWithRestart;

		[SerializeField] private Library _itemLibrary;

		private System.Random _random = new System.Random(System.DateTime.Now.Second);
		private List<RandomIndexGenerator> _indexGenerators = new List<RandomIndexGenerator>();
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
			if (_itemLibrary.Sets.Count > 1)
			{
				_activeSetIndex = _random.Next(0, _itemLibrary.Sets.Count);
			}

			int randomIndex = _indexGenerators[_activeSetIndex].GetNextRandomIndex();
			CurrentItem = ActiveSet.Items[randomIndex];
			return string.Format(_taskTemplate, CurrentItem.Name);
		}

		private void RandomizeSets()
		{
			for (int i = 0; i < _itemLibrary.Sets.Count; i++)
			{
				int itemsCount = _itemLibrary.Sets[i].Items.Count;
				_indexGenerators.Add(new RandomIndexGenerator(itemsCount));
			}
		}
	}
}