using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAnim : MonoBehaviour
{
    private NavMeshAgent agent;
		private SpriteRenderer animRenderer;

		private void Awake()
		{
				agent = GetComponentInParent<NavMeshAgent>();
				animRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
				animRenderer.flipX = (agent.velocity.x < 0);
				animRenderer.transform.forward = Camera.main.transform.forward;

		}
}
