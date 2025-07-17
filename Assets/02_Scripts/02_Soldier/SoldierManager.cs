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
				foreach (GameObject team in teamArray)
				{
						teamSoldiers.Add(team.GetComponent<Soldier>());
				}
				isBattle = true;

#if UNITY_ANDROID
				UIManager.Instance().joystickPanel.gameObject.SetActive(true);
#endif
		}

		public void Update()
		{
				if (!isBattle) return;
				enemySoldiers.RemoveAll(soldier => soldier == null);
				teamSoldiers.RemoveAll(soldier => soldier == null);

				if (teamSoldiers.Count == 0)
				{
						UIManager.Instance().defeatImage.gameObject.SetActive(true);

						Debug.Log("ÆÐ¹è");
						isBattle = false;

				}
				else if (enemySoldiers.Count == 0)
				{
						UIManager.Instance().victoryImage.gameObject.SetActive(true);
						Debug.Log("½Â¸®");
						isBattle = false;
				}


		}


}
