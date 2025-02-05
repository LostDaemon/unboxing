using Core;
using UnityEngine;
using Zenject;

public class TimeManagerController : MonoBehaviour
{
    private static TimeManagerController Instance;

    [SerializeField]
    private float _cooldownInSeconds = 1f;
    private TimeManager _timeManager;

    [Inject]
    public void Construct(TimeManager timeManager)
    {
        _timeManager = timeManager;
    }

    void FixedUpdate()
    {
        if (Time.fixedTime % _cooldownInSeconds < Time.fixedDeltaTime)
        {
            _timeManager.Tick();
        }
    }
}
