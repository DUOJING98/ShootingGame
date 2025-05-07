using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : Chara
{
    [SerializeField] bool regenerateHealth = true;
    [SerializeField] float healthRegenerateTime;
    [SerializeField,Range(0f,1f)] float healthRegeneratePercent;
    private Rigidbody2D rb;
    private Coroutine moveCoroutine;
    private Coroutine healthRegenerateCoroutine;
    [Header("----------INPUT------------")]
    [SerializeField] PlayerInput input;
    [Header("----------MOVE------------")]
    [SerializeField] float moveSpeed;
    [SerializeField] float accelerationTime = 3f;
    [SerializeField] float decelerationTime = 3f;
    [SerializeField] float moveRotarionAngle = 30;
    [SerializeField] float halfWidth;
    [SerializeField] float halfHeight;
    [Header("----------FIRE------------")]
    [SerializeField] GameObject Projectile1;
    [SerializeField] GameObject Projectile2;
    [SerializeField] GameObject Projectile3;
    [SerializeField, Range(0, 2)] int weaponPower = 0;
    [SerializeField] Transform muzzleMiddle;
    [SerializeField] Transform muzzleLeft;
    [SerializeField] Transform muzzleRight;
    [SerializeField] float fireInterval = 0.3f;
    WaitForSeconds waitFireInterval;
    WaitForSeconds waitHealthRegenerateTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onFire += Fire;
        input.onStopFire += StopFire;
    }

    private void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
        input.onFire -= Fire;
        input.onStopFire -= StopFire;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.gravityScale = 0;
        input.EnablePlayerInput();
        waitFireInterval = new WaitForSeconds(fireInterval);
        waitHealthRegenerateTime = new WaitForSeconds(healthRegenerateTime);

    }


    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (gameObject.activeSelf)
        {
            if (regenerateHealth)
            {
                if (healthRegenerateCoroutine != null)
                {
                    StopCoroutine(healthRegenerateCoroutine);
                }
                healthRegenerateCoroutine = StartCoroutine(HealthRegenerateCoroutine(waitHealthRegenerateTime, healthRegeneratePercent));
            }
        }

    }
    #region MOVE
    private void Move(Vector2 moveInput)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        Quaternion moveRotation = Quaternion.AngleAxis(moveRotarionAngle * moveInput.x, Vector3.down);
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveInput.normalized * moveSpeed, moveRotation));
        StartCoroutine(movePostionLimitCoroutine());
    }

    private void StopMove()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime, Vector2.zero, Quaternion.identity));
        StopCoroutine(movePostionLimitCoroutine());
    }
    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity, Quaternion moveRotation)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.fixedDeltaTime / time;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, moveVelocity, t / time);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, t / time);

            yield return null;
        }
    }


    IEnumerator movePostionLimitCoroutine()
    {
        while (true)
        {
            transform.position = ViewPort.instance.PlayerMoveablePostion(transform.position, halfWidth, halfHeight);

            yield return null;
        }
    }
    #endregion

    #region FIRE
    void Fire()
    {
        StartCoroutine(nameof(FireCoroutine));
    }

    void StopFire()
    {
        StopCoroutine(nameof(FireCoroutine));
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            //switch (weaponPower)
            //{
            //    case 0:
            //        Instantiate(Projectile1, muzzleMiddle.position, Quaternion.identity);
            //        break;
            //    case 1:
            //        Instantiate(Projectile2, muzzleRight.position, Quaternion.identity);
            //        Instantiate(Projectile3, muzzleLeft.position, Quaternion.identity);
            //        break;
            //    case 2:
            //        Instantiate(Projectile2, muzzleRight.position, Quaternion.identity);
            //        Instantiate(Projectile1, muzzleMiddle.position, Quaternion.identity);
            //        Instantiate(Projectile3, muzzleLeft.position, Quaternion.identity);
            //        break;
            //    default:
            //        break;
            //}

            switch (weaponPower)
            {
                case 0:
                    PoolManager.Release(Projectile1, muzzleMiddle.position);
                    break;
                case 1:
                    PoolManager.Release(Projectile2, muzzleRight.position);
                    PoolManager.Release(Projectile3, muzzleLeft.position);
                    break;
                case 2:
                    PoolManager.Release(Projectile1, muzzleMiddle.position);
                    PoolManager.Release(Projectile2, muzzleRight.position);
                    PoolManager.Release(Projectile3, muzzleLeft.position);
                    break;
                default:
                    break;
            }
            yield return waitFireInterval;
        }
    }
    #endregion

}
