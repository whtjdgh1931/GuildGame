using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player_Tanker : Player_Skill
{
		public Vector3 targetPos;
		public bool isDash;


		public GameObject shieldGameObject;

		public GameObject tauntEffectPrefab;
		public override void DoAttack(Soldier target)
		{
				DoDamage(target, soldier.attackPower);
		}


		public override void DoSkill(Vector3 mousePosition)
		{
				isDash = true;
				Vector3 skillDir = mousePosition - transform.position;
				float length = skillDir.magnitude * 0.9f;
				skillDir.Normalize();
				skillDir *= Mathf.Min(soldier.skillRange, length);
				targetPos = transform.position + skillDir;
				StartCoroutine(DashEnd());
				int shield = Mathf.RoundToInt((soldier.maxHp * 0.5f));
				soldier.shield += shield;
				StartCoroutine(RemoveShield(shield, 2, soldier));

		}

		public override void DoSkill(Soldier target)
		{
				isDash = true;
				Vector3 skillDir = target.transform.position - transform.position;
				float length = skillDir.magnitude * 0.9f;
				skillDir.Normalize();
				skillDir *= Mathf.Min(soldier.skillRange, length);
				targetPos = transform.position + skillDir;
				StartCoroutine(DashEnd());
				int shield = Mathf.RoundToInt((soldier.maxHp * 0.5f));
				soldier.shield += shield;
				StartCoroutine(RemoveShield(shield, 2, soldier));
		}



		public override void DoUlti(Vector3 mousePosition)
		{
				Collider[] enemyList = Physics.OverlapSphere(transform.position, 15f);
				foreach (Collider enemy in enemyList)
				{
						FSM enemyFSM = enemy.GetComponent<FSM>();
						if (enemyFSM == null || enemy.CompareTag(soldier.tag)) continue;
						StartCoroutine(Taunt(enemyFSM));

				}

				GameObject tauntEffect = Instantiate(tauntEffectPrefab, transform.position, Quaternion.identity);
				tauntEffect.transform.localScale = new Vector3(15f, 1, 15f);
				Destroy(tauntEffect, 1f);

		}

		public override void DoUlti(Soldier target)
		{
				Collider[] enemyList = Physics.OverlapSphere(transform.position, 15f);
				foreach (Collider enemy in enemyList)
				{
						FSM enemyFSM = enemy.GetComponent<FSM>();
						if (enemyFSM == null || enemy.CompareTag(soldier.tag)) continue;
						StartCoroutine(Taunt(enemyFSM));

				}

				GameObject tauntEffect = Instantiate(tauntEffectPrefab, transform.position, Quaternion.identity);
				tauntEffect.transform.localScale = new Vector3(15f, 1, 15f);
				Destroy(tauntEffect, 1f);
		}

		public void Update()
		{
				if (soldier.shield > 0) shieldGameObject.SetActive(true);
				else shieldGameObject.SetActive(false);
				if (!isDash) return;
				transform.position = Vector3.MoveTowards(transform.position, targetPos, soldier.moveSpeed * 50f * Time.deltaTime);

		}

		public IEnumerator DashEnd()
		{
				yield return new WaitForSeconds(0.5f);
				isDash = false;
		}

		public IEnumerator Taunt(FSM enemyFSM)
		{
				enemyFSM.SetTargetSoldier(soldier);
				enemyFSM.isTaunt = true;
				yield return new WaitForSeconds(2f);
				enemyFSM.isTaunt = false;
				enemyFSM.SetSoldierCondition(Soldier_Condition.SEARCH);
				
		}

		public void OnTriggerEnter(Collider other)
		{
						if (!isDash) return;
						Soldier target = other.gameObject.GetComponent<Soldier>();
				if (other.gameObject.CompareTag(Constants.TAG_ENEMY)&&target!=null)
				{
						DoDamage(target, soldier.attackPower);
				}
		}

}
