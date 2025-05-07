using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] GameObject[] projectiles;
    [SerializeField] Transform muzzle;
    [SerializeField] float paddingX;
    [SerializeField] float paddingY;
    [SerializeField] float moveSpeed;
    [SerializeField] float moveRotationAngle;
    [SerializeField] float minFireInterval;
    [SerializeField] float MaxFireInterval;

    private void Start()
    {
        StartCoroutine(RandomMoveCoroutine());
        StartCoroutine(RandomFireCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator RandomMoveCoroutine()
    {
        yield return new WaitUntil(() => ViewPort.instance != null);

        transform.position = ViewPort.instance.RandomEnemySpawnPosition(paddingX,paddingY );
        Vector3 targetPos = ViewPort.instance.RandomTopHalfPosition(paddingX, paddingY);

        while (gameObject.activeSelf)
        {
            if (Vector3.Distance(transform.position, targetPos) > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                Vector3 direction = (targetPos - transform.position).normalized;
                transform.rotation = Quaternion.AngleAxis(direction.x * moveRotationAngle, Vector3.down);
            }
            else
            {
                targetPos = ViewPort.instance.RandomTopHalfPosition(paddingX, paddingY);
            }

            yield return null;
        }
    }
    IEnumerator RandomFireCoroutine()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minFireInterval, MaxFireInterval));

            foreach(var projectile in projectiles)
            {
                PoolManager.Release(projectile,muzzle.position);
            }
        }
    }
}
