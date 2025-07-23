using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyBtn : BtnUI
{

		public new void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_LobbyBtnClicked);

		}



		public void CALLBACK_LobbyBtnClicked()
		{
				Time.timeScale = 1f;
				SceneManager.LoadScene(Constants.LOBBYSCENE);
		}
}
