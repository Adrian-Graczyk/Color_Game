using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Color_Game/GameEvent", order = 0)]
public class GameEvent : ScriptableObject {
    public List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(Component sender, object data) {
        foreach (var listener in listeners) {
            listener.OnEventRaised(sender, data);
        }
    }

    public void RegisterListener(GameEventListener listener) {
        if (!listeners.Contains(listener)) {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener) {
        if (listeners.Contains(listener)) {
            listeners.Remove(listener);
        }
    }
}

