using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBtn : BtnUI
{
		[SerializeField] private string _class;

		// Start is called before the first frame update
		protected override void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_SetPlayerClass);
				AddBtnAnim();
		}

		public void CALLBACK_SetPlayerClass()
		{
				GameManager.GetInstance().SetPlayerClassString(_class);
		}
		
}
