using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSceneLoad : MonoBehaviour
{
    public PlayerScriptableObject playerClassScriptableObject;

    public string playerClass = Constants.CLASS_TANKER;

    public Image playerImage;

    public bool isAuto;

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

				playerImage.sprite = playerClassScriptableObject.GetClassDataByClassName(playerClass).classImage;

		}

		private void CALLBACK_MakePlayer(Scene arg0, LoadSceneMode arg1)
		{

				if (arg0.buildIndex != 2) return;

        Debug.Log("플레이어 생성");
				GameObject existingPlayer = GameObject.Find(Constants.NAME_Player);
				if (existingPlayer != null)
				{
						Destroy(existingPlayer);
				}

				Soldier playerPrefab =  playerClassScriptableObject.GetClassDataByClassName(playerClass).soldierPrefab;
      Soldier player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.name = Constants.NAME_Player;
        player.level = PlayerPrefs.GetInt(Constants.CLASS_PLAYER);
        player.GetComponent<Player_Soldier>().isAuto = this.isAuto;
		}

    

    public void SetPlayerClassString(string playerClass)
    {
        this.playerClass = playerClass;
        playerImage.sprite = playerClassScriptableObject.GetClassDataByClassName(playerClass).classImage;
				
				

		}

    public void SetAuto(bool auto)
    { this.isAuto = auto; }


		
}
