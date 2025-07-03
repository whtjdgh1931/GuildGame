using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Soldier_Condition
{
		NONE,
		SEARCH,
		MOVE,
		ATTACK
}

public class FSM : MonoBehaviour
{
		//이동 컴포넌트
    private Soldier_Move solider_Move;


		// 현재 솔져 상태 
		[SerializeField]
		private Soldier_Condition condition;

		// 타겟팅 된 적 솔져
		[SerializeField]
		private Soldier targetSoldier;


		// 리스트를 가지고 있는 솔져매니저
		private SoldierManager soldierManager;

		// 상태
		private Soldier soldier;
		

		

		public void Awake()
		{
				solider_Move = GetComponent<Soldier_Move>();
				soldierManager = FindObjectOfType<SoldierManager>();
				soldier = GetComponent<Soldier>();
		}

		public void Start()
		{
				condition = Soldier_Condition.SEARCH;
		}

		public void Update()
		{
				if(soldierManager.isBattle)
				DoFsm();
		}

		public void DoFsm()
		{
				if (targetSoldier == null) condition = Soldier_Condition.SEARCH;
				switch (condition)
				{
						case Soldier_Condition.NONE:
								break;
						case Soldier_Condition.SEARCH:
								SearchEnemy(tag);

								break;
						case Soldier_Condition.MOVE:
								SoldierMove();
								break;
						case Soldier_Condition.ATTACK:
								SoldierAttack();
								break;
				}
		}
				
		/// <summary>
		/// 타겟 서칭
		/// </summary>
		/// <param name="tag">자신의 진영</param>
		public void SearchEnemy(string tag)
		{
				
				
				float targetDistacne = int.MaxValue;
				if(tag == Constants.TAG_TEAM)
				{
						foreach(Soldier enemy in soldierManager.enemySoldiers)
						{
								float distance = Vector3.Distance(transform.position, enemy.transform.position);
								if(distance < targetDistacne)
								{
										targetSoldier = enemy;
										targetDistacne = distance;
								}
								
						}

				}
				else if(tag == Constants.TAG_ENEMY)
				{
						foreach (Soldier team in soldierManager.teamSoldiers)
						{
								float distance = Vector3.Distance(transform.position, team.transform.position);
								if (distance < targetDistacne)
								{
										targetSoldier = team;
										targetDistacne = distance;
								}
						}
				}

				condition = Soldier_Condition.MOVE;

		}

		/// <summary>
		/// 전투원 이동
		/// </summary>
		public void SoldierMove()
		{
				solider_Move.SetNavDestination(targetSoldier.transform);
				if (Vector3.Distance(transform.position,targetSoldier.transform.position) < soldier.attackRange)
				{
						solider_Move.SetNavDestination(transform);
						condition = Soldier_Condition.ATTACK;
				}
		}

		/// <summary>
		/// 전투원 공격
		/// </summary>
		public void SoldierAttack()
		{
				Debug.Log("Attack");
		}

}
