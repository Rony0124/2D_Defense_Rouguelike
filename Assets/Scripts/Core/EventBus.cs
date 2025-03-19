using System.Collections.Generic;
using UnityEngine.Events;
using Util;

namespace InGame
{
    public static class EventBus
    {
        private static readonly IDictionary<Define.GameState, UnityEvent> Events 
            = new Dictionary<Define.GameState, UnityEvent>();
        
        public static void Register(Define.GameState gameState, UnityAction action)
        {
            if (!Events.ContainsKey(gameState))
                Events.Add(gameState, new UnityEvent());

            Events[gameState].AddListener(action);
        }
        
        public static void Unregister(Define.GameState gameState, UnityAction action)
        {
            if (Events.TryGetValue(gameState, out var @event))
            {
                @event.RemoveListener(action);
            }
        }
        
        public static void Publish(Define.GameState gameState)
        {
            if (Events.TryGetValue(gameState, out var @event))
            {
                @event.Invoke();
            }
        }

    }
}
