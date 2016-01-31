using UnityEngine;
using System.Collections;

public static class Bezier
{
    public static Vector2 CalculateBezierPoint(Vector2 start, Vector2 handleA, Vector2 handleB, Vector2 end, float position)
    {
        float u = 1 - position;
        float tt = position * position;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * position;

        Vector2 p = uuu * start;
        p += 3 * uu * position * handleA;
        p += 3 * u * tt * handleB;
        p += ttt * end;

        return p;
    }

    public static Vector3 CalculateBezierPoint(Vector3 start, Vector3 handleA, Vector3 handleB, Vector3 end, float position)
    {
        float u = 1 - position;
        float tt = position * position;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * position;

        Vector3 p = uuu * start;
        p += 3 * uu * position * handleA;
        p += 3 * u * tt * handleB;
        p += ttt * end;

        return p;
    }
}
