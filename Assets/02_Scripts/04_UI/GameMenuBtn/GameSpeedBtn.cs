using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedBtn : BtnUI
{
    public TextMeshProUGUI text;

		public enum GameSpeed
		{ 
				ONE,
				TWO,
				THREE,
		}

		public GameSpeed speed;



		public new void Start()
		{
				base.Start();
				text = GetComponentInChildren<TextMeshProUGUI>();
				button = GetComponent<Button>();

				button.onClick.AddListener(CALLBACK_GameSpeedBtnClicked);
				switch(PlayerPrefs.GetFloat(Constants.GAMESPEED))
				{
						case 1f:
								speed = GameSpeed.ONE;
								text.text = "X1";
								break;
						case 2f:
								speed = GameSpeed.TWO;
								text.text = "X2";
								break;
						case 3f:
								speed = GameSpeed.THREE;
								text.text = "X3";
								break;
						default:
								PlayerPrefs.SetFloat(Constants.GAMESPEED, 1f);
								speed = GameSpeed.ONE;
								text.text = "X1";
								break;
				}
				
		}



		public void CALLBACK_GameSpeedBtnClicked()
    {
				switch (speed)
				{
						case GameSpeed.ONE:
								Time.timeScale = 2f;
								text.text = "X2";
								speed = GameSpeed.TWO;
								PlayerPrefs.SetFloat(Constants.GAMESPEED, 2f);
								break;
						case GameSpeed.TWO:
								Time.timeScale = 3f;
								text.text = "X3";
								speed = GameSpeed.THREE;
								PlayerPrefs.SetFloat(Constants.GAMESPEED, 3f);
								break;
						case GameSpeed.THREE:
								Time.timeScale = 1f;
								text.text = "X1";
								speed = GameSpeed.ONE;
								PlayerPrefs.SetFloat(Constants.GAMESPEED, 1f);
								break;
				}
		}
}
