using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankerSkill : ClassSkill
{
		public GameObject shieldGameObject;
		public override void DoAttack(Soldier target)
		{
				Debug.Log("attack" + target);

				if (target.shield > 0)
				{
						target.shield -= soldier.attackPower;
						if(target.shield < 0)
						{
								target.currentHp += target.shield;
								target.shield = 0;
						}
				}
				else target.currentHp -= soldier.attackPower;
				if(target.currentHp < 0 )
				{
						target.DieSoldier();
				}
		}		


		public override void DoSkill(Soldier target)
		{
				
				soldier.shield += 30;
				
				
				StartCoroutine(RemoveShield(30, 3, soldier));
		}

		public void Update()
		{
				if(soldier.shield > 0) shieldGameObject.SetActive(true);
				else shieldGameObject.SetActive(false);	
		}


}
