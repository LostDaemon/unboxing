using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core;
using Zenject;
using UnityEngine;

public class EventScheduler : IDisposable
{
    private readonly TimeManager _timeManager;
    private List<IEvent> _scheduledEvents;
    private Queue<IEvent> _executionQueue;

    [Inject]
    public EventScheduler(TimeManager timeManager)
    {
        _timeManager = timeManager;
        _scheduledEvents = new List<IEvent>();
        _executionQueue = new Queue<IEvent>();
        _timeManager.OnTick += (timestamp) => OnTick(timestamp);
    }

    public void Dispose()
    {
        _timeManager.OnTick -= (timestamp) => OnTick(timestamp);
    }

    public void RegisterEvent(IEvent eventToRegister)
    {
        _scheduledEvents.Add(eventToRegister);
        UnityEngine.Debug.Log("Event registered");
    }

    public void UnregisterEvent(IEvent eventToUnregister)
    {
        _scheduledEvents.Remove(eventToUnregister);
    }

    private void OnTick(DateTime timeStamp)
    {
        var eventToFire = _scheduledEvents.Where(c => c.ScheduledTime < timeStamp).ToList();
        _scheduledEvents.RemoveAll(c => eventToFire.Contains(c));

        eventToFire.ForEach(e => e.Invoke()); //TODO!!!
        return;
        //eventToFire.ForEach(e => _executionQueue.Enqueue(e));
        //SubscribeNext();
    }

    private void OnResolve(IEvent sender)
    {
        sender.OnResolve -= (sender) => OnResolve(sender);
        SubscribeNext();
    }

    private void SubscribeNext()
    {

        var next = _executionQueue.Dequeue();
        next.OnResolve += (sender) => OnResolve(sender);
        next.Invoke();
    }
}
