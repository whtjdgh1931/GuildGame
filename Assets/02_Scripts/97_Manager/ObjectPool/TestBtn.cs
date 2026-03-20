using System.Collections;
using UnityEngine;

public class TestBtn : MonoBehaviour
{
    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Soldier targetSoldier;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private float durationSeconds = 5f;
    [SerializeField] private int totalShots = 500;

    private Coroutine arrowStressCoroutine;

    public void StartArrowStressTest()
    {
        if (arrowPrefab == null)
        {
            Debug.LogWarning("StartArrowStressTest: arrowPrefab is null.");
            return;
        }
        if (durationSeconds <= 0f || totalShots <= 0)
        {
            Debug.LogWarning("StartArrowStressTest: durationSeconds and totalShots must be greater than 0.");
            return;
        }
        if (!TryResolveObjectPool(out _))
        {
            Debug.LogWarning("StartArrowStressTest: ObjectPool reference is missing.");
            return;
        }

        if (arrowStressCoroutine != null)
        {
            StopCoroutine(arrowStressCoroutine);
        }

        arrowStressCoroutine = StartCoroutine(FireArrowsForDuration());
    }

    private IEnumerator FireArrowsForDuration()
    {
        int fired = 0;
        float startTime = Time.time;
        float interval = durationSeconds / totalShots;
        Transform spawn = firePoint != null ? firePoint : transform;

        while (fired < totalShots && Time.time - startTime < durationSeconds)
        {
            if (!TryResolveObjectPool(out ObjectPool pool))
            {
                Debug.LogWarning("FireArrowsForDuration: ObjectPool reference lost during test.");
                break;
            }

            GameObject spawned = pool.GetFromPool(arrowPrefab, spawn.position, spawn.rotation);
            if (spawned == null)
            {
                fired++;
                yield return new WaitForSeconds(interval);
                continue;
            }

            Arrow arrow = spawned.GetComponent<Arrow>();

            if (arrow != null)
            {
                arrow.target = targetSoldier;
            }

            fired++;
            yield return new WaitForSeconds(interval);
        }

        arrowStressCoroutine = null;
    }

    private bool TryResolveObjectPool(out ObjectPool resolvedPool)
    {
        if (objectPool != null)
        {
            resolvedPool = objectPool;
            return true;
        }

        if (GameManager.Instance != null && GameManager.Instance.ObjectPool != null)
        {
            objectPool = GameManager.Instance.ObjectPool;
            resolvedPool = objectPool;
            return true;
        }

        objectPool = FindAnyObjectByType<ObjectPool>();
        resolvedPool = objectPool;
        return resolvedPool != null;
    }
}
