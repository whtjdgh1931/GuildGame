using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelectBtn : BtnUI
{

    public String className;
    public Vector3Int characterPosition;
    public Sprite classLogoSprite;
  
    public new void Start()
    {
        base.Start();


        button.onClick.AddListener(CALLBACK_ClassSelectBtnClicked);
    }

    private void CALLBACK_ClassSelectBtnClicked()
    {
        LevelSceneUIManager.Instance().SetClassDataInfo(className,characterPosition,classLogoSprite);
    }




}
