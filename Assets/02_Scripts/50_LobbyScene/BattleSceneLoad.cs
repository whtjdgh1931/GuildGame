using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSceneLoad : MonoBehaviour
{
    public PlayerScriptableObject playerClassScriptableObject;

	public string playerClass;


		public void Awake()
		{
				if (FindObjectsOfType(GetType()).Length > 1)
				{
						Destroy(gameObject);
						return;
				}
				DontDestroyOnLoad(gameObject);

		}

		// Start is called before the first frame update
		void Start()
    {
				SceneManager.sceneLoaded += CALLBACK_MakePlayer;



    }


    private void CALLBACK_MakePlayer(Scene arg0, LoadSceneMode arg1)
		{
		if (LobbySceneUIMgr.Instance() != null) LobbySceneUIMgr.Instance().playerClass = playerClass;

				if (arg0.buildIndex != 2) return;

				GameObject existingPlayer = GameObject.Find(Constants.NAME_Player);
				if (existingPlayer != null)
				{
						Destroy(existingPlayer);
				}

		playerClass = LobbySceneUIMgr.Instance().playerClass;
				Soldier playerPrefab =  playerClassScriptableObject.GetClassDataByClassName(playerClass).soldierPrefab;
      Soldier player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.name = Constants.NAME_Player;
        player.level = Mathf.Max(PlayerPrefs.GetInt(Constants.CLASS_PLAYER),1);
		player.GetComponent<Player_Soldier>().isAuto = LobbySceneUIMgr.Instance().isAuto;
		}

 
  
		
}
