using System.Collections;
using UnityEngine;

namespace Utility.Loading
{
    public class LoadIcon : MonoBehaviour
    {
        [SerializeField] private LoadIconFormula formulaPreset;
        [SerializeField] private GameObject origin;

        private Coroutine _coroutine;
        private RectTransform[] _points;

        private void Awake()
        {
            _points = new RectTransform[formulaPreset.PointsCount];
            for (var i = 0; i < formulaPreset.PointsCount; i++)
                _points[i] = Instantiate(origin, transform).GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            Play();
        }
        private void OnDisable()
        {
            Stop();
        }

        public void Play()
        {
            Stop();
            _coroutine = StartCoroutine(AnimationLoop());
        }
        public void Stop()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
        
        private IEnumerator AnimationLoop()
        {
            float progress = 0;
            while (true)
            {
                UpdateDots(progress);
                yield return null;
                progress += Time.deltaTime / formulaPreset.TimeScale;
                if (progress > 1)
                    progress--;
            }
        }
        private void UpdateDots(float progress)
        {
            var space = 1f / formulaPreset.PointsCount;
            for (var i = 0; i < formulaPreset.PointsCount; i++)
            {
                var currentProgress = Mathf.Repeat(progress + i * space, 1);
                _points[i].anchoredPosition = formulaPreset.Formula(currentProgress);
            }
        }
    }
}
