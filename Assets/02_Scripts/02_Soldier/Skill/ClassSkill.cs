using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassSkill : MonoBehaviour
{
		protected Soldier soldier;

		public void Awake()
		{
				soldier = GetComponent<Soldier>();
		}

		public abstract void DoSkill(Soldier target);

		public abstract void DoAttack(Soldier target);

		public IEnumerator RemoveShield(int shield, int second, Soldier target)
		{
				yield return new WaitForSeconds(second);
				target.shield -= shield;
				if (target.shield < 0)
				{
						target.shield = 0;
				}
		}

		public void DoDamage(Soldier target, float attackPower)
		{
				if (target.shield > 0)
				{
						target.shield -= Mathf.RoundToInt(attackPower);
						if (target.shield < 0)
						{
								target.currentHp += target.shield;
								target.shield = 0;
						}
				}
				else target.currentHp -= Mathf.RoundToInt(attackPower);
				if (target.currentHp < 0)
				{
						target.DieSoldier();
				}
		}


		public Soldier SearchEnemyTarget(float attackRange)
		{
				Collider[] enemyList = Physics.OverlapSphere(transform.position, attackRange);
				if (enemyList.Length == 0) return null;
				Soldier targetSoldier = null;
				float minDistance = float.MaxValue;
				foreach (Collider enemy in enemyList)
				{
						Soldier enemySoldier = enemy.GetComponent<Soldier>();
						float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
						if (enemySoldier == null || enemy.CompareTag(tag)) continue;
						if (minDistance > enemyDistance)
						{
								targetSoldier = enemySoldier;
								minDistance = enemyDistance;
						}
				}
				return targetSoldier;
		}

		public Soldier SearchEnemyTarget()
		{
				Collider[] enemyList = Physics.OverlapSphere(transform.position, soldier.attackRange);
				if (enemyList.Length == 0) return null;
				Soldier targetSoldier = null;
				float minDistance = float.MaxValue;
				foreach (Collider enemy in enemyList)
				{
						Soldier enemySoldier = enemy.GetComponent<Soldier>();
						float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
						if (enemySoldier == null || enemy.CompareTag(tag)) continue;
						if (minDistance > enemyDistance)
						{
								targetSoldier = enemySoldier;
								minDistance = enemyDistance;
						}
				}
				return targetSoldier;
		}
		public Soldier SearchEnemyTarget(Vector3 mousePosition)
		{
				Collider[] enemyList = Physics.OverlapSphere(transform.position, soldier.attackRange);
				if (enemyList.Length == 0) return null;
				Soldier targetSoldier = null;
				float minDistance = float.MaxValue;
				foreach (Collider enemy in enemyList)
				{
						Soldier enemySoldier = enemy.GetComponent<Soldier>();
						float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
						if (enemySoldier == null || enemy.CompareTag(tag)) continue;
						if (minDistance > enemyDistance)
						{
								targetSoldier = enemySoldier;
								minDistance = enemyDistance;
						}
				}
				return targetSoldier;
		}

		public List<Soldier> SearchEnemyTarget(Soldier attackedSoldier, int repeat)
		{
				List<Soldier> attackedSoldierList = new List<Soldier>();
				attackedSoldierList.Add(attackedSoldier);
				Soldier searchSoldier = attackedSoldier;
				for (int i = 0; i < repeat; i++)
				{
				Collider[] enemyList = Physics.OverlapSphere(searchSoldier.transform.position, soldier.attackRange);

						if (enemyList.Length == 0) return null;
						Soldier targetSoldier = null;
						float minDistance = float.MaxValue;
						foreach (Collider enemy in enemyList)
						{
								Soldier enemySoldier = enemy.GetComponent<Soldier>();
								float enemyDistance = Vector3.Distance(searchSoldier.transform.position, enemy.transform.position);

								if (enemySoldier == null || enemy.CompareTag(tag) || attackedSoldierList.Contains(enemySoldier)) continue;
								if (minDistance > enemyDistance)
								{
										targetSoldier = enemySoldier;
										minDistance = enemyDistance;
								}
						}
						if (targetSoldier != null)
						{
								attackedSoldierList.Add(targetSoldier);
								searchSoldier = targetSoldier;	
						}
						else break;
				}
				return attackedSoldierList;
		}

		public IEnumerator TargetStun(Soldier target, float stunTime, GameObject effect)
		{
				FSM targetFSM = target.GetComponent<FSM>();
				GameObject stunEffect = Instantiate(effect, target.transform.position, Quaternion.identity);
				targetFSM.isStun = true;
				yield return new WaitForSeconds(stunTime);
				targetFSM.isStun = false;
				Destroy(stunEffect.gameObject);

		}

}
