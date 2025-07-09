using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier_Move : MonoBehaviour
{
    public NavMeshAgent soldierNav { get; private set; }

		private void Awake()
		{
				soldierNav = GetComponent<NavMeshAgent>();


		}



		public void SetNavDestination(Transform destination)
		{
				soldierNav.SetDestination(destination.position);
		}
}
