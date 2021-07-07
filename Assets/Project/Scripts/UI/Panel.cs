using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OctanGames
{
	[RequireComponent(typeof(GridLayoutGroup))]
	public class Panel : MonoBehaviour
	{
		[SerializeField] private ItemView _itemPrefab;

		[SerializeField, Min(1)] private int _columnsPerLevel = 3;

		private List<ItemView> _items = new List<ItemView>();
		private GridLayoutGroup _grid;

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

			int countItems = countRows * _columnsPerLevel;

			for (int i = 0; i < countItems; i++)
			{
				ItemView item = Instantiate(_itemPrefab, transform);
				item.BackgroundColor = Random.ColorHSV(0, 1, 0.22f, 0.55f, 1, 1);

				_items.Add(item);
			}
		}

		private void ResetPanel()
		{
			_items.Clear();
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}
		
	}
}