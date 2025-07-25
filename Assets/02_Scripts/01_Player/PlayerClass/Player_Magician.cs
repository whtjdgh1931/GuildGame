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
				FireBall fireBall = Instantiate(fireBallPrefab);
				fireBall.target = target;
				fireBall.transform.position = transform.position;
				fireBall.arrowPower = soldier.attackPower;
				fireBall.gameObject.tag = gameObject.tag;
		}

		public override void DoSkill(Vector3 mousePosition)
		{
				Soldier targetSoldier = SearchEnemyTarget(mousePosition);
				if (targetSoldier == null) return;
				LightningBoltScript attackLightning = Instantiate(lightning);
				attackLightning.StartObject = gameObject;
				attackLightning.EndObject = targetSoldier.gameObject;
				List<Soldier> targetSoldiers = SearchEnemyTarget(targetSoldier, 3);
				if (targetSoldiers == null || targetSoldiers.Count == 0) return;
				for (int i = 0; i < targetSoldiers.Count; i++)
				{
						if (i!=targetSoldiers.Count-1)
						{
						LightningBoltScript soldierLightning = Instantiate(lightning);
								soldierLightning.StartObject = targetSoldiers[i].gameObject;
								soldierLightning.EndObject = targetSoldiers[i + 1].gameObject;
						}
						DoDamage(targetSoldiers[i], soldier.attackPower);
				}
		}

		public override void DoSkill(Soldier target)
		{
				if (target == null) return;
				LightningBoltScript attackLightning = Instantiate(lightning);
				attackLightning.StartObject = gameObject;
				attackLightning.EndObject = target.gameObject;
				List<Soldier> targetSoldiers = SearchEnemyTarget(target, 3);
				if (targetSoldiers == null || targetSoldiers.Count == 0) return;
				for (int i = 0; i < targetSoldiers.Count; i++)
				{
						if (i != targetSoldiers.Count - 1)
						{
								LightningBoltScript soldierLightning = Instantiate(lightning);
								soldierLightning.StartObject = targetSoldiers[i].gameObject;
								soldierLightning.EndObject = targetSoldiers[i + 1].gameObject;
						}
						DoDamage(targetSoldiers[i], soldier.attackPower);
				}
		}

		public override void DoUlti(Vector3 mousePosition)
		{
				Instantiate(playerMagiacian_Ulti, SearchEnemyTarget(mousePosition).transform.position, Quaternion.identity).SearchAndHitEnemy(soldier, SearchEnemyTarget(mousePosition));

		}

		public override void DoUlti(Soldier target)
		{
				Instantiate(playerMagiacian_Ulti,target.transform.position,Quaternion.identity).SearchAndHitEnemy(soldier, target);

		}
}
