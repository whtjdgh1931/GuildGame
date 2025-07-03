using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassManager : MonoBehaviour
{
		static ClassManager instance = null;

		public static ClassManager Instance()
		{
				return instance;
		}

		public int tankerLevel;
		public int warriorLevel;
		public int assassinLevel;
		public int archerLevel;
		public int healerLevel;
		public int magicianLevel;

		void Start()
		{
				if (instance == null)
				{
						instance = this;
				}
								LoadClassLevel();
		}

		public void ClassLevelUp(string className)
		{
				if(PlayerPrefs.GetInt(className)<Constants.maxLevel) 
				PlayerPrefs.SetInt(className,PlayerPrefs.GetInt(className)+1);

		}

		public void LoadClassLevel()
		{
				tankerLevel = PlayerPrefs.GetInt(Constants.CLASS_TANKER);
				warriorLevel = PlayerPrefs.GetInt(Constants.CLASS_WARRIOR);
				assassinLevel = PlayerPrefs.GetInt(Constants.CLASS_ASSASSIN);
				archerLevel = PlayerPrefs.GetInt(Constants.CLASS_ARCHER);
				healerLevel = PlayerPrefs.GetInt(Constants.CLASS_HEALER);
				magicianLevel = PlayerPrefs.GetInt(Constants.CLASS_MAGICIAN);
		}

		private void ClearClassLevel()
		{
				PlayerPrefs.SetInt(Constants.CLASS_TANKER,1);
				PlayerPrefs.SetInt(Constants.CLASS_WARRIOR,1);
				PlayerPrefs.SetInt(Constants.CLASS_ASSASSIN,1);
				PlayerPrefs.SetInt(Constants.CLASS_ARCHER,1);
				PlayerPrefs.SetInt(Constants.CLASS_HEALER,1);
				PlayerPrefs.SetInt(Constants.CLASS_MAGICIAN,1);
		}
}
