using UnityEngine;
using System.Collections;

public static class Lerper
{
    public enum LerpType { Normal, FadeOut, FadeIn, Exponential, Smooth, Smoother };

    #region Lerp Int
    public static int Lerp(int startPoint, int endPoint, float lerpPosition, LerpType lerpType)
    {
        switch (lerpType)
        {
            default:
            case LerpType.Normal:
                return NormalLerp(startPoint, endPoint, lerpPosition);
            case LerpType.FadeOut:
                return FadeOut(startPoint, endPoint, lerpPosition);
            case LerpType.FadeIn:
                return FadeIn(startPoint, endPoint, lerpPosition);
            case LerpType.Exponential:
                return Exponential(startPoint, endPoint, lerpPosition);
            case LerpType.Smooth:
                return SmoothLerp(startPoint, endPoint, lerpPosition);
            case LerpType.Smoother:
                return SmootherLerp(startPoint, endPoint, lerpPosition);
        }
    }

    public static int NormalLerp(int startPoint, int endPoint, float lerpPosition)
    {
        return (int)Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static int FadeOut(int startPoint, int endPoint, float lerpPosition)
    {
        lerpPosition = Mathf.Sin(lerpPosition * Mathf.PI * 0.5f);
        return (int)Mathf.RoundToInt( Mathf.Lerp(startPoint, endPoint, lerpPosition));
    }

    public static int FadeIn(int startPoint, int endPoint, float lerpPosition)
    {
        lerpPosition = (1f - Mathf.Cos(lerpPosition * Mathf.PI * 0.5f));
        return (int)Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static int Exponential(int startPoint, int endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition;
        return (int)Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static int SmoothLerp(int startPoint, int endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * (3f - 2f * lerpPosition);
        return (int)Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static int SmootherLerp(int startPoint, int endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * lerpPosition * (lerpPosition * (6f * lerpPosition - 15f) + 10f);
        return (int)Mathf.RoundToInt(Mathf.Lerp(startPoint, endPoint, lerpPosition));
    }
    #endregion
    #region Lerp Float
    public static float Lerp(float startPoint, float endPoint, float lerpPosition, LerpType lerpType)
    {
        switch(lerpType)
        {
            default:
            case LerpType.Normal:
                return NormalLerp(startPoint, endPoint, lerpPosition);
            case LerpType.FadeOut:
                return FadeOut(startPoint, endPoint, lerpPosition);
            case LerpType.FadeIn:
                return FadeIn(startPoint, endPoint, lerpPosition);
            case LerpType.Exponential:
                return Exponential(startPoint, endPoint, lerpPosition);
            case LerpType.Smooth:
                return SmoothLerp(startPoint, endPoint, lerpPosition);
            case LerpType.Smoother:
                return SmootherLerp(startPoint, endPoint, lerpPosition);
        }
    }

    public static float NormalLerp(float startPoint, float endPoint, float lerpPosition)
    {
        return Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static float FadeOut(float startPoint, float endPoint, float lerpPosition)
    {
        lerpPosition = Mathf.Sin(lerpPosition * Mathf.PI * 0.5f);
        return Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static float FadeIn(float startPoint, float endPoint, float lerpPosition)
    {
        lerpPosition = (1f - Mathf.Cos(lerpPosition * Mathf.PI * 0.5f));
        return Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static float Exponential(float startPoint, float endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition;
        return Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static float SmoothLerp(float startPoint, float endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * (3f - 2f * lerpPosition);
        return Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static float SmootherLerp(float startPoint, float endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * lerpPosition * (lerpPosition * (6f * lerpPosition - 15f) + 10f);
        return Mathf.Lerp(startPoint, endPoint, lerpPosition);
    }
    #endregion
    #region Lerp Vector2
    public static Vector2 Lerp(Vector2 startPoint, Vector2 endPoint, float lerpPosition, LerpType lerpType)
    {
        switch (lerpType)
        {
            default:
            case LerpType.Normal:
                return NormalLerp(startPoint, endPoint, lerpPosition);
            case LerpType.FadeOut:
                return FadeOut(startPoint, endPoint, lerpPosition);
            case LerpType.FadeIn:
                return FadeIn(startPoint, endPoint, lerpPosition);
            case LerpType.Exponential:
                return Exponential(startPoint, endPoint, lerpPosition);
            case LerpType.Smooth:
                return SmoothLerp(startPoint, endPoint, lerpPosition);
            case LerpType.Smoother:
                return SmootherLerp(startPoint, endPoint, lerpPosition);
        }
    }

    public static Vector2 NormalLerp(Vector2 startPoint, Vector2 endPoint, float lerpPosition)
    {
        return Vector2.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector2 FadeOut(Vector2 startPoint, Vector2 endPoint, float lerpPosition)
    {
        lerpPosition = Mathf.Sin(lerpPosition * Mathf.PI * 0.5f);
        return Vector2.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector2 FadeIn(Vector2 startPoint, Vector2 endPoint, float lerpPosition)
    {
        lerpPosition = (1f - Mathf.Cos(lerpPosition * Mathf.PI * 0.5f));
        return Vector2.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector2 Exponential(Vector2 startPoint, Vector2 endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition;
        return Vector2.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector2 SmoothLerp(Vector2 startPoint, Vector2 endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * (3f - 2f * lerpPosition);
        return Vector2.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector2 SmootherLerp(Vector2 startPoint, Vector2 endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * lerpPosition * (lerpPosition * (6f * lerpPosition - 15f) + 10f);
        return Vector2.Lerp(startPoint, endPoint, lerpPosition);
    }
    #endregion
    #region Lerp Vector3
    public static Vector3 Lerp(Vector3 startPoint, Vector3 endPoint, float lerpPosition, LerpType lerpType)
    {
        switch (lerpType)
        {
            default:
            case LerpType.Normal:
                return NormalLerp(startPoint, endPoint, lerpPosition);
            case LerpType.FadeOut:
                return FadeOut(startPoint, endPoint, lerpPosition);
            case LerpType.FadeIn:
                return FadeIn(startPoint, endPoint, lerpPosition);
            case LerpType.Exponential:
                return Exponential(startPoint, endPoint, lerpPosition);
            case LerpType.Smooth:
                return SmoothLerp(startPoint, endPoint, lerpPosition);
            case LerpType.Smoother:
                return SmootherLerp(startPoint, endPoint, lerpPosition);
        }
    }

    public static Vector3 NormalLerp(Vector3 startPoint, Vector3 endPoint, float lerpPosition)
    {
        return Vector3.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector3 FadeOut(Vector3 startPoint, Vector3 endPoint, float lerpPosition)
    {
        lerpPosition = Mathf.Sin(lerpPosition * Mathf.PI * 0.5f);
        return Vector3.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector3 FadeIn(Vector3 startPoint, Vector3 endPoint, float lerpPosition)
    {
        lerpPosition = (1f - Mathf.Cos(lerpPosition * Mathf.PI * 0.5f));
        return Vector3.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector3 Exponential(Vector3 startPoint, Vector3 endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition;
        return Vector3.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector3 SmoothLerp(Vector3 startPoint, Vector3 endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * (3f - 2f * lerpPosition);
        return Vector3.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Vector3 SmootherLerp(Vector3 startPoint, Vector3 endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * lerpPosition * (lerpPosition * (6f * lerpPosition - 15f) + 10f);
        return Vector3.Lerp(startPoint, endPoint, lerpPosition);
    }
    #endregion
    #region Lerp Color
    public static Color Lerp(Color startPoint, Color endPoint, float lerpPosition, LerpType lerpType)
    {
        switch (lerpType)
        {
            default:
            case LerpType.Normal:
                return NormalLerp(startPoint, endPoint, lerpPosition);
            case LerpType.FadeOut:
                return FadeOut(startPoint, endPoint, lerpPosition);
            case LerpType.FadeIn:
                return FadeIn(startPoint, endPoint, lerpPosition);
            case LerpType.Exponential:
                return Exponential(startPoint, endPoint, lerpPosition);
            case LerpType.Smooth:
                return SmoothLerp(startPoint, endPoint, lerpPosition);
            case LerpType.Smoother:
                return SmootherLerp(startPoint, endPoint, lerpPosition);
        }
    }

    public static Color NormalLerp(Color startPoint, Color endPoint, float lerpPosition)
    {
        return Color.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Color FadeOut(Color startPoint, Color endPoint, float lerpPosition)
    {
        lerpPosition = Mathf.Sin(lerpPosition * Mathf.PI * 0.5f);
        return Color.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Color FadeIn(Color startPoint, Color endPoint, float lerpPosition)
    {
        lerpPosition = (1f - Mathf.Cos(lerpPosition * Mathf.PI * 0.5f));
        return Color.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Color Exponential(Color startPoint, Color endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition;
        return Color.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Color SmoothLerp(Color startPoint, Color endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * (3f - 2f * lerpPosition);
        return Color.Lerp(startPoint, endPoint, lerpPosition);
    }

    public static Color SmootherLerp(Color startPoint, Color endPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * lerpPosition * (lerpPosition * (6f * lerpPosition - 15f) + 10f);
        return Color.Lerp(startPoint, endPoint, lerpPosition);
    }
    #endregion
    #region Lerp String
    public static string Lerp(string startPoint, float lerpPosition, LerpType lerpType)
    {
        switch (lerpType)
        {
            default:
            case LerpType.Normal:
                return NormalLerp(startPoint, lerpPosition);
            case LerpType.FadeOut:
                return FadeOut(startPoint, lerpPosition);
            case LerpType.FadeIn:
                return FadeIn(startPoint, lerpPosition);
            case LerpType.Exponential:
                return Exponential(startPoint, lerpPosition);
            case LerpType.Smooth:
                return SmoothLerp(startPoint, lerpPosition);
            case LerpType.Smoother:
                return SmootherLerp(startPoint, lerpPosition);
        }
    }

    public static string NormalLerp(string startPoint, float lerpPosition)
    {
        return startPoint.Substring(0, (int)(startPoint.Length * lerpPosition));
    }

    public static string FadeOut(string startPoint, float lerpPosition)
    {
        lerpPosition = Mathf.Sin(lerpPosition * Mathf.PI * 0.5f);
        return startPoint.Substring(0, (int)(startPoint.Length * lerpPosition));
    }

    public static string FadeIn(string startPoint, float lerpPosition)
    {
        lerpPosition = (1f - Mathf.Cos(lerpPosition * Mathf.PI * 0.5f));
        return startPoint.Substring(0, (int)(startPoint.Length * lerpPosition));
    }

    public static string Exponential(string startPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition;
        return startPoint.Substring(0, (int)(startPoint.Length * lerpPosition));
    }

    public static string SmoothLerp(string startPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * (3f - 2f * lerpPosition);
        return startPoint.Substring(0, (int)(startPoint.Length * lerpPosition));
    }

    public static string SmootherLerp(string startPoint, float lerpPosition)
    {
        lerpPosition = lerpPosition * lerpPosition * lerpPosition * (lerpPosition * (6f * lerpPosition - 15f) + 10f);
        return startPoint.Substring(0, (int)(startPoint.Length * lerpPosition));
    }
    #endregion
}
