using UnityEngine;

public class HealerSkill : ClassSkill
{
		public Holy holyPrefab;
		public GameObject healEffectPrefab;

		public override void DoAttack(Soldier target)
		{
				Holy holy = Instantiate(holyPrefab);
				holy.target = target;
				holy.transform.position = transform.position;
				holy.arrowPower = soldier.attackPower;
				holy.gameObject.tag = gameObject.tag;
		}

		public override void DoSkill(Soldier target)
		{
				Collider[] healTeam = Physics.OverlapSphere(transform.position, soldier.skillRange);
				Soldier healTargetSoldier = null;
				float minSoldierHp = 1f;
				foreach (Collider healTarget in healTeam)
				{
						if (healTarget.GetComponent<Soldier>() == null || !healTarget.CompareTag(soldier.tag)) continue;
						if (minSoldierHp > (float)healTarget.GetComponent<Soldier>().currentHp/healTarget.GetComponent<Soldier>().maxHp)
						{
								healTargetSoldier = healTarget.GetComponent<Soldier>();
								minSoldierHp = (float)healTarget.GetComponent<Soldier>().currentHp / healTarget.GetComponent<Soldier>().maxHp;
						}

				}

				if (healTargetSoldier == null || healTargetSoldier.currentHp >= healTargetSoldier.maxHp)
				{
						DoAttack(target);
						GetComponent<FSM>().curTime = GetComponent<FSM>().skillCoolTime - 1f;
						return;
				}
				healTargetSoldier.currentHp += Mathf.RoundToInt(soldier.attackPower * soldier.skillCoefficient);
				if (healTargetSoldier.currentHp > healTargetSoldier.maxHp) healTargetSoldier.currentHp = healTargetSoldier.maxHp;
				GameObject healEffect = Instantiate(healEffectPrefab, healTargetSoldier.transform.position, Quaternion.identity);
				Destroy(healEffect, 1f);
			

		}
}

