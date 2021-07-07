using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGenerator : MonoBehaviour
{
	[Header("Task info")]
	[SerializeField] private string _taskTemplate;
	
	public List<Dictionary> Sets = new List<Dictionary>();
	//public Dictionary ActiveSet => Sets.Count > 0 ? Sets[_activeSetIndex] : null;




	private int _activeSetIndex = 0;

	private void Start()
	{
		
    }

	public void OnLevelStarted(int level)
	{

	}

	public void OnGameFinished()
	{

	}

	public string GetNextTask()
	{
		throw new System.NotImplementedException();

	}

	private void GenerateSet()
	{

	}
}

