using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBattleSceneBtn : BtnUI,IPoolable,IReleasePoolable
{
		[SerializeField] private World _world;
		[SerializeField] private Level _level;
		[SerializeField] TextMeshProUGUI stageNum;

		

		public void InitBtn()
		{
				base.Start();
				
				button.onClick.AddListener(CALLBACK_LoadBattleSceneBtnClicked);
				AddBtnAnim();
		}

		private void CALLBACK_LoadBattleSceneBtnClicked()
		{
				World selectedWorld = _world;
				Level selectedLevel = _level;
				string sceneName = StageHelper.ToSceneName(selectedWorld, selectedLevel);

				StagePanel stagePanel = GetComponentInParent<StagePanel>();
				if (stagePanel != null)
				{
						stagePanel.ReleaseStageButtons();
				}

				GameManager.GetInstance().SetStageKey(selectedWorld, selectedLevel);
				PlayerPrefs.SetString(Constants.ENEMYSCENE, sceneName);

				SceneManager.LoadScene(Constants.BATTLESCENE);
				SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		}

		public void SetStage(World world, Level level)
		{
				_world = world;
				_level = level;

				if (stageNum != null)
				{

						stageNum.text = "Stage" + "\n"+ ((int)level).ToString();
				}
		}

    public void OnGetFromPool(Vector3 position, Quaternion rotation)
    {
		transform.position = position;
		transform.rotation = rotation;
    }

    public void ReleaseObjectPool()
    {
		if (button != null)
		{
				button.onClick.RemoveAllListeners();
		}
       _world = World.NONE;
				_level = Level.NONE;
				if (stageNum != null)
				{
						stageNum.text = "";
				}
    }
}
