using UnityEngine;
using System.Collections;

public class KeepPosRelativeTo : MonoBehaviour {

	public Transform _Anchor;
	public Vector3 _Offset = Vector3.zero;
	public Vector3 _Scale = Vector3.one;
	public Vector3 _RotationEuler = Vector3.zero;

	// Use this for initialization
	void Start () {
		if (_Anchor == null) {
			_Anchor = transform.parent;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_Anchor == null) {
			transform.localPosition = _Offset;
		} else {
			transform.position = _Anchor.position + _Offset;
		}
		transform.localScale = _Scale;
		transform.localRotation = Quaternion.Euler(_RotationEuler);
	
	}
}
