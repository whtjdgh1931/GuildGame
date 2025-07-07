using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Arrow
{

		public new void OnTriggerEnter(Collider other)
		{
				if (other.GetComponent<Soldier>() != null && !other.CompareTag(gameObject.tag))
				{
						Debug.Log("Fireball");
						if (target.shield > 0)
						{
								target.shield -= arrowPower;
								if (target.shield < 0)
								{
										target.currentHp += target.shield;
										target.shield = 0;
								}
						}
						else target.currentHp -= arrowPower;
						if (target.currentHp < 0)
						{
								target.DieSoldier();
						}
						Destroy(gameObject);
				}
		}
				
		
}
