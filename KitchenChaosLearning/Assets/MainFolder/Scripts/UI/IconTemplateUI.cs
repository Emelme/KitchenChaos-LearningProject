using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTemplateUI : MonoBehaviour
{
	[SerializeField] private Image image;

	public void SetIconSprite(Sprite sprite)
	{
		image.sprite = sprite;
	}
}
