using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeBtn : BtnUI
{

		public new void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_ResumeBtnClicked);
		}

		private void CALLBACK_ResumeBtnClicked()
		{
				Time.timeScale = PlayerPrefs.GetFloat(Constants.GAMESPEED);
		}
}
