using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            //Debug.LogWarning($"重复的单例实例 {typeof(T)}，销毁 {gameObject.name}");
            Destroy(gameObject);
        }
    }
}
