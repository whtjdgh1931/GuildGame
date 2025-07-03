using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMaker : MonoBehaviour
{
    // 전투원 프리팹
    public Soldier soldierPrefab;
    
    // 전투원 정보 스크립터블오브젝트
    public ClassScriptableObject classScriptableObject;


    public void MakeSoldier(string className)
    {
        ClassData classData = classScriptableObject.GetClassDataByClassName(className);
        Soldier soldier = Instantiate(classData.soldierPrefab);
        soldier.level = PlayerPrefs.GetInt(className);
        
     
    }

    public void MakeTankerButton()
    {
        MakeSoldier(Constants.CLASS_TANKER); 
    }
}
