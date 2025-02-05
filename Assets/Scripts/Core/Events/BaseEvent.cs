using System;

public abstract class BaseEvent : IEvent
{
    public BaseEvent(DateTime scheduledTime)
    {
        ScheduledTime = scheduledTime;
    }

    public DateTime ScheduledTime { get; }

    public event Action<IEvent> OnResolve;

    public abstract void Invoke();

    protected void Resolve()
    {
        UnityEngine.Debug.Log("Event resolved");
        OnResolve?.Invoke(this);
    }
}
