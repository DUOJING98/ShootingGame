using System.Collections;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    public bool destroyGameObject;
    public float lifeTime = 3f;
    WaitForSeconds waitLifetime;

    private void Awake()
    {
        waitLifetime = new WaitForSeconds(lifeTime);
    }

    private void OnEnable()
    {
        StartCoroutine(DeactivateCoroutine());
    }

    IEnumerator DeactivateCoroutine()
    {
        yield return waitLifetime;

        if(destroyGameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
