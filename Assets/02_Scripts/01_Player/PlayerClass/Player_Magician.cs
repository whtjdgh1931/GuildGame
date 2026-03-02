using DigitalRuby.LightningBolt;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player_Magician : Player_Skill
{
		public FireBall fireBallPrefab;
		public LightningBoltScript lightning;
		public PlayerMagician_Ulti playerMagiacian_Ulti;

		public override void DoAttack(Soldier target)
		{
				FireBall fireBall = GameManager.Instance.ObjectPool.GetFromPool(fireBallPrefab,transform.position,Quaternion.identity).GetComponent<FireBall>();
				fireBall.target = target;
				fireBall.arrowPower = soldier.attackPower;
				fireBall.gameObject.tag = gameObject.tag;
		}

		public override void DoSkill(Vector3 mousePosition)
		{
				Soldier targetSoldier = SearchEnemyTarget(mousePosition);
				if (targetSoldier == null) return;
				LightningBoltScript attackLightning = GameManager.Instance.ObjectPool.GetFromPool(lightning,transform.position,Quaternion.identity).GetComponent<LightningBoltScript>();
				attackLightning.StartObject = gameObject;
				attackLightning.EndObject = targetSoldier.gameObject;
				List<Soldier> targetSoldiers = SearchEnemyTarget(targetSoldier, 3);
				if (targetSoldiers == null || targetSoldiers.Count == 0) return;
				for (int i = 0; i < targetSoldiers.Count; i++)
				{
						if (i!=targetSoldiers.Count-1)
						{
						LightningBoltScript soldierLightning = GameManager.Instance.ObjectPool.GetFromPool(lightning,transform.position,Quaternion.identity).GetComponent<LightningBoltScript>();
								soldierLightning.StartObject = targetSoldiers[i].gameObject;
								soldierLightning.EndObject = targetSoldiers[i + 1].gameObject;
						}
						DoDamage(targetSoldiers[i], soldier.attackPower);
				}
		}

		public override void DoSkill(Soldier target)
		{
				if (target == null) return;
				LightningBoltScript attackLightning = GameManager.Instance.ObjectPool.GetFromPool(lightning,transform.position,Quaternion.identity).GetComponent<LightningBoltScript>();
				attackLightning.StartObject = gameObject;
				attackLightning.EndObject = target.gameObject;
				List<Soldier> targetSoldiers = SearchEnemyTarget(target, 3);
				if (targetSoldiers == null || targetSoldiers.Count == 0) return;
				for (int i = 0; i < targetSoldiers.Count; i++)
				{
						if (i != targetSoldiers.Count - 1)
						{
								LightningBoltScript soldierLightning = GameManager.Instance.ObjectPool.GetFromPool(lightning,transform.position,Quaternion.identity).GetComponent<LightningBoltScript>();
								soldierLightning.StartObject = targetSoldiers[i].gameObject;
								soldierLightning.EndObject = targetSoldiers[i + 1].gameObject;
						}
						DoDamage(targetSoldiers[i], soldier.attackPower);
				}
		}

		public override void DoUlti(Vector3 mousePosition)
		{
				GameManager.Instance.ObjectPool.GetFromPool(playerMagiacian_Ulti, SearchEnemyTarget(mousePosition).transform.position, Quaternion.identity).GetComponent<PlayerMagician_Ulti>().SearchAndHitEnemy(soldier, SearchEnemyTarget(mousePosition));
	
		}

		public override void DoUlti(Soldier target)
		{
				GameManager.Instance.ObjectPool.GetFromPool(playerMagiacian_Ulti,target.transform.position,Quaternion.identity).GetComponent<PlayerMagician_Ulti>().SearchAndHitEnemy(soldier, target);
	
		}
}
