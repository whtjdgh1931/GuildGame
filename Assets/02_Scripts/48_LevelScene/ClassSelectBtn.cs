using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSelectBtn : BtnUI
{

    public String className;
    public Image classImage;
    public void Awake()
    {
        classImage = GetComponentInChildren<Image>();

    }
    public new void Start()
    {
        base.Start();


        button.onClick.AddListener(CALLBACK_ClassSelectBtnClicked);
    }

    private void CALLBACK_ClassSelectBtnClicked()
    {
        LevelSceneUIManager.Instance().SetClassDataInfo(className,classImage.sprite);
    }

    public void SetClassDataInfo()
    {
       
    }


}
