using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneLoad : MonoBehaviour
{
    public PlayerScriptableObject playerClassScriptableObject;

    public string playerClass = Constants.CLASS_TANKER;

    public bool isAuto;

    // Start is called before the first frame update
    void Start()
    {
				SceneManager.sceneLoaded -= CALLBACK_MakePlayer;
				SceneManager.sceneLoaded += CALLBACK_MakePlayer; 
    }

		private void CALLBACK_MakePlayer(Scene arg0, LoadSceneMode arg1)
		{

				if (arg0.buildIndex != 2) return;
				Soldier playerPrefab =  playerClassScriptableObject.GetClassDataByClassName(playerClass).soldierPrefab;
      Soldier player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.name = Constants.NAME_Player;
        player.level = PlayerPrefs.GetInt(Constants.CLASS_PLAYER);
        player.GetComponent<Player_Soldier>().isAuto = this.isAuto;
		}

    public void PressStartButton()
    {
        SceneManager.LoadScene(2);
    }

    public void SetPlayerClassString(string playerClass)
    {
        this.playerClass = playerClass;
    }

    public void SetAuto(bool auto)
    { this.isAuto = auto; }
}
