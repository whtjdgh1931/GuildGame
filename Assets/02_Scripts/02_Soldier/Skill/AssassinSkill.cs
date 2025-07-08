using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinSkill : ClassSkill
{
		public Dart dartPrefab;

		public override void DoAttack(Soldier target)
		{
				Debug.Log("AssassinAttack");
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
				Debug.Log("AssassinSkill");
				Dart dart = Instantiate(dartPrefab);
				dart.target = target;
				dart.transform.position = transform.position;
				dart.arrowPower = soldier.attackPower;
				dart.gameObject.tag = Constants.TAG_TEAM;
		}

		
}
