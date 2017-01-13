using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Instantiator_RandomChoose : MonoBehaviour {
	public List<GameObject> _prefab;
	public List<float> _weights;

	private List<float> _probThres = new List<float>();
	public Transform _ParentTransform;

	public bool _SetActiveOnBorn = true;
	public bool _BornActive = true;

	private int _InstantiateCount = 0;

	public int _TriggerCount = 10;
	public UnityEvent _CountOver;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < _weights.Count; i++) {
			_probThres.Add (Mathf.Clamp(_weights [i],0f,float.PositiveInfinity));
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Instantiate()
	{
		float max = _probThres [_probThres.Count - 1];
		float rvalue = Random.Range (0f, max);
		int id = 0;
		for (int i = 1; i < _probThres.Count; i++) {
			if (rvalue > _probThres [i]) {
				id = i-1;
				break;
			}
		}

		GameObject gb = GameObject.Instantiate (_prefab [id]);
		Transform TF = (_ParentTransform != null) ? _ParentTransform : transform;
		gb.transform.SetParent (TF);

		if (_SetActiveOnBorn) {
			gb.SetActive (_BornActive);
		} 
		gb.name = gb.name + Time.realtimeSinceStartup;
		_InstantiateCount++;

		if (_InstantiateCount >= _TriggerCount) {
			_CountOver.Invoke ();
		}

		//Debug.Log ("Instantiate:" + gb);
	}

	public int GetInstantiateCount()
	{
		return _InstantiateCount;
	}
}
