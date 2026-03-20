using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour, IPoolable, IReleasePoolable
{
	protected bool isInit = false;

	public List<Dictionary<string, object>> classLevelData;

	public SoldierRange attackRangeObject;

	[SerializeField] private CharacterHp _hpSlider;

	public CharacterHp hpSlider { get { return _hpSlider; } }
	public void SetHpSlider(CharacterHp hpSlider)
	{
		_hpSlider = hpSlider;
	}

	public int shield { get; set; }
	public int maxHp { get; set; }
	public int currentHp { get; set; }
	public int attackPower { get; set; }
	public float attackRange { get; set; }
	public float attackSpeed { get; set; }
	public float moveSpeed { get; set; }
	public float skillRange { get; set; }

	public float skillCoefficient { get; set; }

	public int level;

	public void SetLevelData(int level)
	{
		if (level < 1) level = 1;
		maxHp = int.Parse(classLevelData[level - 1]["MaxHp"].ToString());
		maxHp = Mathf.RoundToInt(maxHp * Constants.Multi_HP);
		attackPower = int.Parse(classLevelData[level - 1]["AttackPower"].ToString());
		attackRange = float.Parse(classLevelData[level - 1]["AttackRange"].ToString());
		attackSpeed = float.Parse(classLevelData[level - 1]["AttackSpeed"].ToString());
		moveSpeed = float.Parse(classLevelData[level - 1]["MoveSpeed"].ToString());
		skillRange = float.Parse(classLevelData[level - 1]["SkillRange"].ToString());
		skillCoefficient = float.Parse(classLevelData[level - 1]["SkillCoefficient"].ToString());
		currentHp = maxHp;


		GetComponent<Soldier_Move>().soldierNav.speed = moveSpeed;

		attackRangeObject = GetComponentInChildren<SoldierRange>();
		if (attackRangeObject != null)
		{
			Vector3 rangeScale = new(attackRange, 0.1f, attackRange);
			attackRangeObject.transform.localScale = rangeScale;
			attackRangeObject.gameObject.SetActive(false);
		}



		isInit = true;
	}

	public void SetHpRatio()
	{
		_hpSlider.SetSliderValue((float)currentHp / (float)maxHp);
	}



	public void DieSoldier()
	{
		Animator anim = GetComponentInChildren<Animator>();
		if (_hpSlider != null)
		{
			GameManager.Instance.ObjectPool.ReturnToPool(_hpSlider.gameObject, 0f);
		}
		if (anim != null)
		{
			anim.SetTrigger("IsDead");
		}
		GameManager.Instance.ObjectPool.ReturnToPool(gameObject, 1f);
	}

	public void OnGetFromPool(Vector3 position, Quaternion rotation)
	{
		transform.SetPositionAndRotation(position, rotation);
	}

	public void ReleaseObjectPool()
	{
		isInit = false;
		_hpSlider = null;
		attackRangeObject = null;
		GetComponent<NavMeshAgent>().enabled = true;
	}
}
