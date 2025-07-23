using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBattleSceneBtn : BtnUI
{
		public SceneAsset enemyScene;


		protected new void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_LoadBattleSceneBtnClicked);
		}

		private void CALLBACK_LoadBattleSceneBtnClicked()
		{
				PlayerPrefs.SetString(Constants.ENEMYSCENE, enemyScene.name);
				SceneManager.LoadScene(Constants.BATTLESCENE);
				SceneManager.LoadScene(enemyScene.name,LoadSceneMode.Additive);
		}
}
