using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TimeToInvoke : MonoBehaviour {

	public UnityEvent _OnTimeOver;

	public float _LeftTime = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_LeftTime <= 0.0f)
			return;

		float leftT = _LeftTime - Time.deltaTime;
		if (leftT <= 0.0f && _LeftTime >= 0.0f) {
			_OnTimeOver.Invoke();
		}
		_LeftTime = leftT;
	
	}
}
