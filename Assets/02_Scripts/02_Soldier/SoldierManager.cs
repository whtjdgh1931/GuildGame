using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    public List<Soldier> enemySoldiers;

    public List<Soldier> teamSoldiers;

    public bool isBattle = false;

    public void StartBattle()
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag(Constants.TAG_ENEMY);

        foreach (GameObject enemy in enemyArray)
        {
            enemySoldiers.Add(enemy.GetComponent<Soldier>());
        }

        GameObject[] teamArray = GameObject.FindGameObjectsWithTag(Constants.TAG_TEAM);
        foreach(GameObject team in teamArray)
        {
            teamSoldiers.Add(team.GetComponent<Soldier>());
        }
        teamSoldiers.Add(GameObject.FindGameObjectWithTag(Constants.TAG_Player).GetComponent<Soldier>());

        
        isBattle = true;
    }


}
