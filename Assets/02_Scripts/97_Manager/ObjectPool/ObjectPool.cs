using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 사용 가능한 오브젝트들을 모아두는 풀
    private Dictionary<string, List<GameObject>> availablePools = new Dictionary<string, List<GameObject>>();
    // 현재 사용중인 오브젝트들을 모아두는 풀
    private Dictionary<string, List<GameObject>> inUsePools = new Dictionary<string, List<GameObject>>();
    // 풀 컨테이너
    private Dictionary<string, GameObject> poolContainers = new Dictionary<string, GameObject>();



    /// <summary>
    /// 풀에서 꺼내오기
    /// </summary>
    /// <param name="poolable"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parentTransform"></param>
    /// <param name="data"></param>
    /// <returns></returns>
     public GameObject GetFromPool(IPoolable poolable, Vector3 position, Quaternion rotation, Transform parentTransform = null, object data = null)
    {
        Component poolableComponent = poolable as Component;
        if (poolableComponent == null)
        {
            Debug.LogError("GetFromPool: poolable must be a Component.");
            return null;
        }
        GameObject prefabObject = poolableComponent.gameObject;
        if (!availablePools.ContainsKey(prefabObject.name))
        {
            availablePools.Add(prefabObject.name, new List<GameObject>());
            if (!inUsePools.ContainsKey(prefabObject.name))
            {
                inUsePools.Add(prefabObject.name, new List<GameObject>());
            }
        }

        if (availablePools[prefabObject.name].Count == 0)
        {
            GameObject newObj = CreateNewObject(prefabObject, position, rotation, parentTransform);

            if (poolable is IInitializePoolable)
            {
                IInitializePoolable initializePoolable = newObj.GetComponent<IInitializePoolable>();
                initializePoolable?.Initialize(data);
            }

            return newObj;
        }

        GameObject obj = availablePools[prefabObject.name][availablePools[prefabObject.name].Count - 1];
        availablePools[prefabObject.name].RemoveAt(availablePools[prefabObject.name].Count - 1);
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

        obj.SetActive(true);
        inUsePools[prefabObject.name].Add(obj);

        return obj;
    }


    /// <summary>
    /// 풀이 비어있을 때 새로운 오브젝트 생성
    /// </summary>
    private GameObject CreateNewObject(GameObject prefabObject, Vector3 position, Quaternion rotation, Transform parentTransform = null)
    {
        GameObject newObj;
        // 새 객체 생성
        if (parentTransform == null)
        {
            newObj = Instantiate(prefabObject);
        }
        else
        {
            newObj = Instantiate(prefabObject, parentTransform);
        }

        newObj.name = prefabObject.name;

        // 객체 저장을 위한 부모 저장용 오브젝트풀 자식 객체
        if (!poolContainers.ContainsKey(prefabObject.name))
        {
            GameObject container = new GameObject($"Pool_{prefabObject.name}");
            container.transform.SetParent(transform);
            poolContainers[prefabObject.name] = container;
        }

        newObj.transform.position = position;
        newObj.transform.rotation = rotation;

        newObj.SetActive(true);
        inUsePools[prefabObject.name].Add(newObj);
        return newObj;
    }

    /// <summary>
    /// 인터페이스를 이용한 오브젝트풀 해제
    /// </summary>
    /// <param name="poolable"></param>
    public void ReleaseToPoolByInterface(IPoolable poolable)
    {
        Component poolableComponent = poolable as Component;
        if (poolableComponent == null)
        {
            return;
        }
        GameObject obj = poolableComponent.gameObject;

        if (poolable is IReleasePoolable)
        {
            IReleasePoolable releaseObject = obj.GetComponent<IReleasePoolable>();
            releaseObject?.ReleaseObjectPool();
        }

        if (!inUsePools.ContainsKey(obj.name))
        {
            Destroy(obj);
            return;
        }

        int index = inUsePools[obj.name].IndexOf(obj);
        if (index >= 0)
        {
            inUsePools[obj.name].RemoveAt(index);
            availablePools[obj.name].Add(obj);
            obj.SetActive(false);
            obj.transform.SetParent(poolContainers[obj.name].transform);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
        }
        else
        {
        }
    }


/// <summary>
    /// 풀에 오브젝트 반환 (사용 완료)
    /// </summary>
    public void ReleaseToPool(GameObject obj)
    {
        if (!inUsePools.ContainsKey(obj.name))
        {
            Destroy(obj);
            return;
        }

        int index = inUsePools[obj.name].IndexOf(obj);
        if (index >= 0)
        {
            inUsePools[obj.name].RemoveAt(index);
            availablePools[obj.name].Add(obj);
            obj.SetActive(false);
            obj.transform.SetParent(poolContainers[obj.name].transform);
        }
        else
        {
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        ReleaseToPool(obj);
    }

    /// <summary>
    /// 특정 풀 상태 확인
    /// </summary>
    public (int available, int inUse, int total) GetPoolStatus(string poolName)
    {
        if (!availablePools.ContainsKey(poolName))
            return (0, 0, 0);

        int available = availablePools[poolName].Count;
        int inUse = inUsePools[poolName].Count;
        return (available, inUse, available + inUse);
    }

    /// <summary>
    /// 모든 풀 해제
    /// </summary>
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


    /// <summary>
    /// 모든 풀 초기화
    /// </summary>
    public void ClearAllPools()
    {
        foreach (var pool in availablePools.Values)
        {
            foreach (GameObject obj in pool)
            {
                Destroy(obj);
            }
        }
        foreach (var pool in inUsePools.Values)
        {
            foreach (GameObject obj in pool)
            {
                Destroy(obj);
            }
        }
        availablePools.Clear();
        inUsePools.Clear();

        foreach (var container in poolContainers.Values)
        {
            Destroy(container);
        }
        poolContainers.Clear();
    }
}
