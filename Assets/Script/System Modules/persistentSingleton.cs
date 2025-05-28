using UnityEngine;

public class persistentSingleton<T> : MonoBehaviour where T : Component
{
    public static T instance {  get; private set; }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
