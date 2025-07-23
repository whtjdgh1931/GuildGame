using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayBtn : BtnUI
{
		
		public new void Start()
		{
				base.Start();
				button.onClick.AddListener(CALLBACK_ReplayBtnClicked);
						
		}



		public void CALLBACK_ReplayBtnClicked()
    {
				Time.timeScale = 1f; 
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
}
