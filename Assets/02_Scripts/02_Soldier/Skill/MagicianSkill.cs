using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSkill : ClassSkill
{
		public Magicial_Explosion Magicial_Explosion;
		public FireBall FireBall;


		public override void DoAttack(Soldier target)
		{
				
				Arrow arrow = Instantiate(FireBall);
				arrow.target = target;
				arrow.transform.position = transform.position;
				arrow.arrowPower = soldier.attackPower;
				arrow.gameObject.tag = Constants.TAG_TEAM;
		}

		public override void DoSkill(Soldier target)
		{
				Debug.Log("Skill : " + target);
				Instantiate(Magicial_Explosion,target.transform.position,Quaternion.identity).SearchAndHitEnemy(soldier);
		}
}
