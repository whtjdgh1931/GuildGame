using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill : ClassSkill
{
		public override void DoAttack(Soldier target)
		{
				Debug.Log("Warriorattack" + target);

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

		public override void DoSkill(Soldier target)
		{
				Debug.Log("WarriorSkill" + target);
						Debug.Log(Mathf.RoundToInt(soldier.attackPower * 1.5f));

				if (target.shield > 0)
				{
						Debug.Log(target.shield );
						int remain = target.shield -= Mathf.RoundToInt(soldier.attackPower*1.5f);
						Debug.Log($"{remain}");
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
