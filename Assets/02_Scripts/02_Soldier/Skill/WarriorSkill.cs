using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill : ClassSkill
{
		public override void DoAttack(Soldier target)
		{
				Debug.Log("Warriorattack" + target);

				DoDamage(target, soldier.attackPower);
				
		}

		public override void DoSkill(Soldier target)
		{
			

				if (target.shield > 0)
				{
						int remain = target.shield -= Mathf.RoundToInt(soldier.attackPower*1.5f);
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
