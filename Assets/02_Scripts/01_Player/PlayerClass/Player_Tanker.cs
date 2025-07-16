using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Tanker : Player_Skill
{
		public override void DoAttack(Soldier target)
		{
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
				Debug.Log("PlayerAttack");
		}


		public override void DoSkill(Vector3 mousePosition)
		{
				throw new System.NotImplementedException();
		}

		public override void DoSkill(Soldier target)
		{
				Debug.Log("PlayerSkill");
		}



		public override void DoUlti(Vector3 mousePosition)
		{
				throw new System.NotImplementedException();
		}

		public override void DoUlti(Soldier target)
		{
				Debug.Log("PlayerUlti");
		}
}
