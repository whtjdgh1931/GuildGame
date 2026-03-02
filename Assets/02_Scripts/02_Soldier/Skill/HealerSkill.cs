using UnityEngine;

public class HealerSkill : ClassSkill
{
		public Holy holyPrefab;
		public PoolableObject healEffectPrefab;

		public override void DoAttack(Soldier target)
		{
				Holy holy = GameManager.Instance.ObjectPool.GetFromPool(holyPrefab, transform.position, Quaternion.identity).GetComponent<Holy>();
				holy.target = target;
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
				GameObject healEffect = GameManager.Instance.ObjectPool.GetFromPool(healEffectPrefab, healTargetSoldier.transform.position, Quaternion.identity);
				GameManager.Instance.ObjectPool.ReturnToPool(healEffect, 1f);
			

		}
}

