using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Soldier : Soldier
{
		public string PlayerClassName;

		public bool isAuto;

		public void Start()
		{
				if (isInit) return;

				classLevelData = ClassManager.Instance().GetLevelData(PlayerClassName);
				level = Mathf.Min(level, Constants.maxLevel);
				SetLevelData(level);

				maxHp *= 2;
				currentHp = maxHp;
				attackPower *= 2;
				GetComponent<Player_Attack>().ReadyAttack();
				GetComponent<Player_Move>().ReadyMove();

		}
}
