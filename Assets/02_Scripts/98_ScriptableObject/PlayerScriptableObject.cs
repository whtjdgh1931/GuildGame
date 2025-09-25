using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerClass", menuName = "ScriptableObject/PlayerClass", order = int.MaxValue)]
public class PlayerScriptableObject : ScriptableObject
{
    [Header("# 클래스 데이터 리스트")]
    public List<PlayerClass> playerClasses;


    public PlayerClass GetClassDataByClassName(string className)
            => playerClasses.Find(classData => classData.className == className);
}
[System.Serializable]
public class PlayerClass
{
    [Header("# 클래스 이름")]
    public string className;

    [Header("# 클래스 프리팹")]
    public Soldier soldierPrefab;

    [Header("# 클래스 로고")]
    public Sprite soldierLogoPrefab;

    [Header("# 클래스 이미지")]
    public Sprite classImage;

}

