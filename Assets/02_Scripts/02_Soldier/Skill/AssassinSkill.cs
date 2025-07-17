using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinSkill : ClassSkill
{
		public Dart dartPrefab;

		public override void DoAttack(Soldier target)
		{
				Debug.Log("AssassinAttack");
				DoDamage(target, soldier.attackPower);
		}

		public override void DoSkill(Soldier target)
		{
				Debug.Log("AssassinSkill");
				Dart dart = Instantiate(dartPrefab);
				Debug.Log(dart.name);
				dart.target = target;
				dart.transform.position = transform.position;
				dart.arrowPower = soldier.attackPower;
				dart.gameObject.tag = gameObject.tag;
		}

		
}
