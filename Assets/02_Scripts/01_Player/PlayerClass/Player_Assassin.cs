using UnityEngine;

public class Player_Assassin : Player_Skill
{
		public override void DoAttack(Soldier target)
		{
				DoDamage(target, soldier.attackPower);
		}

		public override void DoSkill(Vector3 mousePosition)
		{
				foreach (Soldier enemySoldier in SoldierManager.Instance().enemySoldiers)
				{
						FSM enemyFSM = enemySoldier.GetComponent<FSM>();
						if (enemyFSM == null) continue;

						if (enemyFSM.targetSoldier == soldier)
						{
								enemyFSM.ResearchTarget();
						}
				}

		}

		public override void DoSkill(Soldier target)
		{
				throw new System.NotImplementedException();
		}

		public override void DoUlti(Vector3 mousePosition)
		{
				Soldier target = SearchEnemyTarget(mousePosition);
				DoDamage(target, soldier.attackPower * 4f);
		}

		public override void DoUlti(Soldier target)
		{
				DoDamage(target, soldier.attackPower * 4f);
		}


}
