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

		public IEnumerator RemoveShield(int shield, int second, Soldier target)
		{
				yield return new WaitForSeconds(second);
				target.shield -= shield;
				if (target.shield < 0)
				{
						target.shield = 0;
				}
		}

		public void DoDamage(Soldier target, int attackPower)
		{
				if (target.shield > 0)
				{
						target.shield -= attackPower;
						if (target.shield < 0)
						{
								target.currentHp += target.shield;
								target.shield = 0;
						}
				}
				else target.currentHp -= attackPower;
				if (target.currentHp < 0)
				{
						target.DieSoldier();
				}
		}
}
