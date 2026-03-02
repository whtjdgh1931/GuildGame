using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<string, List<GameObject>> availablePools = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, List<GameObject>> inUsePools = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, GameObject> poolContainers = new Dictionary<string, GameObject>();
    // InstanceID -> original pool key
    private Dictionary<int, string> objectPoolKeys = new Dictionary<int, string>();

    public GameObject GetFromPool(IPoolable poolable, Vector3 position, Quaternion rotation, Transform parentTransform = null, object data = null)
    {
        Component poolableComponent = poolable as Component;
        if (poolableComponent == null)
        {
            Debug.LogError("GetFromPool: poolable must be a Component.");
            return null;
        }

        GameObject prefabObject = poolableComponent.gameObject;
        string poolKey = prefabObject.name;
        EnsurePool(poolKey);

        if (availablePools[poolKey].Count == 0)
        {
            GameObject newObj = CreateNewObject(prefabObject, position, rotation, parentTransform, poolKey);

            if (poolable is IInitializePoolable)
            {
                IInitializePoolable initializePoolable = newObj.GetComponent<IInitializePoolable>();
                initializePoolable?.Initialize(data);
            }
            newObj.GetComponent<IPoolable>()?.OnGetFromPool(position, rotation);
            return newObj;
        }

        GameObject obj = availablePools[poolKey][availablePools[poolKey].Count - 1];
        availablePools[poolKey].RemoveAt(availablePools[poolKey].Count - 1);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        if (parentTransform != null)
        {
            obj.transform.SetParent(parentTransform);
        }

        if (poolable is IInitializePoolable)
        {
            IInitializePoolable initializePoolable = obj.GetComponent<IInitializePoolable>();
            initializePoolable?.Initialize(data);
        }

        obj.GetComponent<IPoolable>()?.OnGetFromPool(position, rotation);

        obj.SetActive(true);
        inUsePools[poolKey].Add(obj);
        objectPoolKeys[obj.GetInstanceID()] = poolKey;

        return obj;
    }

    private void EnsurePool(string poolKey)
    {
        if (!availablePools.ContainsKey(poolKey))
        {
            availablePools.Add(poolKey, new List<GameObject>());
        }

        if (!inUsePools.ContainsKey(poolKey))
        {
            inUsePools.Add(poolKey, new List<GameObject>());
        }

        if (!poolContainers.ContainsKey(poolKey))
        {
            GameObject container = new GameObject($"{poolKey}");
            container.transform.SetParent(transform);
            poolContainers[poolKey] = container;
        }
    }

    private GameObject CreateNewObject(GameObject prefabObject, Vector3 position, Quaternion rotation, Transform parentTransform, string poolKey)
    {
        GameObject newObj;
        if (parentTransform == null)
        {
            newObj = Instantiate(prefabObject);
        }
        else
        {
            newObj = Instantiate(prefabObject, parentTransform);
        }

        newObj.name = prefabObject.name;
        newObj.transform.position = position;
        newObj.transform.rotation = rotation;
        newObj.SetActive(true);

        EnsurePool(poolKey);
        inUsePools[poolKey].Add(newObj);
        objectPoolKeys[newObj.GetInstanceID()] = poolKey;
        return newObj;
    }

    public void ReleaseToPool(GameObject obj)
    {
        if (obj == null)
        {
            return;
        }

        int instanceId = obj.GetInstanceID();
        if (!objectPoolKeys.TryGetValue(instanceId, out string poolKey))
        {
            poolKey = obj.name;
        }

        if (!inUsePools.ContainsKey(poolKey))
        {
            Destroy(obj);
            objectPoolKeys.Remove(instanceId);
            return;
        }

        IReleasePoolable poolableComponent = obj.GetComponent<IReleasePoolable>();
        if (poolableComponent != null)
        {
            poolableComponent.ReleaseObjectPool();
        }

        int index = inUsePools[poolKey].IndexOf(obj);
        if (index >= 0)
        {
            inUsePools[poolKey].RemoveAt(index);
            availablePools[poolKey].Add(obj);
            obj.SetActive(false);
            obj.transform.SetParent(poolContainers[poolKey].transform);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        ReleaseToPool(obj);
    }

    public void ReturnToPool(GameObject obj, float delay)
    {
        StartCoroutine(ReturnToPoolWithDelay(obj, delay));
    }

    private System.Collections.IEnumerator ReturnToPoolWithDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReleaseToPool(obj);
    }

    public (int available, int inUse, int total) GetPoolStatus(string poolName)
    {
        if (!availablePools.ContainsKey(poolName))
            return (0, 0, 0);

        int available = availablePools[poolName].Count;
        int inUse = inUsePools[poolName].Count;
        return (available, inUse, available + inUse);
    }

    public void ReleaseAllPools()
    {
        foreach (var poolName in inUsePools.Keys)
        {
            var inUseList = inUsePools[poolName];
            for (int i = inUseList.Count - 1; i >= 0; i--)
            {
                GameObject obj = inUseList[i];
                ReleaseToPool(obj);
            }
        }
    }

    public void ClearAllPools()
    {
        foreach (var pool in availablePools.Values)
        {
            foreach (GameObject obj in pool)
            {
                if (obj != null)
                {
                    objectPoolKeys.Remove(obj.GetInstanceID());
                    Destroy(obj);
                }
            }
        }

        foreach (var pool in inUsePools.Values)
        {
            foreach (GameObject obj in pool)
            {
                if (obj != null)
                {
                    objectPoolKeys.Remove(obj.GetInstanceID());
                    Destroy(obj);
                }
            }
        }

        availablePools.Clear();
        inUsePools.Clear();

        foreach (var container in poolContainers.Values)
        {
            if (container != null)
            {
                Destroy(container);
            }
        }

        poolContainers.Clear();
        objectPoolKeys.Clear();
    }
}
