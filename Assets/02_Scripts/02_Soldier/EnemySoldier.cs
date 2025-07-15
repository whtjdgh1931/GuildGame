using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySoldier : Soldier
{
		public string EnemyClassName;
		public void Start()
		{
				if (isInit) return;

				classLevelData = ClassManager.Instance().GetLevelData(EnemyClassName);
				level = Mathf.Min(level, Constants.maxLevel);
				SetLevelData(level);
		}

}
