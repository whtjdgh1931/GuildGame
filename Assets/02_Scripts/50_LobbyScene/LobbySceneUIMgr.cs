using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbySceneUIMgr : MonoBehaviour
{
    private static LobbySceneUIMgr instance;

    public static LobbySceneUIMgr Instance()
    {
        return instance;

    }
    public Text playerClassText;
    public Text playerClassLevel;

    public BattleSceneLoad battleSceneLoad;


    public PlayerScriptableObject playerClassScriptableObject;

    public string playerClass = Constants.CLASS_TANKER;

    public Image playerImage;

    public InputField playerLevelInputField;

    public bool isAuto;

    public void Awake()
    {
        if(instance == null) instance = this;
    }
    public void Start()
    {
        SetPlayerClassString(playerClass);

    }




    public void SetPlayerClassString(string playerClass)
    {
        this.playerClass = playerClass;
        playerImage.sprite = playerClassScriptableObject.GetClassDataByClassName(playerClass).classImage;
        playerClassLevel.text = "PlayerLevel : " +PlayerPrefs.GetInt(Constants.CLASS_PLAYER).ToString();


    }

    public void Update()
		{
				playerClassText.text = playerClass;
		}

    public void SetAuto(bool auto)
    { this.isAuto = auto; }

    public void SetPlayerLevel()
    {
        string level = playerLevelInputField.text;
        // 문자열 level을 정수로 파싱
        if (int.TryParse(level, out int parsedLevel))
        {
            // 유효한 범위로 클램핑
            int playerLevel = Mathf.Clamp(parsedLevel, Constants.minLevel, Constants.maxLevel);

            // 저장
            PlayerPrefs.SetInt(Constants.CLASS_PLAYER, playerLevel);

            SetPlayerClassString(playerClass);
        }
        else
        {
            Debug.LogWarning("레벨 문자열 파싱 실패: " + level);
        }
    }

}
