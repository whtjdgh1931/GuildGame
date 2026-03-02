using UnityEngine;

public class Arrow : MonoBehaviour, IPoolable, IReleasePoolable
{
	public Soldier target;
	public float arrowPower;
	public float arrowSpeed;

	public void Update()
	{
		if (target == null)
		{
			GameManager.Instance.ObjectPool.ReturnToPool(gameObject);
			return;
		}
		transform.LookAt(target.transform);
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, arrowSpeed * Time.deltaTime);

	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Soldier>() != null && !other.CompareTag(gameObject.tag))
		{
			if (target.shield > 0)
			{
				target.shield -= Mathf.RoundToInt(arrowPower);
				if (target.shield < 0)
				{
					target.currentHp += target.shield;
					target.shield = 0;
				}
			}
			else target.currentHp -= Mathf.RoundToInt(arrowPower);
			if (target.currentHp < 0)
			{
				target.DieSoldier();
			}
			GameManager.Instance.ObjectPool.ReturnToPool(gameObject);
		}
	}


	public void OnGetFromPool(Vector3 position, Quaternion rotation)
	{
		transform.SetPositionAndRotation(position, rotation);
	}

	private void ReleaseOrDestroy()
	{
		ObjectPool pool = GameManager.Instance != null ? GameManager.Instance.ObjectPool : null;
		if (pool != null)
		{
			pool.ReturnToPool(gameObject);
		}
		else
		{
			GameManager.Instance.ObjectPool.ReturnToPool(gameObject);
		}
	}

	public void ReleaseObjectPool()
	{
		target = null;
		arrowPower = 0f;
		arrowSpeed = 0f;
	}

}
