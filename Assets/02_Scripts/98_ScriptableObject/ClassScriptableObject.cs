using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Class", menuName = "ScriptableObject/Class", order = int.MaxValue)]
public class ClassScriptableObject : ScriptableObject
{
		[Header("# 클래스 데이터 리스트")]
		public List<ClassData> classes;


		public ClassData GetClassDataByClassName(string className)
				=>classes.Find(classData=>classData.className == className);
}

[System.Serializable]
public class ClassData
{
		[Header("# 클래스 이름")]
		public string className;

		public Soldier soldierPrefab;

}
