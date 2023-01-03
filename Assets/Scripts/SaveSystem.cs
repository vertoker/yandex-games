using System;
using YG;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private UnityEvent beginData = new UnityEvent();
    [SerializeField] private UnityEvent loadData = new UnityEvent();

    private void Awake()
    {
        beginData.Invoke();
    }
    private void OnEnable()
    {
        YandexGame.GetDataEvent += LoadCloudData;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadCloudData;
    }

    private void LoadCloudData()
    {
        YandexGame.SaveProgress();
        loadData.Invoke();
    }
}
