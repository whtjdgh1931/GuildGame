using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBtn : BtnUI
{
		[SerializeField] private string _class;
		[SerializeField] private Sprite _classSprite;
		[SerializeField] private Image _classImage;
		[SerializeField] private TextMeshProUGUI _className;

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
				_classImage.sprite = _classSprite;
				_className.text = _class;

		}
		
}
