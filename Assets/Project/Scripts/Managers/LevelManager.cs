using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
	[Range(1, 3)]
	[SerializeField] int _countLevels = 3;
	[SerializeField] private float _startDelay = 0.1f;

	[Header("Components")]
	[SerializeField] private TaskGenerator _taskGenerator;

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

	private Color _taskLabelColor;
	private int _currentLevel = 1;


	private void Awake()
	{
		InitMenu();
	}

	private void Start()
	{
		StartGame();
	}

	private void Update()
	{
		
	}

	private void StartGame()
	{
		_levelStarted.Invoke(_currentLevel);
		AnimateMenu();
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