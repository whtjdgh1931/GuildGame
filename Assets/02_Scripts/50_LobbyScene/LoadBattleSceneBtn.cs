using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBattleSceneBtn : BtnUI
{
		public string enemyScene;


		protected new void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_LoadBattleSceneBtnClicked);
		}

		private void CALLBACK_LoadBattleSceneBtnClicked()
		{
				PlayerPrefs.SetString(Constants.ENEMYSCENE, enemyScene);
				SceneManager.LoadScene(Constants.BATTLESCENE);
				SceneManager.LoadScene(enemyScene,LoadSceneMode.Additive);
		}
}
