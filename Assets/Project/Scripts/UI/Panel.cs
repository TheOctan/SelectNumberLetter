using OctanGames.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OctanGames
{
	[RequireComponent(typeof(GridLayoutGroup))]
	public class Panel : MonoBehaviour
	{
		[SerializeField, Min(1)] private int _columnsPerLevel = 3;
		[SerializeField] private ItemView _itemPrefab;
		[SerializeField] private MonoBehaviour _provider;

		[Header("Events")]
		[SerializeField] private SelectionEvent _answerSelected;

		private IItemProvider _itemProvider;
		private GridLayoutGroup _grid;
		private List<string> _items = new List<string>();
		private RandomIndexGenerator _indexGenerator = new RandomIndexGenerator();
		private System.Random _random = new System.Random(System.DateTime.Now.Second);

		private void OnValidate()
		{
			if (_provider is IItemProvider itemProvider)
			{
				_itemProvider = itemProvider;
			}
			else if (_provider != null)
			{
				Debug.LogError(_provider.name + " needs to implement " + nameof(IItemProvider));
				_provider = null;
			}
		}

		private void Awake()
		{
			UpdateGrid();
		}

		public void OnLevelStarted(int level)
		{
			GenerateGrid(level);
		}

		private void OnSelectAnswer(int index)
		{
			string answer = _items[index];
			_answerSelected.Invoke(answer);
		}

		private void UpdateGrid()
		{
			_grid = GetComponent<GridLayoutGroup>();
			_grid.constraintCount = _columnsPerLevel;
		}

		private void GenerateGrid(int countRows)
		{
			ResetPanel();

			List<Item> items = _itemProvider.ActiveSet.Items;
			_indexGenerator.Randomize(items.Count);


			int countItems = countRows * _columnsPerLevel;
			int activePosition = _random.Next(0, countItems);

			for (int i = 0; i < countItems; i++)
			{
				if (i == activePosition)
				{
					InitItemView(_itemProvider.CurrentItem, i);
				}
				else
				{
					Item item;
					do
					{
						int randomIndex = _indexGenerator.GetNextRandomIndex();
						item = items[randomIndex];

					} while (item.Name.Equals(_itemProvider.CurrentItem.Name));

					InitItemView(item, i);
				}
			}
		}

		private void ResetPanel()
		{
			_items.Clear();
			for (int i = 0; i < transform.childCount; i++)
			{
				var item = transform.GetChild(i).gameObject;
				item.GetComponent<Button>().onClick.RemoveAllListeners();
				Destroy(item);
			}
		}

		private ItemView InitItemView(Item item, int index)
		{
			ItemView itemView = Instantiate(_itemPrefab, transform);
			itemView.BackgroundColor = Random.ColorHSV(0, 1, 0.22f, 0.55f, 1, 1);
			itemView.iconImage = item.Image;
			itemView.GetComponent<Button>().onClick.AddListener(() => OnSelectAnswer(index));

			_items.Add(item.Name);

			if (item.RotateImage)
			{
				itemView.RotateIcon(-90);
			}

			return itemView;
		}
	}

	[System.Serializable]
	public class SelectionEvent : UnityEvent<string>
	{
	}
}