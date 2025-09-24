using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBtn : BtnUI
{
		public new void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_QuitBtnClicked);
		}

		private void CALLBACK_QuitBtnClicked()
		{
				Application.Quit();
		}
}
