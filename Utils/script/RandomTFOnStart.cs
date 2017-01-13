using UnityEngine;
using System.Collections;

public class RandomTFOnStart : MonoBehaviour {

	public bool _local = true;
	public bool _randPosition = true;
	public bool _randRotation = true;
	public bool _randScale = true;

	public Bounds _PositionBounds;

	public Bounds _RotBounds;

	public Bounds _ScaleBounds;

	public bool _ForceScaleXYEqual = true;

	// Use this for initialization
	void Start () {
		Vector3 
			p = Vector3.zero, 
			r = Vector3.zero, 
			s = Vector3.one;
		for (int i = 0; i < 3; i++) {
			if (_randPosition) {
				p [i] = Random.Range (
					_PositionBounds.center [i] - _PositionBounds.extents [i],
					_PositionBounds.center [i] + _PositionBounds.extents [i]);
			}
			if (_randRotation) {
				r [i] = Random.Range (
					_RotBounds.center [i] - _RotBounds.extents [i],
					_RotBounds.center [i] + _RotBounds.extents [i]);
			}
			if (_randScale) {
				s [i] = Random.Range (
					_ScaleBounds.center [i] - _ScaleBounds.extents [i],
					_ScaleBounds.center [i] + _ScaleBounds.extents [i]);
			}
		}

		if (_ForceScaleXYEqual) {
			float scl = (s.x + s.y) / 2f;
			s.x = scl;
			s.y = scl;
		}

		if (_local) {
			if (_randPosition) {
				transform.localPosition = p;
			}
			if (_randRotation) {
				transform.localRotation = Quaternion.Euler (r);
			}
			if (_randScale) {
				transform.localScale = s;
			}
		} else {
			if (_randPosition) {
				transform.position = p;
			}
			if (_randRotation) {
				transform.rotation = Quaternion.Euler (r);
			}
			if (_randScale) {
				Vector3 ls = transform.lossyScale;
				Vector3 s2 = s;
				for (int i = 0; i < 3; i++) {
					s2 [i] = s [i] / ls [i];
				}
				transform.localScale = s2;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
