using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OctanGames
{
	[RequireComponent(typeof(Button))]
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

		public void RotateIcon(float angle)
		{
			_icon.rectTransform.Rotate(Vector3.forward, angle);
		}

		[SerializeField] private Image _background;
		[SerializeField] private Image _icon;
	}
}