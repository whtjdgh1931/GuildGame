using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Soldier_Condition
{
		NONE,
		SEARCH,
		MOVE,
		ATTACK
}

public class FSM : MonoBehaviour
{
		//이동 컴포넌트
    private Soldier_Move solider_Move;

		// 현재 솔져 상태 
		private Soldier_Condition condition;

		// 타겟팅 된 적 솔져
		private Soldier targetSoldier;

		// 리스트를 가지고 있는 솔져매니저
		private SoldierManager soldierManager;
		

		public void Awake()
		{
				solider_Move = GetComponent<Soldier_Move>();
				soldierManager = FindObjectOfType<SoldierManager>();
		}

		public void Start()
		{
				condition = Soldier_Condition.SEARCH;
		}

		public void Update()
		{
				
		}

		public void DoFsm()
		{
				if (targetSoldier == null) condition = Soldier_Condition.SEARCH;
				switch (condition)
				{
						case Soldier_Condition.NONE:
								break;
						case Soldier_Condition.SEARCH:
								SearchEnemy(tag);

								break;
						case Soldier_Condition.MOVE:
								solider_Move.SetNavDestination(targetSoldier.transform);
								break;
						case Soldier_Condition.ATTACK:
								break;
				}
		}
				
		public void SearchEnemy(string tag)
		{
				if(tag == Constants.TAG_TEAM)
		}

}
