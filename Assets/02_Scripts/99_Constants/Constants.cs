using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    /// <summary>
    /// 태그
    /// </summary>
    public static string TAG_ENEMY = "Enemy";
    public static string TAG_TEAM = "Team";
    public static string NAME_Player = "Player";



    /// <summary>
    /// 클래스
    /// </summary>
    public static string CLASS_TANKER = "Tanker";
    public static string CLASS_WARRIOR = "Warrior";
    public static string CLASS_ASSASSIN = "Assassin";
    public static string CLASS_ARCHER = "Archer";
    public static string CLASS_HEALER = "Healer";
    public static string CLASS_MAGICIAN = "Magician";


    /// <summary>
    /// 최대치
    /// </summary>
    public static int maxLevel = 50;

    public static float AttackTime = 2f;
    public static float skillCoolTime = 5f;
    public static float ultiCoolTime = 30f;


    /// <summary>
    /// 계수
    /// </summary>
    public static float Multi_HP = 5.0f;
}
