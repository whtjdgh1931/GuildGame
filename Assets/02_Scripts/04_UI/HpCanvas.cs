using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HpCanvas : MonoBehaviour
{
    [SerializeField] SoldierManager soldierManager;
    [SerializeField] CharacterHp hpSlider;
		

		[SerializeField] bool isInit = false;

		public void StartGame()
		{
				foreach (Soldier soldier in soldierManager.teamSoldiers)
				{
						Debug.Log(soldier.name);
						// HP바 인스턴스 생성
						soldier.SetHpSlider(Instantiate(hpSlider, transform));
						soldier.hpSlider.ChangeColor(0);
				}


				foreach (Soldier soldier in soldierManager.enemySoldiers)
				{
						soldier.SetHpSlider(Instantiate(hpSlider,transform));
						soldier.hpSlider.ChangeColor(1);
				}

				isInit = true;

		}

		private void Update()
		{
				if(!isInit)
				{
						return;
				}

				foreach (Soldier soldier in soldierManager.teamSoldiers)
				{
						if (soldier != null && soldier.hpSlider != null)
						{
								// HP바 위치를 캐릭터 위에 표시
								Vector3 screenPos = Camera.main.WorldToScreenPoint(soldier.transform.position + Vector3.forward * 3f);
								soldier.hpSlider.transform.position = screenPos;

								// HP 값 업데이트
								soldier.SetHpRatio();
						}
				}

				


				foreach (Soldier soldier in soldierManager.enemySoldiers)
				{
						if (soldier != null && soldier.hpSlider != null)
						{
								// HP바 위치를 캐릭터 위에 표시
								Vector3 screenPos = Camera.main.WorldToScreenPoint(soldier.transform.position + Vector3.forward * 3f);
								soldier.hpSlider.transform.position = screenPos;

								// HP 값 업데이트
								soldier.SetHpRatio();
						}
				}

		}
}
