using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBtn : BtnUI
{
		private Player_Move player_Move;
		private Color btnColor;


		public new void Start()
		{
				base.Start();
				player_Move = GameObject.Find(Constants.NAME_Player).GetComponent<Player_Move>();
				btnColor = player_Move.GetComponent<Player_Soldier>().isAuto ? Color.black: Color.white;
				button.image.color = btnColor;


				button.onClick.AddListener(CALLBACK_OnAutoBtnClicked);
		}

		public void CALLBACK_OnAutoBtnClicked()
		{
				bool isAutoEnabled = player_Move.SetAuto();
				btnColor = isAutoEnabled ? Color.black: Color.white;
				button.image.color = btnColor;
		}
}
