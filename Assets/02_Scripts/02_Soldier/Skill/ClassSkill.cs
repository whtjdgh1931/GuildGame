using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClassSkill : MonoBehaviour
{
    protected Soldier soldier;

		public void Awake()
		{
				soldier = GetComponent<Soldier>();
		}

		public abstract void DoSkill(Soldier target);
    
    public abstract void DoAttack(Soldier target);    
}
