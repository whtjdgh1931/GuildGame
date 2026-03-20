using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ArcherSkill : ClassSkill
{
    public Arrow arrowPrefab;

		public override void DoAttack(Soldier target)
		{
				Arrow arrow = GameManager.Instance.ObjectPool.GetFromPool(arrowPrefab,transform.position,Quaternion.identity).GetComponent<Arrow>();
				arrow.target = target;
				arrow.arrowPower = soldier.attackPower;
				arrow.gameObject.tag = gameObject.tag;

		}

		public override void DoSkill(Soldier target)
		{
				
				Arrow arrow1 = GameManager.Instance.ObjectPool.GetFromPool(arrowPrefab,transform.position,Quaternion.identity).GetComponent<Arrow>();
				arrow1.target = target;
				arrow1.transform.Translate(Vector3.right * 0.5f, Space.Self);
				arrow1.arrowPower = Mathf.RoundToInt(soldier.attackPower*soldier.skillCoefficient*0.5f);
				arrow1.gameObject.tag = gameObject.tag;

				Arrow arrow2 = GameManager.Instance.ObjectPool.GetFromPool(arrowPrefab,transform.position,Quaternion.identity).GetComponent<Arrow>();
				arrow2.target = target;
				arrow2.transform.Translate(Vector3.left * 0.5f, Space.Self);
				arrow2.arrowPower = Mathf.RoundToInt(soldier.attackPower * soldier.skillCoefficient * 0.5f);
				arrow2.gameObject.tag = gameObject.tag;



		}


}
