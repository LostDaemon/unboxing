using System;

public interface IEvent
{
    public DateTime ScheduledTime { get; }
    public void Invoke();
    public event Action<IEvent> OnResolve;
}
