using UnityEngine;

public class ViewPort : Singleton<ViewPort>
{
    private float minX, minY;
    private float maxX, maxY;
    private float halfWidth;
    private float halfHeight;
    private float middleY;


    protected override void Awake()
    {
        base.Awake();
        Camera cam = Camera.main;
        Vector2 bottomLeft = cam.ViewportToWorldPoint(new Vector2(0f, 0f));
        Vector2 topRight = cam.ViewportToWorldPoint(new Vector2(1f, 1f));
        middleY = cam.ViewportToWorldPoint(new Vector2(0f, 0.5f)).y;
        minX = bottomLeft.x;
        minY = bottomLeft.y;
        maxX = topRight.x;
        maxY = topRight.y;
    }

    public Vector2 PlayerMoveablePostion(Vector2 pos, float halfWidth, float halfHeight)
    {
        Vector2 postion = Vector2.zero;

        postion.x = Mathf.Clamp(pos.x, minX + halfWidth, maxX - halfWidth);
        postion.y = Mathf.Clamp(pos.y, minY + halfHeight, maxY - halfHeight);
        return postion;
    }

    public Vector2 RandomEnemySpawnPosition(float halfWidth, float halfHeight)
    {
        Vector2 pos = Vector2.zero;
        pos.y = maxY + halfHeight;
        pos.x = Random.Range(minX + halfWidth, maxX - halfWidth);
        return pos;
    }

    public Vector2 RandomTopHalfPosition(float halfWidth, float halfHeight)
    {
        Vector2 pos = Vector2.zero;
        pos.y = Random.Range(middleY, maxY - halfHeight);
        pos.x = Random.Range(minX + halfWidth, maxX - halfWidth);
        return pos;
    }

    public Vector2 RandomTopPosition(float halfWidth, float halfHeight)
    {
        Vector2 pos = Vector2.zero;
        pos.y = Random.Range(minX + halfHeight, maxY - halfHeight);
        pos.x = Random.Range(minX + halfWidth, maxX - halfWidth);
        return pos;
    }
}
