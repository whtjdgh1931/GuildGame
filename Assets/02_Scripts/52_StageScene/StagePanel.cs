using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StagePanel : MonoBehaviour
{
    [SerializeField] private World stageWorld;

    [SerializeField] private LoadBattleSceneBtn clearedStage;
    [SerializeField] private LoadBattleSceneBtn lockedStage;
    [SerializeField] private LoadBattleSceneBtn defaultStage;

    public void Start()
    {
        ReleaseStageButtons();

        int maxInt = Enum.GetValues(typeof(Level))
                         .Cast<int>()
                         .Max();
        for (int i = 0; i < maxInt; ++i)
        {
            LoadBattleSceneBtn stageBtn = GameManager.Instance.ObjectPool
                .GetFromPool(UpdateStageButton((int)stageWorld, i + 1, maxInt), transform.position, Quaternion.identity)
                .GetComponent<LoadBattleSceneBtn>();

            stageBtn.transform.SetParent(transform);
            stageBtn.SetStage(stageWorld, (Level)i + 1);
            stageBtn.InitBtn();
        }
    }

    public void SaveStageClear(int world, int level)
    {
        string key = $"Stage_{world}_{level}_Cleared";
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    public bool IsStageCleared(int world, int level)
    {
        string key = $"Stage_{world}_{level}_Cleared";
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    public bool IsStageUnlocked(int world, int level, int maxLevelPerWorld)
    {
        if (world == 1 && level == 1) return true;

        if (level > 1)
        {
            return IsStageCleared(world, level - 1);
        }
        else
        {
            return IsStageCleared(world - 1, maxLevelPerWorld);
        }
    }

    public LoadBattleSceneBtn UpdateStageButton(int world, int level, int maxLevelPerWorld)
    {
        bool cleared = IsStageCleared(world, level);
        bool unlocked = IsStageUnlocked(world, level, maxLevelPerWorld);

        if (cleared)
        {
            return clearedStage;
        }
        else if (unlocked)
        {
            return defaultStage;
        }
        else
        {
            return lockedStage;
        }
    }

    public void ResetStages(int maxWorld, int maxLevelPerWorld)
    {
        for (int w = 1; w <= maxWorld; w++)
        {
            for (int l = 1; l <= maxLevelPerWorld; l++)
            {
                string key = $"Stage_{w}_{l}_Cleared";
                PlayerPrefs.DeleteKey(key);
            }
        }
    }

    /// <summary>
    /// 현재 패널에 존재하는 스테이지 버튼들을 모두 반환하는 함수
    /// </summary>
    public void ReleaseStageButtons()
    {
        List<GameObject> stageButtons = new List<GameObject>();

        foreach (Transform child in transform)
        {
            LoadBattleSceneBtn stageBtn = child.GetComponent<LoadBattleSceneBtn>();
            if (stageBtn != null)
            {
                stageButtons.Add(stageBtn.gameObject);
            }
        }

        for (int i = 0; i < stageButtons.Count; i++)
        {
            GameManager.Instance.ObjectPool.ReturnToPool(stageButtons[i]);
        }
    }
}
