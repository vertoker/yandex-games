using UnityEngine;

public static class Easings
{
    private const float c1 = 1.70158f;
    private const float c2 = c1 * 1.525f;
    private const float c3 = c1 + 1f;
    private const float c4 = 2 * Mathf.PI / 3;
    private const float c5 = 2 * Mathf.PI / 4.5f;

    public static float GetEasing(float x, EasingType easing)
    {
        switch (easing)
        {
            case EasingType.Linear:
                return x;
            case EasingType.Constant:
                return Mathf.Floor(x);

            case EasingType.InSine:
                return 1 - Mathf.Cos((x * Mathf.PI) / 2);
            case EasingType.OutSine:
                return Mathf.Sin((x * Mathf.PI) / 2);
            case EasingType.InOutSine:
                return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;

            case EasingType.InQuad:
                return x * x;
            case EasingType.OutQuad:
                return 1 - Mathf.Pow(1 - x, 2);
            case EasingType.InOutQuad:
                return x < 0.5f ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;

            case EasingType.InCubic:
                return x * x * x;
            case EasingType.OutCubic:
                return 1 - Mathf.Pow(1 - x, 3);
            case EasingType.InOutCubic:
                return x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;

            case EasingType.InQuart:
                return x * x * x * x;
            case EasingType.OutQuart:
                return 1 - Mathf.Pow(1 - x, 4);
            case EasingType.InOutQuart:
                return x < 0.5f ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;

            case EasingType.InQuint:
                return x * x * x * x * x;
            case EasingType.OutQuint:
                return 1 - Mathf.Pow(1 - x, 5);
            case EasingType.InOutQuint:
                return x < 0.5f ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;

            case EasingType.InExpo:
                return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
            case EasingType.OutExpo:
                return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
            case EasingType.InOutExpo:
                return x == 0 ? 0 : x == 1 ? 1 : x < 0.5f
                    ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;

            case EasingType.InCirc:
                return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
            case EasingType.OutCirc:
                return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
            case EasingType.InOutCirc:
                return x < 0.5f ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
                    : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;

            case EasingType.InBack:
                return c3 * x * x * x - c1 * x * x;
            case EasingType.OutBack:
                return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
            case EasingType.InOutBack:
                return x < 0.5f ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
                    : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;

            case EasingType.InElastic:
                return x == 0 ? 0 : x == 1 ? 1 : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
            case EasingType.OutElastic:
                return x == 0 ? 0 : x == 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
            case EasingType.InOutElastic:
                return x == 0 ? 0 : x == 1 ? 1 : x < 0.5f
                    ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
                    : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
        }
        return 0;
    }
}

public enum EasingType
{
    Linear = 0, Constant = 1,// без изменений
    InSine = 2, OutSine = 3, InOutSine = 4,// синусовые функции
    InQuad = 5, OutQuad = 6, InOutQuad = 7,// 2-степенные функции (квадрат)
    InCubic = 8, OutCubic = 9, InOutCubic = 10,// 3-степенные функции (куб)
    InQuart = 11, OutQuart = 12, InOutQuart = 13,// 4-степенные функции (тессеракт)
    InQuint = 14, OutQuint = 15, InOutQuint = 16,// 5-степенные функции
    InExpo = 17, OutExpo = 18, InOutExpo = 19,// экспанинциальные функции
    InCirc = 20, OutCirc = 21, InOutCirc = 22,// круговые функции
    InBack = 23, OutBack = 24, InOutBack = 25,// инерциальные функции
    InElastic = 26, OutElastic = 27, InOutElastic = 28// эластичные функции
}