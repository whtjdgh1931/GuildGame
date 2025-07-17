using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player_Tanker : Player_Skill
{
		public Vector3 targetPos;
		public bool isDash;

		private BoxCollider playerCollider;

		public override void DoAttack(Soldier target)
		{
				if (target.shield > 0)
				{
						target.shield -= soldier.attackPower;
						if (target.shield < 0)
						{
								target.currentHp += target.shield;
								target.shield = 0;
						}
				}
				else target.currentHp -= soldier.attackPower;
				if (target.currentHp < 0)
				{
						target.DieSoldier();
				}
		}


		public override void DoSkill(Vector3 mousePosition)
		{
				isDash = true;
				Vector3 skillDir = mousePosition - transform.position;
				float length = skillDir.magnitude;
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
				Debug.Log("PlayerSkill");
		}



		public override void DoUlti(Vector3 mousePosition)
		{
				throw new System.NotImplementedException();
		}

		public override void DoUlti(Soldier target)
		{
				Debug.Log("PlayerUlti");
		}

		public void Update()
		{
				if (!isDash) return;
				transform.position = Vector3.MoveTowards(transform.position, targetPos, soldier.moveSpeed * 50f * Time.deltaTime);

		}

		public IEnumerator DashEnd()
		{
				yield return new WaitForSeconds(0.5f);
				isDash = false;
		}

		public void OnTriggerEnter(Collider other)
		{
						if (!isDash) return;
				if (other.gameObject.CompareTag(Constants.TAG_ENEMY))
				{
						Soldier target = other.gameObject.GetComponent<Soldier>();
						if (target.shield > 0)
						{
								target.shield -= soldier.attackPower;
								if (target.shield < 0)
								{
										target.currentHp += target.shield;
										target.shield = 0;
								}
						}
						else target.currentHp -= soldier.attackPower;
						if (target.currentHp < 0)
						{
								target.DieSoldier();
						}
				}
		}

}
