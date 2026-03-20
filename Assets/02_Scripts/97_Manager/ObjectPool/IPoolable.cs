using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{

    void OnGetFromPool(Vector3 position, Quaternion rotation);
}

public interface IReleasePoolable
{
    void ReleaseObjectPool();
}

public interface IInitializePoolable
{
    void Initialize(object data);
}
