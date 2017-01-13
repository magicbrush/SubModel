using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeToActivate : MonoBehaviour {

	public List<GameObject> _gameObjects;
	public float _timeToActivate;
	public bool _start = true;
	public bool _targetActivity = true;
	public bool _disableOnTrigger = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!_start)
			return;

		_timeToActivate -= Time.deltaTime;
		if (_timeToActivate > 0f)
			return;

		for (int i = 0; i < _gameObjects.Count; i++) {
			_gameObjects [i].SetActive (_targetActivity);
			//_gameObjects [i].SetActive (true);// debug
		}
			
		if (_disableOnTrigger) {
			enabled = false;
		}
	
	}

	public void ReadyToStart(bool bReady = true)
	{
		_start = bReady;
	}

}
