using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Coroutine moveCoroutine;
    public PlayerInput input;
    public float moveSpeed;
    public float accelerationTime = 3f;
    public float decelerationTime = 3f;
    public float moveRotarionAngle = 30;
    public float halfWidth;
    public float halfHeight;
    public GameObject Projectile;
    public Transform muzzle;
    public float fireInterval = 0.3f;
    WaitForSeconds waitFireInterval;

    private void OnEnable()
    {
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
            Instantiate(Projectile, muzzle.position, Quaternion.identity);

            yield return waitFireInterval;
        }
    }
    #endregion

}
