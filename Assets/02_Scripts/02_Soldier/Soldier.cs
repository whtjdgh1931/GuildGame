using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
		// 클래스 정보
		private ClassData classData;


		public int shield { get; set;}
		public int maxHp{get;set;}
		public int currentHp { get; set;}
		public int attackPower{get;set;}
		public float attackRange{get;set;}
		public float attackSpeed{get;set;}
		public float moveSpeed{get;set;}
		public int level { get; set; }

		private void Awake()
		{
				attackRange = 3f;
				attackPower = 50;
				maxHp = 100;
				currentHp = maxHp;
				shield = 33;
		}

		

		public void SetData(ClassData classData)
		=> this.classData = classData;		

		public void DieSoldier()
		{
				Debug.Log("die");
				Destroy(gameObject);
		}
}
