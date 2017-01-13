using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ObjectsStateTrigger : MonoBehaviour {

	public enum CheckType
	{
		GAMEOBJECT,
		BEHAVIOUR,
		OBJECT_LIFE
	}
	public enum CheckRelation
	{
		ALL,
		ANY,
		NONE
	}
	private struct ThreeFlag
	{
		public bool FlagAll;
		public bool FlagNone;
		public bool FlagAny;
		public ThreeFlag(bool bAll, bool bNone, bool bAny)
		{
			FlagAll = bAll;
			FlagNone = bNone;
			FlagAny = bAny;
		}
	}

	public CheckType _CheckIfFollowing = CheckType.GAMEOBJECT;
	public List<GameObject> _GameObjects;
	public List<Behaviour> _Behaviours;
	public CheckRelation _Are = CheckRelation.ALL;
	private List<Object> _objects = new List<Object>();

	private bool _ON = false;
	private bool _PrevON = false;
	private bool _bReady = false;

	public int _LeftTriggerCount = 1;
	public bool _DisableWhenOver = true;

	public UnityEvent _ON_Callback;
	public UnityEvent _OFF_Callback;
	public UnityEvent _TurnOn_Callback;
	public UnityEvent _TurnOff_Callback;

	public float _CheckPeriod = 0.1f;
	private float _TimeToCheck;


	// Use this for initialization
	void Start () {
		_TimeToCheck = _CheckPeriod;

		foreach (GameObject gb in _GameObjects) {
			_objects.Add (gb);
		}
		foreach (Behaviour bh in _Behaviours) {
			_objects.Add (bh);
		}
	}
	
	// Update is called once per frame
	void Update () {
		_TimeToCheck -= Time.deltaTime;
		if (_TimeToCheck >= 0f) {
			return;
		}
		_TimeToCheck += _CheckPeriod;

		ThreeFlag F = new ThreeFlag(true,true,true);
		if (_CheckIfFollowing == CheckType.BEHAVIOUR) {
			foreach (Behaviour bh in _Behaviours) {
				F.FlagAll = F.FlagAll && bh.enabled;
				F.FlagNone = F.FlagNone && !bh.enabled;
			}
		} else if (_CheckIfFollowing == CheckType.GAMEOBJECT) {
			foreach (GameObject g in _GameObjects) {
				F.FlagAll = F.FlagAll && g.activeSelf;
				F.FlagNone = F.FlagNone && !g.activeSelf;
			}
		} else if (_CheckIfFollowing == CheckType.OBJECT_LIFE) {
			foreach (Object obj in _objects) {
				F.FlagAll = F.FlagAll && (obj!=null);
				F.FlagNone = F.FlagNone && (obj==null);
			}
		}
		F.FlagAny = (!F.FlagAll && !F.FlagNone);

		if (_Are == CheckRelation.ALL) {
			_ON = F.FlagAll;
		} else if (_Are == CheckRelation.ANY) {
			_ON = F.FlagAny;
		} else if (_Are == CheckRelation.NONE) {
			_ON = F.FlagNone;
		}

		//Debug.Log ("ON?" + _ON);

		if (_bReady) {
			//Debug.Log ("Ready!");
			if (_ON && _PrevON) {
				_ON_Callback.Invoke ();
			}
			if(!_ON && !_PrevON) {
				_OFF_Callback.Invoke ();
			}
			if (_ON && !_PrevON) {
				_TurnOn_Callback.Invoke ();
				//Debug.Log ("_TurnOn_Callback.Invoke ()");
			}
			if (!_ON && _PrevON) {
				_TurnOff_Callback.Invoke ();
			}
		}

		_PrevON = _ON;
		_bReady = true;
	}
}
