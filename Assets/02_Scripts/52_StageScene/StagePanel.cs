using System;
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
        int maxInt = Enum.GetValues(typeof(Level))
                         .Cast<int>()
                         .Max();
        for (int i = 0; i < maxInt; ++i)
        {
            LoadBattleSceneBtn stageBtn = Instantiate(UpdateStageButton((int)stageWorld, i + 1, maxInt), transform);
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
        // 첫 번째 스테이지는 항상 열려 있음
        if (world == 1 && level == 1) return true;

        // 같은 월드 내에서 이전 레벨 클리어 여부 확인
        if (level > 1)
        {
            return IsStageCleared(world, level - 1);
        }
        else
        {
            // 이전 월드의 마지막 레벨 클리어 여부 확인
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

}
