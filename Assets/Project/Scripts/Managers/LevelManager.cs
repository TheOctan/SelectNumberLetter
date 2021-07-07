using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using OctanGames.Extensions;

namespace OctanGames.Managers
{
	public class LevelManager : MonoBehaviour
	{
		[Header("Settings")]
		[Range(1, 3)]
		[SerializeField] int _countLevels = 3;
		[SerializeField, Min(0.01f)] private float _startDelay = 0.1f;
		[SerializeField, Min(0.01f)] private float _loadDelay = 0.1f;

		[Header("UI")]
		[SerializeField] private Image _fadeScreen;
		[SerializeField] private float _fadeScreenDuration = 0.3f;
		[Space(10)]
		[SerializeField] private Image _loadScreen;
		[SerializeField] private float _loadScreenDuration = 0.3f;
		[Space(10)]
		[SerializeField] private Transform _restartButton;
		[SerializeField, Min(0.01f)] private float _buttonInDuration = 0.3f;
		[SerializeField] private Ease _buttonEaseType = Ease.Linear;

		[Header("Panel settings")]
		[SerializeField] private Transform _panel;
		[SerializeField, Min(0.01f)] private float _panelInDuration = 0.3f;
		[SerializeField] private Ease _panelEaseType = Ease.Linear;

		[Header("Task settings")]
		[SerializeField] private TextMeshProUGUI _taskLabel;
		[SerializeField, Min(0.01f)] private float _fadeLabelDuration = 0.5f;
		[SerializeField] private Ease _taskEaseType = Ease.Linear;

		[Header("Events")]
		[SerializeField] private LevelEvent _levelStarted;
		[SerializeField] private UnityEvent _gameFinished;

		private ITaskGenerator _taskGenerator;

		private int _currentLevel = 1;

		private void Awake()
		{
			_taskGenerator = GetComponent<ITaskGenerator>();
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

		public void NextLevel()
		{
			_currentLevel++;
			if (_currentLevel > _countLevels)
			{
				RestartLevels();
				StopGame();
				return;
			}

			UpdateTask();
			_levelStarted.Invoke(_currentLevel);
		}

		public void RestartGame()
		{
			FadeInOutLoadScreen();
		}

		private void StartGame()
		{
			UpdateTask();
			AnimateMenu();
			_levelStarted.Invoke(_currentLevel);
		}

		private void StopGame()
		{
			FadeInScreen();
			_gameFinished.Invoke();
		}

		private void RestartLevels()
		{
			_currentLevel = 0;
		}

		private void UpdateTask()
		{
			_taskLabel.text = _taskGenerator.GetNextTask();
		}

		private void AnimateMenu()
		{
			Color taskLabelColor = _taskLabel.color;
			_taskLabel.color = new Color(0, 0, 0, 0);
			_panel.localScale = Vector3.zero;

			DOTween.Sequence()
				.AppendInterval(_startDelay)
				.Append(_panel.transform.DOScale(1, _panelInDuration).SetEase(_panelEaseType))
				.Append(_taskLabel.DOColor(taskLabelColor, _fadeLabelDuration).SetEase(_taskEaseType));
		}

		private void FadeInScreen()
		{
			_fadeScreen.gameObject.SetActive(true);
			Color fadeScreenColor = _fadeScreen.color;
			_fadeScreen.color = Color.black.SetAlpha(0);
			_restartButton.localScale = Vector3.zero;

			DOTween.Sequence()
				.Append(_fadeScreen.DOFade(fadeScreenColor.a, _fadeScreenDuration))
				.Append(_restartButton.DOScale(1, _buttonInDuration).SetEase(_buttonEaseType));
		}

		private void DisableFadeScreen()
		{
			_fadeScreen.gameObject.SetActive(false);
		}

		private void FadeInOutLoadScreen()
		{
			_loadScreen.gameObject.SetActive(true);
			_loadScreen.color = Color.white.SetAlpha(0);

			DOTween.Sequence()
				.Append(_loadScreen.DOFade(1, _loadScreenDuration))
				.AppendCallback(() =>
				{
					DisableFadeScreen();
					NextLevel();
				})
				.AppendInterval(_loadDelay)
				.Append(_loadScreen.DOFade(0, _loadScreenDuration))
				.OnComplete(() =>
				{
					_loadScreen.gameObject.SetActive(false);
				});
		}
	}

	[System.Serializable]
	public class LevelEvent : UnityEvent<int>
	{
	}
}