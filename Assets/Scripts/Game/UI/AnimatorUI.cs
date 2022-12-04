using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Events;

namespace Scripts.Game.UI
{
    public class AnimatorUI : MonoBehaviour
    {
        [SerializeField] private bool openOnStart = false;
        [SerializeField] private bool isOpen = false;
        private bool isTimeout = false;
        [SerializeField] private float openTime = 0.5f;
        [SerializeField] private float closeTime = 0.5f;
        [SerializeField] private float timeoutTime = 0.5f;

        public Vector2 openPosition = new Vector2(-250f, 0f);
        public Vector2 closePosition = new Vector2(-5000f, 0f);

        [SerializeField] private RectTransform layout;

        private Coroutine currentAnimation;
        private Coroutine timeoutTimer;
        private Coroutine offTimer;

        private UnityEvent openStartEvent = new UnityEvent();
        private UnityEvent openEndEvent = new UnityEvent();
        private UnityEvent closeStartEvent = new UnityEvent();
        private UnityEvent closeEndEvent = new UnityEvent();

        public event UnityAction OpenStartEvent
        {
            add => openStartEvent.AddListener(value);
            remove => openStartEvent.RemoveListener(value);
        }
        public event UnityAction OpenEndEvent
        {
            add => openEndEvent.AddListener(value);
            remove => openEndEvent.RemoveListener(value);
        }
        public event UnityAction CloseStartEvent
        {
            add => closeStartEvent.AddListener(value);
            remove => closeStartEvent.RemoveListener(value);
        }
        public event UnityAction CloseEndEvent
        {
            add => closeEndEvent.AddListener(value);
            remove => closeEndEvent.RemoveListener(value);
        }

        public bool IsOpen => isOpen;

        private void Awake()
        {
            //layout.gameObject.SetActive(isOpen);
            if (isOpen)
                layout.anchoredPosition = openPosition;
            else
                layout.anchoredPosition = closePosition;
        }
        private void Start()
        {
            if (openOnStart)
            {
                Open();
            }
        }

        public void Timeout()
        {
            if (timeoutTimer != null)
                StopCoroutine(timeoutTimer);

            isTimeout = true;
            timeoutTimer = StartCoroutine(TimeoutTimer());
        }
        public void OpenClose()
        {
            if (isOpen)
                Close();
            else
                Open();
        }
        public void Open()
        {
            if (isTimeout)
                return;
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            if (offTimer != null)
                StopCoroutine(offTimer);

            openStartEvent.Invoke();
            //layout.gameObject.SetActive(true);
            currentAnimation = StartCoroutine(Animation(layout, layout.anchoredPosition, openPosition, EasingType.InOutQuad, openTime));
            offTimer = StartCoroutine(OffTimer());
        }
        public void Close()
        {
            if (isTimeout)
                return;
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            if (offTimer != null)
                StopCoroutine(offTimer);

            closeStartEvent.Invoke();
            currentAnimation = StartCoroutine(Animation(layout, layout.anchoredPosition, closePosition, EasingType.InOutQuad, closeTime));
            offTimer = StartCoroutine(OffTimer());
        }
        private static IEnumerator Animation(RectTransform layout, Vector2 origin, Vector2 target, EasingType easingType, float time)
        {
            for (float i = 0; i <= time; i += Time.deltaTime)
            {
                yield return null;
                Vector2 current = origin + (target - origin) * Easings.GetEasing(i / time, easingType);
                layout.anchoredPosition = current;
            }
            layout.anchoredPosition = target;
        }
        private IEnumerator OffTimer()
        {
            yield return new WaitForSeconds(closeTime);
            if (isOpen)
                openEndEvent.Invoke();
            else
                closeEndEvent.Invoke();
            isOpen = !isOpen;
        }
        private IEnumerator TimeoutTimer()
        {
            yield return new WaitForSeconds(timeoutTime);
            isTimeout = false;
        }
    }

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
}