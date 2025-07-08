using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
		protected bool isInit = false;

		public List<Dictionary<string, object>> classLevelData;



		public int shield { get; set;}
		public int maxHp{get;set;}
		public int currentHp { get; set;}
		public int attackPower{get;set;}
		public float attackRange{get;set;}
		public float attackSpeed{get;set;}
		public float moveSpeed{get;set;}
		public float skillRange { get;set;}

		public float skillCoefficient { get;set;}

		public int level;

		public void SetLevelData(int level)
		{
				if(level<1) level=1;		
				maxHp = int.Parse(classLevelData[level-1]["MaxHp"].ToString());
				attackPower = int.Parse(classLevelData[level-1]["AttackPower"].ToString());
				attackRange = float.Parse(classLevelData[level - 1]["AttackRange"].ToString());
				attackSpeed = float.Parse(classLevelData[level - 1]["AttackSpeed"].ToString());
				moveSpeed = float.Parse(classLevelData[level - 1]["MoveSpeed"].ToString());
				skillRange = float.Parse(classLevelData[level - 1]["SkillRange"].ToString());
				skillCoefficient = float.Parse(classLevelData[level - 1]["SkillCoefficient"].ToString());
				currentHp = maxHp;

				GetComponent<Soldier_Move>().soldierNav.speed = moveSpeed;

				isInit = true;
		}


		


		public void DieSoldier()
		{
				Debug.Log("die");
				Destroy(gameObject);
		}
}
