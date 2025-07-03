using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankerSkill : ClassSkill
{
		public void Awake()
		{
				base.Awake();
		}

		public override void DoAttack(Soldier target)
		{
				Debug.Log("attack" + target);
				target.currentHp -= soldier.attackPower;
				if(target.currentHp < 0 )
				{
						target.DieSoldier();
				}
		}		


		public override void DoSkill(Soldier target)
		{
				Debug.Log("skill");
		}

	

}
