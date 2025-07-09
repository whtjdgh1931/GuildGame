using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Magicial_Explosion : MonoBehaviour
{
		public void SearchAndHitEnemy(Soldier soldier)
		{
				Collider[] hitEnemies = Physics.OverlapSphere(transform.position, soldier.skillRange);
				foreach(Collider hitEnemy in hitEnemies)
				{
						Debug.Log(hitEnemy.gameObject);
						if (hitEnemy.GetComponent<Soldier>() == null || hitEnemy.CompareTag(soldier.tag)) continue;
						Debug.Log(hitEnemy.gameObject + "Hit");
						Soldier target = hitEnemy.GetComponent<Soldier>();
						Debug.Log("Explosion : " + Mathf.RoundToInt(soldier.attackPower * soldier.skillCoefficient));

						if (target.shield > 0)
						{
								target.shield -= Mathf.RoundToInt(soldier.attackPower*soldier.skillCoefficient);
								if (target.shield < 0)
								{
										target.currentHp += target.shield;
										target.shield = 0;
								}
						}
						else target.currentHp -=Mathf.RoundToInt(soldier.attackPower * soldier.skillCoefficient);
						if (target.currentHp < 0)
						{
								target.DieSoldier();
						}
				}

				Destroy(gameObject, 0.5f);
		}
}
