using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class CustomEventBus
    {
        private Dictionary<Type, List<Delegate>> eventHandlers = new Dictionary<Type, List<Delegate>>();
        private Dictionary<Type, DateTime> lastEventTimes = new Dictionary<Type, DateTime>();
        private int throttleInterval;
        public CustomEventBus(int throttleInterval)
        {
            this.throttleInterval = throttleInterval;
        }
        public void RegisterHandler<TEvent>(Action<TEvent> handler)
        {
            Type eventType = typeof(TEvent);
            if (!eventHandlers.ContainsKey(eventType))
            {
                eventHandlers[eventType] = new List<Delegate>();
                lastEventTimes[eventType] = DateTime.MinValue;
            }
            eventHandlers[eventType].Add(handler);
        }
        public void UnregisterHandler<TEvent>(Action<TEvent> handler)
        {
            Type eventType = typeof(TEvent);
            if (eventHandlers.ContainsKey(eventType))
                eventHandlers[eventType].Remove(handler);
        }
        public void Dispatch<TEvent>(TEvent ev)
        {
            Type eventType = typeof(TEvent);
            DateTime lastEventTime = lastEventTimes[eventType];
            DateTime currentTime = DateTime.Now;
            TimeSpan elapsedTime = currentTime - lastEventTime;
            if (elapsedTime.TotalMilliseconds < throttleInterval)
                return;
            lastEventTimes[eventType] = currentTime;
            if (eventHandlers.ContainsKey(eventType))
                foreach (Delegate handler in eventHandlers[eventType])
                    if (handler is Action<TEvent> typedHandler)
                        typedHandler(ev);
        }
    }
}
