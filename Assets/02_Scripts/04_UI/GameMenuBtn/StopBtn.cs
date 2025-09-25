using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopBtn : BtnUI 
{

		private new void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_OnStopBtnClicked);
		}

		private void CALLBACK_OnStopBtnClicked()
		{
				Time.timeScale = 0.0f;
		}
}
