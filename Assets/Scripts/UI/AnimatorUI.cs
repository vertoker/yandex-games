using System.Collections;
using Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace UI
{
    public class AnimatorUI : MonoBehaviour
    {
        [SerializeField] private bool openOnStart = false;
        [SerializeField] private bool isOpen = false;
        [SerializeField] private bool isScalable = true;
        private bool isTimeout = false;
        [SerializeField] private float openTime = 0.5f;
        [SerializeField] private float closeTime = 0.5f;
        [SerializeField] private float timeoutTime = 0.5f;

        [SerializeField] private Vector2 openPosition = new Vector2(-250f, 0f);
        [SerializeField] private Vector2 closePosition = new Vector2(-5000f, 0f);
        private Vector2 scalable = new Vector2(1f, 1f);

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
                layout.anchoredPosition = openPosition * scalable;
            else
                layout.anchoredPosition = closePosition * scalable;
        }
        private void Start()
        {
            if (openOnStart)
            {
                Open();
            }
        }

        private void OnEnable()
        {
            if (!isScalable) return;
            ScreenCaller.ScreenOrientationChanged += ScreenUpdate;
        }
        private void OnDisable()
        {
            if (!isScalable) return;
            ScreenCaller.ScreenOrientationChanged -= ScreenUpdate;
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
            currentAnimation = StartCoroutine(Animation(layout, layout.anchoredPosition, openPosition * scalable, EasingType.InOutQuad, openTime));
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
            currentAnimation = StartCoroutine(Animation(layout, layout.anchoredPosition, closePosition * scalable, EasingType.InOutQuad, closeTime));
            offTimer = StartCoroutine(OffTimer());
        }
        public void OpenCustom(string pos)
        {
            if (isTimeout)
                return;
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            if (offTimer != null)
                StopCoroutine(offTimer);

            openStartEvent.Invoke();
            currentAnimation = StartCoroutine(Animation(layout, layout.anchoredPosition, Parse(pos) * scalable, EasingType.InOutQuad, openTime));
            offTimer = StartCoroutine(OffTimer());
        }
        public void CloseCustom(string pos)
        {
            if (isTimeout)
                return;
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            if (offTimer != null)
                StopCoroutine(offTimer);

            closeStartEvent.Invoke();
            currentAnimation = StartCoroutine(Animation(layout, layout.anchoredPosition, Parse(pos) * scalable, EasingType.InOutQuad, closeTime));
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

        private void ScreenUpdate(bool isVertical, float aspect)
        {
            var lastScale = scalable;
            scalable = new Vector2(aspect, 1f);
            layout.anchoredPosition *= scalable / lastScale;
        }

        private static Vector2 Parse(string obj)
        {
            var data = obj.Split(' ');
            return new Vector2(float.Parse(data[0]), float.Parse(data[1]));
        }
    }
}