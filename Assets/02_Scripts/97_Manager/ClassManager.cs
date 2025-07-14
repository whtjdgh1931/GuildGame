using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassManager : MonoBehaviour
{
		static ClassManager instance = null;

		public static ClassManager Instance()
		{
				return instance;
		}

		//TODO 삭제할지 아닐지 결정
		//public int tankerLevel;
		//public int warriorLevel;
		//public int assassinLevel;
		//public int archerLevel;
		//public int healerLevel;
		//public int magicianLevel;


		public string tankerDataPath;
		public List<Dictionary<string,object>> tankerData;

		public string warriorDataPath;
		public List<Dictionary<string, object>> warriorData;

		public string assassinDataPath;
		public List<Dictionary<string, object>> assassinData;

		public string archerDataPath;
		public List<Dictionary<string, object>> archerData;

		public string healerDataPath;
		public List<Dictionary<string, object>> healerData;

		public string magicianDataPath;
		public List<Dictionary<string, object>> magicianData;

		void Awake()
		{
				tankerData = CSVReader.Read(tankerDataPath);
				warriorData = CSVReader.Read(warriorDataPath);
				assassinData = CSVReader.Read(assassinDataPath);
				archerData = CSVReader.Read(archerDataPath);
				healerData = CSVReader.Read(healerDataPath);
				magicianData = CSVReader.Read(magicianDataPath);



				if (instance == null)
				{
						instance = this;
				}
								//LoadClassLevel();


		}

		public List<Dictionary<string, object>> GetLevelData(string className)
		{
				List<Dictionary<string, object>> data;

				switch (className) 
				{
						case "Tanker":
								data = tankerData;
								break;
						case "Warrior":
								data = warriorData;
								break;
						case "Assassin":
								data = assassinData;
								break;
						case "Archer":
								data = archerData;
								break;
						case "Healer":
								data = healerData;
								break;
						case "Magician":
								data = magicianData;
								break;
						default: data = null;
								break;
				}
				return data;
		}

		public float GetAttackRangeData(string className)
		{
				List<Dictionary<string, object>> data;
				int level = PlayerPrefs.GetInt(className);
				switch (className)
				{
						case "Tanker":
								data = tankerData;
								break;
						case "Warrior":
								data = warriorData;
								break;
						case "Assassin":
								data = assassinData;
								break;
						case "Archer":
								data = archerData;
								break;
						case "Healer":
								data = healerData;
								break;
						case "Magician":
								data = magicianData;
								break;
						default:
								data = null;
								break;
				}

				if (level == 0) level = 1;
				return float.Parse(data[level - 1]["AttackRange"].ToString());
		}

		public void ClassLevelUp(string className)
		{
				if(PlayerPrefs.GetInt(className)<Constants.maxLevel) 
				PlayerPrefs.SetInt(className,PlayerPrefs.GetInt(className)+1);


		}

		//TODO 삭제할지 말지 결정
		//public void LoadClassLevel()
		//{
		//		tankerLevel = PlayerPrefs.GetInt(Constants.CLASS_TANKER);
		//		warriorLevel = PlayerPrefs.GetInt(Constants.CLASS_WARRIOR);
		//		assassinLevel = PlayerPrefs.GetInt(Constants.CLASS_ASSASSIN);
		//		archerLevel = PlayerPrefs.GetInt(Constants.CLASS_ARCHER);
		//		healerLevel = PlayerPrefs.GetInt(Constants.CLASS_HEALER);
		//		magicianLevel = PlayerPrefs.GetInt(Constants.CLASS_MAGICIAN);
		//}

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
