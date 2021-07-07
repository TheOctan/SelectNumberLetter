using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
	public Color BackgroundColor
	{
		get => _background.color;
		set => _background.color = value;
	}
	public Sprite iconImage
	{
		get => _icon.sprite;
		set => _icon.sprite = value;
	}
	
	[SerializeField] private Image _background;
	[SerializeField] private Image _icon;
}

