using UnityEngine;

public class ViewPort : Singleton<ViewPort>
{
    float minX, minY;
    float maxX, maxY;
    private float halfWidth;
    private float halfHeight;


    private void Start()
    {
        Camera cam = Camera.main;
        Vector2 bottomLeft = cam.ViewportToWorldPoint(new Vector2(0f, 0f));
        Vector2 topRight = cam.ViewportToWorldPoint(new Vector2(1f, 1f));
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
}
