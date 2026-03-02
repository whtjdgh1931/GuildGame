using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    [Header("Object Pool")]
    private ObjectPool objectPool;
    public ObjectPool ObjectPool { get { return objectPool; } private set { objectPool = value; } }

    [SerializeField]
    [Header("Game References")]
    private PlayerScriptableObject playerClassScriptableObject;
    public PlayerScriptableObject PlayerClassScriptableObject { get { return playerClassScriptableObject; } private set { playerClassScriptableObject = value; } }

    [Header("Player Data")]
    public string playerClass;
    public bool isAuto;

    #region Stage
    [Header("Stage")]
    private string _stageKey;
    [SerializeField] World _world;
    [SerializeField] Level _level;
    public string stageKey { get { return _stageKey; } }

    public void SetStageKey(World world, Level level)
    {
        _world = world;
        _level = level;
        _stageKey = $"Stage_{(int)world}_{(int)level}_Cleared";
    }

    public string NextStage()
    {
        if (_level == Level.Level18)
        {
            _world++;
            _level = Level.Level1;
        }
        else
        {
            _level++;
        }

        _stageKey = $"Stage_{(int)_world}_{(int)_level}_Cleared";
        return StageHelper.ToSceneName(_world, _level);
    }
    #endregion

    #region PLAYER_Level
    [SerializeField] private int _playerLevel;
    public int playerLevel { get { return _playerLevel; } }

    public void SetPlayerLevel(int playerLevel)
    {
        _playerLevel = playerLevel;
        PlayerPrefs.SetInt(Constants.CLASS_PLAYER_LEVEL, _playerLevel);
    }

    [SerializeField] private int _playerLevelupExp;
    public int playerLevelupExp { get { return _playerLevelupExp; } }

    [SerializeField] private int _playerExp;
    public int playerExp { get { return _playerExp; } }

    public void SetPlayerExp(int exp)
    {
        _playerExp = exp;
        while (_playerExp > _playerLevelupExp)
        {
            LevelUp();
        }
        PlayerPrefs.SetInt(Constants.CLASS_PLAYER_EXP, _playerExp);
    }

    public void PlusExp(int exp)
    {
        _playerExp += exp;
        PlayerPrefs.SetInt(Constants.CLASS_PLAYER_EXP, _playerExp);
        if (_playerExp >= _playerLevelupExp)
        {
            LevelUp();
        }
    }

    public void PlusExpByStage()
    {
        PlusExp((int)_world * (int)_level);
    }

    public void LevelUp()
    {
        _playerLevel++;
        PlayerPrefs.SetInt(Constants.CLASS_PLAYER_LEVEL, _playerLevel);
        _playerExp -= _playerLevelupExp;
        PlayerPrefs.SetInt(Constants.CLASS_PLAYER_EXP, _playerExp);
        SetLevelUpExp();
    }

    public void SetLevelUpExp()
    {
        _playerLevelupExp = _playerLevel * 10;
    }

    public float GetExpRatio()
    {
        return (float)_playerExp / (float)_playerLevelupExp;
    }
    #endregion

    public static GameManager GetInstance()
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("GameManager");
            Instance = go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
        return Instance;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (objectPool == null)
        {
            objectPool = FindAnyObjectByType<ObjectPool>();
        }
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(playerClass))
        {
            playerClass = PlayerPrefs.GetString(Constants.CLASS_PLAYER_CLASS, Constants.CLASS_TANKER);
        }

        _playerLevel = PlayerPrefs.GetInt(Constants.CLASS_PLAYER_LEVEL, 1);
        SetLevelUpExp();
        _playerExp = PlayerPrefs.GetInt(Constants.CLASS_PLAYER_EXP, 0);
        SetPlayerClassString(playerClass);
    }

    public void SetPlayerClassString(string playerClass)
    {
        this.playerClass = playerClass;
        SetAuto(PlayerPrefs.GetInt("isAuto") == 1);
        PlayerPrefs.SetString(Constants.CLASS_PLAYER_CLASS, playerClass);
    }

    public void SetAuto(bool auto)
    {
        isAuto = auto;
        PlayerPrefs.SetInt("isAuto", auto ? 1 : 0);
    }
}
