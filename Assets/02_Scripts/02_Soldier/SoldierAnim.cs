using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAnim : MonoBehaviour
{
    private NavMeshAgent agent;
		private SpriteRenderer animRenderer;
		private Animator anim;

		private void Awake()
		{
				agent = GetComponentInParent<NavMeshAgent>();
				animRenderer = GetComponent<SpriteRenderer>();
				anim = GetComponent<Animator>();
		}

		private void Update()
		{
				animRenderer.flipX = (agent.velocity.x < 0);
				animRenderer.transform.forward = Camera.main.transform.forward;

		}

		public void SetAnimAttack()
		{
				anim.SetTrigger("IsAttack");
		}

		public void SetAnimSkill()
		{
				anim.SetTrigger("IsSkill");

		}

		public void SetAnimMove(Vector3 velocity)
		{
				anim.SetBool("IsMove",!Mathf.Approximately(velocity.magnitude, 0));
		}
}
