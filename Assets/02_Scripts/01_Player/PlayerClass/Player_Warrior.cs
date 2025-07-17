using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Warrior : Player_Skill
{
		public GameObject skillEffectPrefab;

		public override void DoAttack(Soldier target)
		{
				DoDamage(target, soldier.attackPower);
		}

		public override void DoSkill(Vector3 mousePosition)
		{
				Collider[] enemyList = Physics.OverlapSphere(transform.position, 15f);
				foreach (Collider enemy in enemyList)
				{
						Soldier targetEnemy = enemy.GetComponent<Soldier>();
						if (targetEnemy == null || targetEnemy.CompareTag(soldier.tag)) continue;
						DoDamage(targetEnemy, soldier.attackPower * 2);

				}

				GameObject skillEffect = Instantiate(skillEffectPrefab, transform.position, Quaternion.identity);
				skillEffect.transform.localScale = new Vector3(6f, 1, 6f);
				Destroy(skillEffect, 1f);
		}

		public override void DoSkill(Soldier target)
		{
				throw new System.NotImplementedException();
		}

		public override void DoUlti(Vector3 mousePosition)
		{
				throw new System.NotImplementedException();
		}

		public override void DoUlti(Soldier target)
		{
				throw new System.NotImplementedException();
		}

		
}
