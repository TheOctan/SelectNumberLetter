using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
	[SerializeField] private ItemView _itemPrefab;

	[SerializeField] private GridLayoutGroup _grid;
	[SerializeField, Min(1)] private int _columnsPerLevel = 3;


	private void Start()
	{
		
    }

	private void Update()
	{
		
	}

	public void OnLevelStarted(int level)
	{
		GenerateGrid(level);
	}

	private void GenerateGrid(int countRows)
	{

	}
}

