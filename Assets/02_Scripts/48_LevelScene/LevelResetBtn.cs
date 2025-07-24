using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetBtn : BtnUI
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        button.onClick.AddListener(CALLBACK_LevelResetBtnClicked);
    }

    private void CALLBACK_LevelResetBtnClicked()
    {
        LevelSceneUIManager.Instance().SetClassDataInfo();
    }

    
}
