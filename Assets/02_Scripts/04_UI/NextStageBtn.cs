using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageBtn : BtnUI
{
    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        button.onClick.AddListener(CALLBACK_NextStage);
        AddBtnAnim();
    }

    public void CALLBACK_NextStage()
    {
				Time.timeScale = 1f;

				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				if (PlayerPrefs.GetString(Constants.ENEMYSCENE) != null)
						SceneManager.LoadScene(PlayerPrefs.GetString(Constants.ENEMYSCENE), LoadSceneMode.Additive);
		}
}
