using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtn : BtnUI
{


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(CALLBACK_StartBtnClicked);
        AddBtnAnim();
    }

		private void CALLBACK_StartBtnClicked()
		{
				SceneManager.LoadScene(Constants.LOBBYSCENE);
		}
}
