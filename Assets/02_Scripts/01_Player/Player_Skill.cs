using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Skill : ClassSkill
{

    public abstract void DoSkill(Vector3 mousePosition);




    public abstract void DoUlti(Vector3 mousePosition);

		public abstract void DoUlti(Soldier target);


		public Soldier SearchEnemyTarget(float attackRange)
    {
				Collider[] enemyList = Physics.OverlapSphere(transform.position, attackRange);
				if (enemyList.Length == 0) return null;
				Soldier targetSoldier = null;
				float minDistance = float.MaxValue;
				foreach (Collider enemy in enemyList)
				{
						if (enemy.GetComponent<Soldier>() == null || enemy.CompareTag(tag)) continue;
						if (minDistance > Vector3.Distance(transform.position,enemy.transform.position))
						{
								targetSoldier = enemy.GetComponent<Soldier>();
								minDistance= Vector3.Distance(transform.position, enemy.transform.position);
						}
				}
				return targetSoldier;
		}

		
}
