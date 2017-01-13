using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeToEnableComp : MonoBehaviour {

	public List<Behaviour> _components;
	public float _LeftTime = 1.0f;
	public bool _AutoDisable = true;
	public bool _targetEnableState = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_LeftTime -= Time.deltaTime;
		if (_LeftTime <= 0f) {
			foreach(Behaviour bh in _components)
			{
				bh.enabled = _targetEnableState;
			}
			if (_AutoDisable) {
				enabled = false;
			}
		}
	
	}
}
