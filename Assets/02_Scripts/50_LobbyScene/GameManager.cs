using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    //[Header("UI References")]
    //public Text playerClassLevel;
    //public Image playerImage;
    //public InputField playerLevelInputField;

    [Header("Game References")]
    
    public PlayerScriptableObject playerClassScriptableObject;

    [Header("Player Data")]
    public string playerClass;
    public bool isAuto;

    private string _stageKey;
    public string stageKey { get { return _stageKey; } }
    public void SetStageKey(string stageKey)
    {
        _stageKey = stageKey;
    }

    public static GameManager GetInstance()
    {
        if (Instance == null)
        {
            // 새 GameObject를 만들어 붙임
            GameObject go = new GameObject("GameManager");
            Instance = go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
        return Instance;
    }


    private void Awake()
    {
        // 싱글톤 보장
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // PlayerPrefs에서 클래스/레벨 불러오기
        if (string.IsNullOrEmpty(playerClass))
            playerClass = PlayerPrefs.GetString(Constants.CLASS_PLAYER_CLASS, Constants.CLASS_TANKER);

        PlayerPrefs.SetInt(Constants.CLASS_PLAYER_LEVEL,
            Mathf.Max(PlayerPrefs.GetInt(Constants.CLASS_PLAYER_LEVEL), 1));

        SetPlayerClassString(playerClass);
    }

    public void SetPlayerClassString(string playerClass)
    {
        this.playerClass = playerClass;

        //// UI 업데이트
        //playerImage.sprite = playerClassScriptableObject
        //    .GetClassDataByClassName(playerClass).soldierLogoPrefab;

        //playerClassLevel.text = "Level : " +
        //    PlayerPrefs.GetInt(Constants.CLASS_PLAYER_LEVEL).ToString();

        // 자동 여부 반영
        SetAuto(PlayerPrefs.GetInt("isAuto") == 1);

        // 클래스 저장
        PlayerPrefs.SetString(Constants.CLASS_PLAYER_CLASS, playerClass);
    }

    public void SetAuto(bool auto)
    {
        isAuto = auto;
        PlayerPrefs.SetInt("isAuto", auto ? 1 : 0);
    }

    public void SetPlayerLevel()
    {
        //string level = playerLevelInputField.text;

        //if (int.TryParse(level, out int parsedLevel))
        //{
        //    int playerLevel = Mathf.Clamp(parsedLevel, Constants.minLevel, Constants.maxLevel);
        //    PlayerPrefs.SetInt(Constants.CLASS_PLAYER_LEVEL, playerLevel);

        //    SetPlayerClassString(playerClass);
        //}
        //else
        //{
        //    //Debug.LogWarning("레벨 문자열 파싱 실패: " + level);
        //}
    }
}