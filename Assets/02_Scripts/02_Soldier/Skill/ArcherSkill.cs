using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ArcherSkill : ClassSkill
{
    public Arrow arrowPrefab;

		public override void DoAttack(Soldier target)
		{
				Arrow arrow = Instantiate(arrowPrefab);
				arrow.target = target;
				arrow.transform.position = transform.position;
				arrow.arrowPower = soldier.attackPower;
				//arrow.transform.rotation = Quaternion.LookRotation(soldier.transform.forward);

		}

		public override void DoSkill(Soldier target)
		{
				throw new System.NotImplementedException();
		}

	
}
