using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ArcherSkill : ClassSkill
{
    public Arrow arrowPrefab;

		public override void DoAttack(Soldier target)
		{
				Debug.Log("Archer Attack");
				Arrow arrow = Instantiate(arrowPrefab);
				arrow.target = target;
				arrow.transform.position = transform.position;
				arrow.arrowPower = soldier.attackPower;
				arrow.gameObject.tag = Constants.TAG_TEAM;
				//arrow.transform.rotation = Quaternion.LookRotation(soldier.transform.forward);

		}

		public override void DoSkill(Soldier target)
		{
				Debug.Log("ArcherSkill");
		}

	
}
