using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
		static UIManager instance = null;

		public static UIManager Instance()
		{
				return instance;
		}

		private Soldier player;
		public Slider playerHp;
		public Image victoryImage;
		public Image defeatImage;
		public JoyStickPanel joystickPanel;

		public void Awake()
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
}
