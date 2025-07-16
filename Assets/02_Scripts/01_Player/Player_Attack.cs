using UnityEngine;

public class Player_Attack : MonoBehaviour
{
		private Player_Skill playerSkill;
		private Player_Soldier player;

		private float curTime;
		private float attackCoolTime;
		private float curSkillTime;
		private float skillCoolTime;
		private float curUltiTimel;
		private float UltiCoolTime;

		public void ReadyAttack()
		{
				player = GetComponent<Player_Soldier>();
				playerSkill = GetComponent<Player_Skill>();

				curTime = float.MaxValue;
				attackCoolTime = Constants.AttackTime / player.attackSpeed;

				curSkillTime = float.MaxValue;
				skillCoolTime = Constants.skillCoolTime;

				curUltiTimel = float.MaxValue;
				UltiCoolTime = Constants.ultiCoolTime;
		}

		public void Update()
		{
				curTime += Time.deltaTime;
				curSkillTime += Time.deltaTime;
				curUltiTimel += Time.deltaTime;

				if (player == null) return;
				if (player.isAuto) return;

				if (Input.GetMouseButton(0))
				{
						if (curTime >= attackCoolTime)
						{
								Soldier target = playerSkill.SearchEnemyTarget(player.attackRange);
								if (target == null) return;
								playerSkill.DoAttack(target);
								curTime = 0;
						}
				}
				else if (Input.GetMouseButton(1))
				{
						if (curSkillTime >= skillCoolTime)
						{
								Soldier target = playerSkill.SearchEnemyTarget(player.attackRange);
								if (target == null) return;
								playerSkill.DoSkill(target);
								curSkillTime = 0;
								curTime = 0;
						}
				}
				else if (Input.GetKeyDown(KeyCode.R))
				{
						if (curUltiTimel >= UltiCoolTime)
						{
								Soldier target = playerSkill.SearchEnemyTarget(player.attackRange);
								if (target == null) return;
								playerSkill.DoUlti(target);
								curUltiTimel = 0;
								curTime = 0;
						}
				}


		}

		public void DoAutoAttack()
		{
				if (curUltiTimel >= UltiCoolTime)
				{
						Soldier target = playerSkill.SearchEnemyTarget(player.attackRange);
						if (target == null) return;
						playerSkill.DoUlti(target);
						curUltiTimel = 0;
						curTime = 0;
						return;
				}

				if (curSkillTime >= skillCoolTime)
				{
						Soldier target = playerSkill.SearchEnemyTarget(player.attackRange);
						if (target == null) return;
						playerSkill.DoSkill(target);
						curSkillTime = 0;
						curTime = 0;
						return;
				}

				if (curTime >= attackCoolTime)
				{
						Soldier target = playerSkill.SearchEnemyTarget(player.attackRange);
						if (target == null) return;
						playerSkill.DoAttack(target);
						curTime = 0;
						return;
				}
		}
}
