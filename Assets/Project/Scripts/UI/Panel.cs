using OctanGames.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OctanGames
{
	[RequireComponent(typeof(GridLayoutGroup))]
	public class Panel : MonoBehaviour
	{
		[SerializeField, Min(1)] private int _columnsPerLevel = 3;
		[SerializeField] private ItemView _itemPrefab;
		[SerializeField] private MonoBehaviour _provider;

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
					InitItemView(_itemProvider.CurrentItem);
				}
				else
				{
					int randomIndex;
					Item item;
					do
					{
						randomIndex = _indexGenerator.GetNextRandomIndex();
						item = items[randomIndex];

					} while (item.Name.Equals(_itemProvider.CurrentItem.Name));

					InitItemView(item);
				}
			}
		}

		private ItemView InitItemView(Item item)
		{
			ItemView itemView = Instantiate(_itemPrefab, transform);
			itemView.BackgroundColor = Random.ColorHSV(0, 1, 0.22f, 0.55f, 1, 1);
			itemView.iconImage = item.Image;

			if (item.RotateImage)
			{
				itemView.RotateIcon(-90);
			}

			return itemView;
		}

		private void ResetPanel()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}

	}
}