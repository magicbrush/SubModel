using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class LineGameEventSystem : MonoBehaviour {

	public static Dictionary <string, UnityEvent> eventDictionary;

	private static LineGameEventSystem eventManager;

	public static LineGameEventSystem instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof (LineGameEventSystem)) as LineGameEventSystem;

				if (!eventManager)
				{
					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init (); 
				}
			}

			return eventManager;
		}
	}

	void Init ()
	{

		UnityAction ua;

		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
	}

	public static void StartListening (string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if (LineGameEventSystem.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			LineGameEventSystem.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening (string eventName, UnityAction listener)
	{
		if (eventManager == null) return;
		UnityEvent thisEvent = null;
		if (LineGameEventSystem.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (string eventName)
	{
		UnityEvent thisEvent = null;
		if (LineGameEventSystem.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}
}
