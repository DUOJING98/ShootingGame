using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] PlayerProjectilePools;
    [SerializeField] Pool[] EnemyProjectilePools;
    static Dictionary<GameObject, Pool> dictionary;
    private void Start()
    {
        dictionary = new Dictionary<GameObject, Pool>();

        Initialize(PlayerProjectilePools);
        Initialize(EnemyProjectilePools);
    }
#if UNITY_EDITOR
    private void OnDestroy()
    {
        CheckPoolSize(PlayerProjectilePools);
        CheckPoolSize(EnemyProjectilePools);
    }
#endif
    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if(pool.runTimesize>pool.Size)
            {
                Debug.LogWarning(
                    string.Format("Pool:{0} has a runTime size {1} bigger than its initial size {2}!",
                    pool.Prefab.name,
                    pool.runTimesize,
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
        return dictionary[prefab].preparedObject();
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
        return dictionary[prefab].preparedObject(position);
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
        return dictionary[prefab].preparedObject(position,rotation);
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
        return dictionary[prefab].preparedObject(position, rotation,localScale);
    }
}
