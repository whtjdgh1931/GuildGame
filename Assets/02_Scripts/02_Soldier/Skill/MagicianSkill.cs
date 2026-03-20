using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSkill : ClassSkill
{
		public Magicial_Explosion magician_Explosion;
		public FireBall fireBallPrefab;


		public override void DoAttack(Soldier target)
		{
				
				FireBall fireBall = GameManager.Instance.ObjectPool.GetFromPool(fireBallPrefab, transform.position, Quaternion.identity).GetComponent<FireBall>();
				fireBall.target = target;
				fireBall.arrowPower = soldier.attackPower;
				fireBall.gameObject.tag = gameObject.tag;
		}

		public override void DoSkill(Soldier target)
		{
				
				Magicial_Explosion explosion = GameManager.Instance.ObjectPool.GetFromPool(magician_Explosion, target.transform.position, Quaternion.identity).GetComponent<Magicial_Explosion>();
				explosion.SearchAndHitEnemy(soldier);
		}
}
