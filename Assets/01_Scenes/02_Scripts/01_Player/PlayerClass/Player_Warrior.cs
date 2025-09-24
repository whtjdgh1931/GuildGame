using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Warrior : Player_Skill
{
		public GameObject skillEffectPrefab;
		public GameObject stunEffectPrefab;

		public override void DoAttack(Soldier target)
		{
				DoDamage(target, soldier.attackPower);
		}

		public override void DoSkill(Vector3 mousePosition)
		{
				Collider[] enemyList = Physics.OverlapSphere(transform.position, 6f);
				foreach (Collider enemy in enemyList)
				{
						Soldier targetEnemy = enemy.GetComponent<Soldier>();
						if (targetEnemy == null || targetEnemy.CompareTag(soldier.tag)) continue;
						DoDamage(targetEnemy, soldier.attackPower * 1.5f);

				}  

				GameObject skillEffect = Instantiate(skillEffectPrefab, transform.position, Quaternion.identity);
				skillEffect.transform.localScale = new Vector3(6f,6f,6f);
				Destroy(skillEffect, 1f);
		}

		public override void DoSkill(Soldier target)
		{
				Collider[] enemyList = Physics.OverlapSphere(transform.position, 6f);
				foreach (Collider enemy in enemyList)
				{
						Soldier targetEnemy = enemy.GetComponent<Soldier>();
						if (targetEnemy == null || targetEnemy.CompareTag(soldier.tag)) continue;
						DoDamage(targetEnemy, soldier.attackPower * 1.5f);

				}

				GameObject skillEffect = Instantiate(skillEffectPrefab, transform.position, Quaternion.identity);
				skillEffect.transform.localScale = new Vector3(6f, 1, 6f);
				Destroy(skillEffect, 1f);
		}

		public override void DoUlti(Vector3 mousePosition)
		{
				Soldier targetEnemy = SearchEnemyTarget();
				DoDamage(targetEnemy,soldier.attackPower * 2);
				StartCoroutine(TargetStun(targetEnemy, 1f, stunEffectPrefab));


		}

		public override void DoUlti(Soldier target)
		{
				
				DoDamage(target, soldier.attackPower * 2);
				StartCoroutine(TargetStun(target,1f, stunEffectPrefab));
		}

	
		
}
