using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player_Archer : Player_Skill
{
		public Arrow arrowPrefab;
		public GameObject skillEffect;
		private FSM player_FSM;
		private Player_Attack player_Attack;

		public void Awake()
		{
				base.Awake();
				player_FSM = GetComponent<FSM>();
				player_Attack = GetComponent<Player_Attack>();
		}

		public override void DoAttack(Soldier target)
		{
				Arrow arrow = Instantiate(arrowPrefab);
				arrow.target = target;
				arrow.transform.position = transform.position;
				arrow.arrowPower = soldier.attackPower;
				arrow.gameObject.tag = gameObject.tag;

		}
		

		public override void DoSkill(Vector3 mousePosition)
		{
				StartCoroutine(StartSkillEffect(2f));
		}

		public override void DoSkill(Soldier target)
		{
				StartCoroutine(StartSkillEffect(2f));

		}

		public override void DoUlti(Vector3 mousePosition)
		{
				Arrow arrow = Instantiate(arrowPrefab);
				Soldier target = SearchEnemyTarget(mousePosition);
				arrow.target = target;
				arrow.transform.position = transform.position;
				arrow.arrowPower = soldier.attackPower*3f;
				arrow.transform.localScale = Vector3.one * 2f;

				arrow.gameObject.tag = gameObject.tag;

		}

		public override void DoUlti(Soldier target)
		{
				Arrow arrow = Instantiate(arrowPrefab);
				arrow.target = target;
				arrow.transform.position = transform.position;
				arrow.arrowPower = soldier.attackPower* 3f;
				arrow.transform.localScale = Vector3.one * 2f;
				arrow.gameObject.tag = gameObject.tag;

		}

		public IEnumerator StartSkillEffect(float seconds)
		{
				GameObject skillEffect = Instantiate(this.skillEffect,transform.position,Quaternion.identity);
				player_FSM.SetAttackSpeed(1/1.5f);
				player_Attack.SetAttackSpeed(1 / 1.5f);
				yield return new WaitForSeconds(seconds);
				player_FSM.SetAttackSpeed(1.5f);
				player_Attack.SetAttackSpeed(1.5f);
				Destroy(skillEffect.gameObject);
		}
}
