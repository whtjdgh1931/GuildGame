using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public static class Constants
{
    /// <summary>
    /// 태그
    /// </summary>
    public static string TAG_ENEMY = "Enemy";
    public static string TAG_TEAM = "Team";
    public static string NAME_Player = "Player";
    public static string GAMESPEED = "GameSpeed";
    public static string ENEMYSCENE = null;
    public static string LOBBYSCENE = "LobbyScene";
    public static string BATTLESCENE = "BattleScene";
    public static string LEVELSCENE = "LevelScene";



        /// <summary>
        /// 클래스
        /// </summary>
        public static string CLASS_TANKER = "Tanker";
    public static string CLASS_WARRIOR = "Warrior";
    public static string CLASS_ASSASSIN = "Assassin";
    public static string CLASS_ARCHER = "Archer";
    public static string CLASS_HEALER = "Healer";
    public static string CLASS_MAGICIAN = "Magician";

    public static string CLASS_PLAYER_LEVEL = "PLAYER_Level";
    public static string CLASS_PLAYER_EXP = "PLAYER_Exp";
    public static string CLASS_PLAYER_CLASS = "PLAYER_Class";


    /// <summary>
    /// 최대치
    /// </summary>
    public static int maxLevel = 50;
    public static int minLevel = 1;

    public static float AttackTime = 2f;
    public static float skillCoolTime = 5f;
    public static float ultiCoolTime = 30f;


    /// <summary>
    /// 계수
    /// </summary>
    public static float Multi_HP = 5.0f;
}

public enum World
{
		World1 = 1,
		World2 = 2,
		World3 = 3,
    World4 = 4,
    World5 = 5,
    World6 = 6,
    World7 = 7,
    World8 = 8,
    World9 = 9,
    World10 = 10,
    World11 = 11,
    World12 = 12,
    World13 = 13,
    World14 = 14,
    World15 = 15,
    World16 = 16,
    World17 = 17,
    World18 = 18,
		// 계속 확장 가능
}

public enum Level
{
		Level1 = 1,
		Level2 = 2,
		Level3 = 3,
    Level4 = 4,
    Level5 = 5,
    Level6 = 6,
    Level7 = 7,
    Level8 = 8,
    Level9 = 9,
    Level10 = 10,
    Level11 = 11,
    Level12 = 12,
    Level13 = 13,
    Level14 = 14,
    Level15 = 15,
    Level16 = 16,
    Level17 = 17,
    Level18 = 18,
		// 계속 확장 가능
}

public static class StageHelper
{
		public static string ToSceneName(World world, Level level)
		{
				// "1-2" 형태로 변환
				return $"{(int)world}-{(int)level}";
		}


}


