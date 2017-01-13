using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TimeTrigger : MonoBehaviour {

	public UnityEvent _OnOnce;

	public float _LeftTime = 1.0f;

	public bool _disableOnInvoke = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_LeftTime -= Time.deltaTime;
		if (_LeftTime > 0f) {
			return;
		}
	
		_OnOnce.Invoke ();

		if (_disableOnInvoke) {
			enabled = false;
		}
	}
}
