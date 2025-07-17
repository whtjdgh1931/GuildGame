using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Soldier target;
    public int arrowPower;
		public float arrowSpeed;

		public void Update()
		{
				if (target == null) 
				{
						Destroy(gameObject);
						return;
				}
				transform.LookAt(target.transform);
				transform.position = Vector3.MoveTowards(transform.position, target.transform.position, arrowSpeed * Time.deltaTime);
				
		}

		public void OnTriggerEnter(Collider other)
		{
				if (other.GetComponent<Soldier>() != null && !other.CompareTag(gameObject.tag))
				{
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
