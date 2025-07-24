using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBtn : BtnUI
{

    public float curTime;
    public float coolTime=0.3f;

    public bool isHold = false;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        button.onClick.AddListener(CALLBACK_LevelUpBtnClicked); 
    }

    private void CALLBACK_LevelUpBtnClicked()
    {
        LevelSceneUIManager.Instance().LevelUp();
    }

    public void Update()
    {
        curTime += Time.deltaTime;
        if (!isHold) return;

        if(curTime>coolTime)
        {
            CALLBACK_LevelUpBtnClicked();
            curTime = 0;
        }
        
    }

    public void OnPointerDown()
    {
        isHold = true;
    }

    public void OnPointerUp()
    {
        isHold = false;
    }
    
}
