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
				transform.LookAt(target.transform);
				transform.position = Vector3.MoveTowards(transform.position, target.transform.position, arrowSpeed * Time.deltaTime);
		}

		public void OnTriggerEnter(Collider other)
		{
				if(other.GetComponent<Soldier>() != null)Destroy(gameObject);
		}
}
