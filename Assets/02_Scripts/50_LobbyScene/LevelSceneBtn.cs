using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneBtn : BtnUI
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        button.onClick.AddListener(CALLBACK_LevelSceneBtnClicked);
    }

    private void CALLBACK_LevelSceneBtnClicked()
    {
        SceneManager.LoadScene(Constants.LEVELSCENE);
    }
}
