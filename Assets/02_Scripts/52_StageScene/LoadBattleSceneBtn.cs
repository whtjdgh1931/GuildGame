using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBattleSceneBtn : BtnUI
{
		[SerializeField] private World _world;
		[SerializeField] private Level _level;


		public void InitBtn()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_LoadBattleSceneBtnClicked);
		}

		private void CALLBACK_LoadBattleSceneBtnClicked()
		{
				string sceneName = StageHelper.ToSceneName(_world, _level);
				PlayerPrefs.SetString(Constants.ENEMYSCENE, sceneName);


				SceneManager.LoadScene(Constants.BATTLESCENE);
				SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		}

	public void SetStage(World world, Level level)
	{
		_world = world;
		_level = level;
	}

		
}
