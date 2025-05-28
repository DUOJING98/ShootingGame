using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] EnemyPools;
    [SerializeField] Pool[] PlayerProjectilePools;
    [SerializeField] Pool[] EnemyProjectilePools;
    [SerializeField] Pool[] vfxPools;
    static Dictionary<GameObject, Pool> dictionary;
     void Awake()
    {
        dictionary = new Dictionary<GameObject, Pool>();

        Initialize(EnemyPools);
        Initialize(PlayerProjectilePools);
        Initialize(EnemyProjectilePools);
        Initialize(vfxPools);
    }
#if UNITY_EDITOR
     void OnDestroy()
    {
        CheckPoolSize(EnemyPools);
        CheckPoolSize(PlayerProjectilePools);
        CheckPoolSize(EnemyProjectilePools);
        CheckPoolSize(vfxPools);
    }
#endif
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if(pool.RunTimesize>pool.Size)
            {
                Debug.LogWarning(
                    string.Format("Pool:{0} has a runTime size {1} bigger than its initial size {2}!",
                    pool.Prefab.name,
                    pool.RunTimesize,
                    pool.Size));
            }
        }
    }

    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
#if Unity_EDITOR
            if(dictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Same prefab in multiple pools Prefab:"+pool.Prefab.name);
                continue;
            }
#endif
            dictionary.Add(pool.Prefab, pool);
            Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

    public static GameObject Release(GameObject prefab)
    {
#if Unity_EDITOR
        if(!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager NOT Find prefab:"+prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject();
    }
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if Unity_EDITOR
        if(!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager NOT Find prefab:"+prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position);
    }
    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation)
    {
#if Unity_EDITOR
        if(!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager NOT Find prefab:"+prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position,rotation);
    }
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation,Vector3 localScale)
    {
#if Unity_EDITOR
        if(!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager NOT Find prefab:"+prefab.name);
            return null;
        }
#endif
        return dictionary[prefab].PreparedObject(position, rotation,localScale);
    }
}
