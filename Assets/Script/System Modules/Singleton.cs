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
            //Debug.LogWarning($"�ظ��ĵ���ʵ�� {typeof(T)}������ {gameObject.name}");
            Destroy(gameObject);
        }
    }
}
