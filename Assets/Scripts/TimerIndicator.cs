using System;
using Core;
using TMPro;
using UnityEngine;
using Zenject;

public class TimerIndicator : MonoBehaviour
{
    public TextMeshProUGUI Label;

    private TimeManager _timeManager;

    [Inject]
    public void Construct(TimeManager timeManager)
    {
        _timeManager = timeManager;
    }

    private void OnEnable()
    {
        _timeManager.OnTick += (timestamp) => OnTimerTick(timestamp);
    }

    private void OnDisable()
    {
        _timeManager.OnTick -= (timestamp) => OnTimerTick(timestamp);
    }

    private void OnTimerTick(DateTime timestamp)
    {
        Label.text = timestamp.ToString("dd.MM.yyyy HH:mm.ss");
    }
}
