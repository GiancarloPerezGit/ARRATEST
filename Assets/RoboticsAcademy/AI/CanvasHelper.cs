using UnityEngine;

public static class CanvasHelper
{
    public static bool WithinCanvasBounds(Vector2 pos, Canvas canv)
    {
        RectTransform r = canv.GetComponent<RectTransform>();
        float minx = r.rect.min.x;
        float miny = r.rect.min.y;
        float maxx = r.rect.max.x;
        float maxy = r.rect.max.y;

        if ((pos.x > maxx || pos.x < minx) || (pos.y > maxy || pos.y < miny))
        {
            return false;
        }
        else return true;
    }

    public static Vector2 GetPositionOnCanvas(Vector3 pos, Canvas canv)
    {
        Vector3 newPos = canv.transform.InverseTransformPoint(pos);
        return new Vector2(newPos.x, newPos.y);
    }
}
