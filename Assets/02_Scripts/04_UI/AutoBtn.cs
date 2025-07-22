using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBtn : MonoBehaviour
{
		private Player_Move player_Move;
		private Color btnColor;
		private Button autoBtn;


		public void Start()
		{
				player_Move = GameObject.Find(Constants.NAME_Player).GetComponent<Player_Move>();
				autoBtn = GetComponent<Button>();
				btnColor = player_Move.GetComponent<Player_Soldier>().isAuto ? Color.black: Color.white;
				autoBtn.image.color = btnColor;


				autoBtn.onClick.AddListener(CALLBACK_OnAutoBtnClicked);
		}

		public void CALLBACK_OnAutoBtnClicked()
		{
				bool isAutoEnabled = player_Move.SetAuto();
				btnColor = isAutoEnabled ? Color.black: Color.white;
				autoBtn.image.color = btnColor;
		}
}
