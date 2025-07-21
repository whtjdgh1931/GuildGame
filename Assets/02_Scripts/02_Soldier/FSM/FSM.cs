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
		private Soldier_Move soldier_Move;

		// 공격 컴포넌트
		private ClassSkill soldier_Attack;

		// 애니메이터
		private SoldierAnim soldier_Anim;

		// 현재 솔져 상태 
		[SerializeField]
		private Soldier_Condition condition;

		// 타겟팅 된 적 솔져
		[SerializeField]
		public Soldier targetSoldier { get; private set; }


		// 리스트를 가지고 있는 솔져매니저
		private SoldierManager soldierManager;

		// 상태
		private Soldier soldier;

		// 스킬 쿨타임
		public float skillCoolTime;
		public float curTime;

		// 기본 공격 쿨타임
		public float attackSpeed;
		public float curAttackTime;

		// 도발 상태인지
		public bool isTaunt;

		// 스턴 상태인지
		public bool isStun;



		public void Start()
		{
				soldier_Move = GetComponent<Soldier_Move>();
				soldierManager = FindObjectOfType<SoldierManager>();
				soldier = GetComponent<Soldier>();
				soldier_Attack = GetComponent<ClassSkill>();
				soldier_Anim = GetComponentInChildren<SoldierAnim>();

				attackSpeed = Constants.AttackTime / soldier.attackSpeed;
				skillCoolTime = Constants.skillCoolTime;

				curTime = int.MaxValue;
				curAttackTime = int.MaxValue;
				condition = Soldier_Condition.SEARCH;
		}


		public void Update()
		{

				curTime += Time.deltaTime;
				curAttackTime += Time.deltaTime;


				if(soldier_Anim != null)
				soldier_Anim.SetAnimMove(soldier_Move.soldierNav.velocity);


				if (soldierManager.isBattle &&!isStun)
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
				if (tag == Constants.TAG_TEAM)
				{

						foreach (Soldier enemy in soldierManager.enemySoldiers)
						{
								if (enemy == null) continue;
								float distance = Vector3.Distance(transform.position, enemy.transform.position);
								if (distance < targetDistacne)
								{
										targetSoldier = enemy;
										targetDistacne = distance;
								}

						}

				}
				else if (tag == Constants.TAG_ENEMY)
				{
						foreach (Soldier team in soldierManager.teamSoldiers)
						{
								if (team == null) continue;
								float distance = Vector3.Distance(transform.position, team.transform.position);
								if (distance < targetDistacne)
								{
										targetSoldier = team;
										targetDistacne = distance;
								}
						}
				}
				if (targetSoldier != null)
				{
						condition = Soldier_Condition.MOVE;
				}

		}

		/// <summary>
		/// 전투원 이동
		/// </summary>
		public void SoldierMove()
		{
				soldier_Move.SetNavDestination(targetSoldier.transform);
				float range = curTime > skillCoolTime? Mathf.Max(soldier.attackRange,soldier.skillRange) : soldier.attackRange;
				if (Vector3.Distance(transform.position, targetSoldier.transform.position) < range)
				{
						soldier_Move.SetNavDestination(transform);
						condition = Soldier_Condition.ATTACK;
				}
		}

		/// <summary>
		/// 전투원 공격
		/// </summary>
		public void SoldierAttack()
		{
				if (targetSoldier == null)
				{
						condition = Soldier_Condition.SEARCH;
						return;
				}
				if(soldier.name == Constants.NAME_Player&&!isTaunt)
				{
						Debug.Log("PlayerAuotAttack");
						soldier.GetComponent<Player_Attack>().DoAutoAttack();
						return;
				}
				if (curTime > skillCoolTime && !isTaunt)
				{
						curTime = 0;
						curAttackTime = 0;
						soldier_Attack.DoSkill(targetSoldier);
						if(soldier_Anim!=null)
						soldier_Anim.SetAnimSkill();

				}
				else if (curAttackTime > attackSpeed)
				{
						soldier_Attack.DoAttack(targetSoldier);	
						curAttackTime = 0;
						if (soldier_Anim != null)
						soldier_Anim.SetAnimAttack();
				}

				condition = Soldier_Condition.MOVE;

		}

		public void SetTargetSoldier(Soldier targetSoldier)
		{
				this.targetSoldier = targetSoldier;
		}

		public void SetSoldierCondition(Soldier_Condition condition)
		{
				this.condition = condition;
		}
}
