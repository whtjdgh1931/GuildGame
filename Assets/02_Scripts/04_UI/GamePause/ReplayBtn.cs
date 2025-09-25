using UnityEngine;
using UnityEngine.SceneManagement;

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
				if (PlayerPrefs.GetString(Constants.ENEMYSCENE) != null)
						SceneManager.LoadScene(PlayerPrefs.GetString(Constants.ENEMYSCENE), LoadSceneMode.Additive);

		}
}
