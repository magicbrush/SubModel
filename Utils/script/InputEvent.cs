using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputEvent : MonoBehaviour {

	public UnityEvent _OnPress;
	public UnityEvent _OnRelease;

	private bool _PrevPressed = false;

	private bool _triggerOnPress = false;
	private bool _triggerOnRelease = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		GraphicRaycaster grc = GetComponent<GraphicRaycaster> ();

		if (_triggerOnPress) {
			_OnPress.Invoke ();
			_triggerOnPress = false;
		}
		if (_triggerOnRelease) {
			_OnRelease.Invoke ();
			_triggerOnRelease = false;
		}

		bool bMouse = Input.GetMouseButton (0);
		//Touch th = Input.GetTouch (0);
		bool bTouched = (Input.touchCount > 0);
		bool bPressed = bTouched || bMouse;
		if (bPressed && !_PrevPressed) {
			_triggerOnPress = true;
		}
		if (!bPressed && _PrevPressed) {
			_triggerOnRelease = true;
		}
		_PrevPressed = bPressed;
	}
}
