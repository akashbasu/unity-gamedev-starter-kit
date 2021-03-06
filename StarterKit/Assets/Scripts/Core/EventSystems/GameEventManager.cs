using System;
using System.Collections.Generic;

namespace Core.EventSystems
{
    internal interface IGameEventManager
    {
        void Subscribe(string gameEvent, Action<object[]> callback);
        void Unsubscribe(string gameEvent, Action<object[]> callback);
        void Broadcast(string gameEvent, params object[] args);
    }
    
    internal class GameEventManager : IGameEventManager
    {
        private readonly Dictionary<string, List<Action<object[]>>> _eventDirectory = new Dictionary<string, List<Action<object[]>>>();

        public void Subscribe(string gameEvent, Action<object[]> callback)
        {
            if (_eventDirectory.ContainsKey(gameEvent))
                _eventDirectory[gameEvent].RemoveAll(x => x == callback);
            else
                _eventDirectory[gameEvent] = new List<Action<object[]>>();

            _eventDirectory[gameEvent].Add(callback);
        }

        public void Unsubscribe(string gameEvent, Action<object[]> callback)
        {
            if (!_eventDirectory.ContainsKey(gameEvent)) return;

            var callbacks = _eventDirectory[gameEvent];
            callbacks?.RemoveAll(x => x == callback);
        }

        public void Broadcast(string gameEvent, params object[] args)
        {
            if (!_eventDirectory.ContainsKey(gameEvent)) return;
            
            if(_eventDirectory[gameEvent] == null) return;

            var callbacks = _eventDirectory[gameEvent];
            var count = callbacks?.Count ?? default;
            for (var i = count - 1; i >= 0; i--)
            {
                callbacks[i]?.Invoke(args);
            }
        }
    }
}