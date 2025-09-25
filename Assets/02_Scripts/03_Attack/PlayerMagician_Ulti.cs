using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMagician_Ulti : MonoBehaviour
{

		private List<Soldier> slowedEnemies = new List<Soldier>();

		private void OnTriggerStay(Collider other)
		{
				Soldier enemy = other.GetComponent<Soldier>();
				if (enemy != null && enemy.tag != tag)
				{
						if (!slowedEnemies.Contains(enemy))
						{
								slowedEnemies.Add(enemy);
								enemy.GetComponent<NavMeshAgent>().speed = enemy.moveSpeed * 0.5f;
						}
				}
		}

		private void OnTriggerExit(Collider other)
		{
				Soldier enemy = other.GetComponent<Soldier>();
				if (enemy != null && slowedEnemies.Contains(enemy))
				{
						enemy.GetComponent<NavMeshAgent>().speed = enemy.moveSpeed;
						slowedEnemies.Remove(enemy);
				}
		}

		private void OnDisable()
		{
				RestoreAll();
		}

		private void OnDestroy()
		{
				RestoreAll();
		}

		private void RestoreAll()
		{
				foreach (Soldier enemy in slowedEnemies)
				{
						if (enemy != null)
						{
								NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
								if (agent != null)
								{
										agent.speed = enemy.moveSpeed;
								}
						}
				}
				slowedEnemies.Clear();
		}

public void SearchAndHitEnemy(Soldier soldier,Soldier target)
		{
				tag = soldier.tag;
				Collider[] hitEnemies = Physics.OverlapSphere(target.transform.position, 6f);
				foreach (Collider hitEnemy in hitEnemies)
				{
						if (hitEnemy.GetComponent<Soldier>() == null || hitEnemy.CompareTag(soldier.tag)) continue;
						Soldier targetSoldier = hitEnemy.GetComponent<Soldier>();

						if (targetSoldier.shield > 0)
						{
								targetSoldier.shield -= Mathf.RoundToInt(soldier.attackPower * 2f);
								if (targetSoldier.shield < 0)
								{
										targetSoldier.currentHp += targetSoldier.shield;
										targetSoldier.shield = 0;
								}
						}
						else targetSoldier.currentHp -= Mathf.RoundToInt(soldier.attackPower * 2f);
						if (targetSoldier.currentHp < 0)
						{
								targetSoldier.DieSoldier();
						}
				}

				Destroy(gameObject, 2f);
		}

}
