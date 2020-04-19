using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace LudumDare46
{
    [CreateAssetMenu(fileName = "NewEvents", menuName = "Game Event", order = 52)]
    public class Event : ScriptableObject
    {
        private List<EventListener> eventListeners = new List<EventListener>();

        public void Register(EventListener listener)
        {
            Debug.Log(string.Format("registering {0} on {1}", listener.name, name));
            eventListeners.Add(listener);
        }

        public void Unregister(EventListener listener)
        {
            eventListeners.Remove(listener);
        }

        public void Occurred(GameObject go)
        {
            for (int i = 0; i < eventListeners.Count; i++)
            {
                eventListeners[i].OnEventOccurs(go);
            }
        }

    }
}