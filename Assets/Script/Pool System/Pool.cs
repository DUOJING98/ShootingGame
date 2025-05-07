using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public GameObject Prefab { get => prefab; }
    public int Size=>size;
    public int runTimesize=>queue.Count;
    [SerializeField] GameObject prefab;
    [SerializeField] int size = 1;

    Queue<GameObject> queue;

    Transform parent;

    public void Initialize(Transform parent)
    {
        this.parent = parent;
        queue = new Queue<GameObject>();

        for (var i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab, parent);
        copy.SetActive(false);
        return copy;
    }

    GameObject AvailableObject()
    {
        GameObject availableObject = null;
        if (queue.Count > 0)
        {
            availableObject = queue.Dequeue();

        }
        else
        {
            availableObject = Copy();
        }
        return availableObject;
    }
    public GameObject preparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);

        return preparedObject;
    }
    public GameObject preparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        return preparedObject;
    }
    public GameObject preparedObject(Vector3 position, Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        return preparedObject;
    }
    public GameObject preparedObject(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;
        return preparedObject;
    }

    public void Return(GameObject gameObject)
    {
        queue.Enqueue(gameObject);
    }
}
