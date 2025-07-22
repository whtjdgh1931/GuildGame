using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Move : Soldier_Move
{
    public FixedJoystick playerJoyStick;

		private CharacterController playerController;

		private Player_Soldier playerState;

		public BoxCollider playerCollider;

		public void ReadyMove()
		{
				playerController = GetComponent<CharacterController>();
				playerState = GetComponent<Player_Soldier>();

				playerController.enabled = !playerState.isAuto;
				soldierNav.enabled = playerState.isAuto;
				GetComponent<FSM>().enabled = playerState.isAuto;
				playerCollider = GetComponent<BoxCollider>();
				playerCollider.enabled = playerState.isAuto;
		}


		public void Update()
		{
				if (playerState.isAuto) return;

        if (playerState.currentHp <= 0) return;

				float h = Input.GetAxis("Horizontal"); //x -1f ~ 1f
				float v = Input.GetAxis("Vertical"); //y -1f ~ 1f

#if UNITY_ANDROID
				if(playerJoyStick != null)
				{
						h = playerJoyStick.Horizontal;
						v = playerJoyStick.Vertical;

				}
#endif

				Vector3 moveDirection = new Vector3(h, 0f, v);

				moveDirection *= playerState.moveSpeed*8.0f;

				playerController.Move(moveDirection * Time.deltaTime);

				
		}

		public bool SetAuto()
		{
				playerState.isAuto = !playerState.isAuto;
				playerController.enabled = !playerState.isAuto;
				soldierNav.enabled = playerState.isAuto;
				GetComponent<FSM>().enabled = playerState.isAuto;
				playerCollider.enabled = playerState.isAuto;
				return playerState.isAuto;
		}		

		public bool returnAuto()
		{
				return playerState.isAuto;
		}
}
