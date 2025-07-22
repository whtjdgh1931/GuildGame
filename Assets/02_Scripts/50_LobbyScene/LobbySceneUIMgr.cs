using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbySceneUIMgr : MonoBehaviour
{
    public Text playerClassText;

    public BattleSceneLoad battleSceneLoad;

		public Image image;
		public void Update()
		{
				playerClassText.text = battleSceneLoad.playerClass;
		}

}
