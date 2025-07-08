using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSkill : ClassSkill
{
		public Magicial_Explosion magician_Explosion;
		public FireBall fireBallPrefab;


		public override void DoAttack(Soldier target)
		{
				
				FireBall fireBall = Instantiate(fireBallPrefab);
				fireBall.target = target;
				fireBall.transform.position = transform.position;
				fireBall.arrowPower = soldier.attackPower;
				fireBall.gameObject.tag = Constants.TAG_TEAM;
		}

		public override void DoSkill(Soldier target)
		{
				
				Instantiate(magician_Explosion,target.transform.position,Quaternion.identity).SearchAndHitEnemy(soldier);
		}
}
