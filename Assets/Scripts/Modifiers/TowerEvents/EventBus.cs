using System.Collections.Generic;
using System;


public class EventBus
{
    private Dictionary<Type, List<Delegate>> _eventListeners;

    public EventBus()
    {
        _eventListeners = new Dictionary<Type, List<Delegate>>();
    }

    public void Subscribe<T>(Action<T> handler) where T : ITowerEvent
    {
        Type eventType = typeof(T);
        if (!_eventListeners.ContainsKey(eventType))
        {
            _eventListeners[eventType] = new List<Delegate>();
        }
        _eventListeners[eventType].Add(handler);
    }

    public void Unsubscribe<T>(Action<T> handler) where T : ITowerEvent
    {
        Type eventType = typeof(T);
        if (_eventListeners.ContainsKey(eventType))
        {
            if (_eventListeners[eventType].Contains(handler))
            {
                _eventListeners[eventType].Remove(handler);
            }
            else
            {
                throw new ArgumentException($"Handler {handler} not found for event type {eventType}");
            }
        }
    }

    public void RaiseEvent<T>(T towerEvent) where T : ITowerEvent
    {
        Type eventType = typeof(T);
        if (_eventListeners.ContainsKey(eventType))
        {
            var handlers = new List<Delegate>(_eventListeners[eventType]);
            foreach (var handler in handlers)
            {
                if (handler is Action<T> action)
                {
                    action(towerEvent);
                }
            }
        }
    }
}