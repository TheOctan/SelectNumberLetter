using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace OctanGames
{
	public class LevelManager : MonoBehaviour
	{
		[Header("Settings")]
		[Range(1, 3)]
		[SerializeField] int _countLevels = 3;
		[SerializeField] private float _startDelay = 0.1f;

		[Header("Panel settings")]
		[SerializeField] private Panel _panel;
		[SerializeField] private float _inDuration = 1f;
		[SerializeField] private Ease _panelEaseType = Ease.Linear;

		[Header("Task settings")]
		[SerializeField] private TextMeshProUGUI _taskLabel;
		[SerializeField] private float _fadeDuration = 1f;
		[SerializeField] private Ease _taskEaseType = Ease.Linear;

		[Header("Events")]
		[SerializeField] private LevelEvent _levelStarted;
		[SerializeField] private UnityEvent _gameFinished;

		private ITaskGenerator _taskGenerator;

		private Color _taskLabelColor;
		private int _currentLevel = 1;

		private void Awake()
		{
			_taskGenerator = GetComponent<ITaskGenerator>();
			InitMenu();
		}

		private void Start()
		{
			StartGame();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				NextLevel();
			}
		}

		private void StartGame()
		{
			_levelStarted.Invoke(_currentLevel);
			UpdateTask();
			AnimateMenu();
		}

		public void NextLevel()
		{
			_currentLevel++;
			if (_currentLevel > _countLevels)
			{
				_currentLevel = 1;

				_gameFinished.Invoke();
			}

			_levelStarted.Invoke(_currentLevel);
			UpdateTask();
		}
		
		private void UpdateTask()
		{
			_taskLabel.text = _taskGenerator.GetNextTask();
		}

		private void InitMenu()
		{
			_taskLabelColor = _taskLabel.color;
			_taskLabel.color = new Color(0, 0, 0, 0);
			_panel.transform.localScale = new Vector3(0, 0, 0);
		}

		private void AnimateMenu()
		{
			DOTween.Sequence()
				.AppendInterval(_startDelay)
				.Append(_panel.transform.DOScale(1, _inDuration).SetEase(_panelEaseType))
				.Append(_taskLabel.DOColor(_taskLabelColor, _fadeDuration).SetEase(_taskEaseType));
		}
	}

	[System.Serializable]
	public class LevelEvent : UnityEvent<int>
	{
	}
}