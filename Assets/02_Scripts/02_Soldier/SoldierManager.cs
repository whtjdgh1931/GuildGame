using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    public List<Soldier> enemySoldiers;

    public List<Soldier> teamSoldiers;

    public bool isBattle = false;

    private static SoldierManager instance = null;

    private UIManager uiManager;

    public static SoldierManager Instance()
    {
        return instance;
    }
    public void Start()
    {
        if (instance == null) instance = this;
    }

    public void StartBattle()
    {
        uiManager = UIManager.Instance();
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag(Constants.TAG_ENEMY);

        foreach (GameObject enemy in enemyArray)
        {
            enemySoldiers.Add(enemy.GetComponent<Soldier>());
        }

        GameObject[] teamArray = GameObject.FindGameObjectsWithTag(Constants.TAG_TEAM);
        foreach (GameObject team in teamArray)
        {
            teamSoldiers.Add(team.GetComponent<Soldier>());
        }
        isBattle = true;
        Time.timeScale = PlayerPrefs.GetFloat(Constants.GAMESPEED);

#if UNITY_ANDROID
        uiManager.SetJoystick();
#endif

    }

    public void Update()
    {
        if (!isBattle) return;
#if UNITY_ANDROID
        uiManager.SetJoystick();
#endif
        enemySoldiers.RemoveAll(soldier => soldier == null);
        teamSoldiers.RemoveAll(soldier => soldier == null);

        if (teamSoldiers.Count == 0)
        {
            uiManager.gameResultPanel.gameObject.SetActive(true);
            uiManager.defeatImage.gameObject.SetActive(true);
            uiManager.victoryImage.gameObject.SetActive(false);
            uiManager.joystickPanel.gameObject.SetActive(false);


            Time.timeScale = 1.0f;
            isBattle = false;


        }
        else if (enemySoldiers.Count == 0)
        {
            uiManager.gameResultPanel.gameObject.SetActive(true);
            uiManager.defeatImage.gameObject.SetActive(false);

            uiManager.victoryImage.gameObject.SetActive(true);
            uiManager.joystickPanel.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            isBattle = false;
        }


    }


}
