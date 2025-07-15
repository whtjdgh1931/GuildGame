using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Soldier player;
		public Slider playerHp;
		public Image victoryImage;
		public Image defeatImage;

		public void Awake()
		{
				player = GameObject.Find(Constants.NAME_Player).GetComponent<Soldier>();
		}

		public void Update()
		{
				playerHp.value = (float)player.currentHp/player.maxHp;
		}
}
