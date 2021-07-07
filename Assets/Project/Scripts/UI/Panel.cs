using OctanGames.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace OctanGames
{
	[RequireComponent(typeof(GridLayoutGroup))]
	public class Panel : MonoBehaviour
	{
		[SerializeField, Min(1)] private int _columnsPerLevel = 3;
		[SerializeField] private ItemView _itemPrefab;
		[SerializeField] private MonoBehaviour _provider;
		[SerializeField] private ParticleSystem _winEffect;

		[Header("Correct answer")]
		[SerializeField] private Ease _correctShakeType = Ease.Linear;
		[SerializeField, Min(0.01f)] private float _correctShakeDuration = 0.3f;
		[SerializeField] private float _correctShakeStrength = 0.15f;

		[Header("Fail answer")]
		[SerializeField] private Ease _failShakeType = Ease.Linear;
		[SerializeField, Min(0.01f)] private float _failShakeDuration = 0.5f;
		[SerializeField] private float _failShakeStrength = 10;

		[Header("Events")]
		[SerializeField] private UnityEvent _answerSelected;

		private IItemProvider _itemProvider;
		private GridLayoutGroup _grid;
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
					ItemView itemView = InitItemView(_itemProvider.CurrentItem);
					SubscribeEvent(itemView, OnCorrectAnswer, i);
				}
				else
				{
					Item item;
					do
					{
						int randomIndex = _indexGenerator.GetNextRandomIndex();
						item = items[randomIndex];

					} while (item.Name.Equals(_itemProvider.CurrentItem.Name));

					ItemView itemView = InitItemView(item);
					SubscribeEvent(itemView, OnFailAnswer, i);
				}
			}
		}

		private void ResetPanel()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				var item = transform.GetChild(i).gameObject;
				item.GetComponent<Button>().onClick.RemoveAllListeners();
				Destroy(item);
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

		private void SubscribeEvent(ItemView itemView, UnityAction<int> action, int index)
		{
			itemView.GetComponent<Button>().onClick.AddListener(() => action(index));
		}

		private void OnCorrectAnswer(int index)
		{
			Transform item = transform.GetChild(index);
			DOTween.Sequence()
				.Append(item.DOShakeScale(_correctShakeDuration, _correctShakeStrength, 10, 0).SetEase(_correctShakeType))
				.AppendInterval(0.5f)
				.OnComplete(_answerSelected.Invoke);

			_winEffect.transform.position = item.transform.position;
			_winEffect.Play();
		}

		private void OnFailAnswer(int index)
		{
			transform.GetChild(index).DOShakeRotation(_failShakeDuration, Vector3.forward * _failShakeStrength).SetEase(_failShakeType);
		}
	}
}