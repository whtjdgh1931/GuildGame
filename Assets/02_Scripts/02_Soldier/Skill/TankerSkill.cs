using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankerSkill : ClassSkill
{
		public GameObject shieldGameObject;
		public override void DoAttack(Soldier target)
		{

				DoDamage(target,soldier.attackPower);
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
