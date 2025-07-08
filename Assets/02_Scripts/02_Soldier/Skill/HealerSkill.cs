using UnityEngine;

public class HealerSkill : ClassSkill
{
		public Holy holyPrefab;

		public override void DoAttack(Soldier target)
		{
				Holy holy = Instantiate(holyPrefab);
				holy.target = target;
				holy.transform.position = transform.position;
				holy.arrowPower = soldier.attackPower;
				holy.gameObject.tag = Constants.TAG_TEAM;
		}

		public override void DoSkill(Soldier target)
		{
				Collider[] healTeam = Physics.OverlapSphere(transform.position, soldier.skillRange);
				Soldier healTargetSoldier = null;
				int minSoldierHp = int.MaxValue;
				foreach (Collider healTarget in healTeam)
				{
						if (healTarget.GetComponent<Soldier>() == null || !healTarget.CompareTag(soldier.tag)) continue;
						Debug.Log(healTarget.gameObject + "Heal");
						if (minSoldierHp > healTarget.GetComponent<Soldier>().currentHp)
						{
								healTargetSoldier = healTarget.GetComponent<Soldier>();
								minSoldierHp = healTargetSoldier.currentHp;
						}

				}

				if (healTargetSoldier == null)
				{
						DoAttack(target);
						GetComponent<FSM>().curTime = int.MaxValue;
				}
				healTargetSoldier.currentHp += Mathf.RoundToInt(soldier.attackPower * soldier.skillCoefficient);
				if (healTargetSoldier.currentHp > healTargetSoldier.maxHp) healTargetSoldier.currentHp = healTargetSoldier.maxHp;
		}
}

