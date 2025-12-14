using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHp : MonoBehaviour
{

		private Slider hpSlider;
		[SerializeField] List<Sprite> barColor;
		[SerializeField] Image barImage;

		
		public void Start()
		{
				hpSlider = GetComponent<Slider>();

		}
		

		public void SetSliderValue(float value)
		{
				if(hpSlider == null) hpSlider = GetComponent<Slider>();
				hpSlider.value = value;
		}

		public void ChangeColor(int barIndex)
		{
				barImage.sprite = barColor[barIndex];
		}
}
