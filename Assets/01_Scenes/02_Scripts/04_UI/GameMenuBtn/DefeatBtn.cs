using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatBtn : BtnUI
{
		new void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_StartBtnClicked);
		}

		private void CALLBACK_StartBtnClicked()
		{
				UIManager.Instance().defeatImage.gameObject.SetActive(false);
				SceneManager.LoadScene(Constants.LOBBYSCENE);
		}
}
