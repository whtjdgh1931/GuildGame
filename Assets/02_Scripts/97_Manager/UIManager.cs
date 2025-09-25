using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
		static UIManager instance = null;

		public static UIManager Instance()
		{
				return instance;
		}

		public Soldier player;
		public Slider playerHp;
		public Image victoryImage;
		public Image defeatImage;
		public JoyStickPanel joystickPanel;

		public RectTransform gameResultPanel;

		public void Start()
		{
				if (instance == null)
				{
						instance = this;
				}

				player = GameObject.Find(Constants.NAME_Player).GetComponent<Soldier>();



		}
		public void Update()
		{
				playerHp.value = (float)player.currentHp/player.maxHp;
		}

	public void SetJoystick()
	{
        joystickPanel.gameObject.SetActive(!player.GetComponent<Player_Soldier>().isAuto);

    }
}
