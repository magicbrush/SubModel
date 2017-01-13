using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
public class EnableDependance : MonoBehaviour {
	
	public enum _StateType
	{
		// All
		ON,
		OFF,
		TurnON,
		TurnOFF,
		// Components
		Enabled,
		Disabled,
		TurnEnable,
		TurnDisable,
		// Gameobjects:
		Active,
		Inactive,
		Activated,
		Disactivated,
		// Objects:
		Alive,
		Dead,
		Born,
		Die
	}
	public enum RelateionType
	{
		ALL,
		ANY
	}
	public List<Object> _IfThese;
	public RelateionType _Are = RelateionType.ALL;
	public _StateType _InState;

	public List<Object> _ThenThose;
	public _StateType _AreTurnTo;

	private struct StateDictionary
	{
		public Dictionary<Object,bool> _On;
		public Dictionary<GameObject,bool> _Activity;
		public Dictionary<Behaviour,bool> _Enable;
		public Dictionary<int,bool> _Life;
		public StateDictionary()
		{
			_On = new Dictionary<Object,bool>();
			_Activity = new Dictionary<GameObject,bool>();
			_Enable = new Dictionary<Behaviour,bool>();
			_Life = new Dictionary<int,bool>();
		}
	}
	StateDictionary _CurState = new StateDictionary();
	StateDictionary _PrevState = new StateDictionary();

	private enum _CheckType
	{
		OBJECT,
		GAMEOBJECT,
		BEHAVIOUR,
		LIFE
	}
	private enum _StateCheckType
	{
		KEEP,
		TURN
	}
	private struct ThreeFlag
	{
		public bool flagAll;
		public bool flagNone;
		public bool flagAny;
		public ThreeFlag(
			bool fall = true,
			bool fnone = true, 
			bool fany = true)
		{
			flagAll = fall;
			flagNone = fnone;
			flagAny = fany;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_CheckType ct = GetCheckType ();
		_StateCheckType sct = GetStateCheckType ();

		Update_CurState ();

		
		if (sct == _StateCheckType.KEEP) {
			ThreeFlag F;
			if (ct == _CheckType.BEHAVIOUR) {
				F.flagAll = CheckBehaviourAll (F.flagAll);
				F.flagNone = CheckBehaviourNone (F.flagNone);
			} else if (ct == _CheckType.GAMEOBJECT) {
				F.flagAll = CheckGameobjectAll (F.flagAll);
				F.flagNone = CheckGameObjectNone (F.flagNone);
			} else if (ct == _CheckType.LIFE) {
				for (int i = 0; i < _CurState._Life.Count; i++) {
					bool alive = _CurState._Life [i];
					F.flagAll = F.flagAll && alive;
					F.flagNone = F.flagNone && !alive;
				}
			} else if (ct == _CheckType.OBJECT) {
				F.flagAll = CheckBehaviourAll (F.flagAll);
				F.flagAll = CheckGameobjectAll (F.flagNone);
				F.flagNone = CheckBehaviourNone (F.flagAll);
				F.flagNone = CheckGameObjectNone (F.flagNone);
			}
			F.flagAny = (!F.flagAll || !F.flagNone);
			bool bTurn = 
				(_Are == RelateionType.ALL && F.flagAll) ||
				(_Are == RelateionType.ANY && F.flagAny);
			if (bTurn)
				TurnThose();
		} else {
			
		}


		_PrevState = _CurState;
	}

	bool CheckGameobjectAll (bool flagAll)
	{
		foreach (KeyValuePair<GameObject,bool> item in _CurState._Activity) {
			flagAll = flagAll && item.Value;
		}
		return flagAll;
	}

	bool CheckGameObjectNone (bool flagNone)
	{
		foreach (KeyValuePair<GameObject,bool> item in _CurState._Activity) {
			flagNone = flagNone && !item.Value;
		}
		return flagNone;
	}

	bool CheckBehaviourAll (bool flagAll)
	{
		foreach (KeyValuePair<Behaviour,bool> item in _CurState._Enable) {
			flagAll = flagAll && item.Value;
		}
		return flagAll;
	}

	bool CheckBehaviourNone (bool flagNone)
	{
		foreach (KeyValuePair<Behaviour,bool> item in _CurState._Enable) {
			flagNone = flagNone && !item.Value;
		}
		return flagNone;
	}

	void Update_CurState ()
	{
		for (int i = 0; i < _CurState._Life.Count; i++) {
			_CurState._Life [i] = false;
		}
		foreach (Object obj in _IfThese) {
			if (obj != null) {
				_CurState._Life [obj.GetInstanceID ()] = true;
			}
			else {
				continue;
			}
			if (obj.GetType () == typeof(GameObject)) {
				bool activity = ((GameObject)obj).activeSelf;
				_CurState._Activity [(GameObject)obj] = activity;
			}
			if (obj.GetType () == typeof(Behaviour)) {
				bool enabled = ((Behaviour)obj).enabled;
				_CurState._Enable [(Behaviour)obj] = enabled;
			}
		}
	}

	_CheckType GetCheckType ()
	{
		_CheckType ct;
		if (_InState == _StateType.ON || _InState == _StateType.OFF || _InState == _StateType.TurnON || _InState == _StateType.TurnOFF) {
			ct = _CheckType.OBJECT;
		}
		else
			if (_InState == _StateType.Enabled || _InState == _StateType.Disabled || _InState == _StateType.TurnEnable || _InState == _StateType.TurnDisable) {
				ct = _CheckType.BEHAVIOUR;
			}
			else
				if (_InState == _StateType.Active || _InState == _StateType.Inactive || _InState == _StateType.Activated || _InState == _StateType.Disactivated) {
					ct = _CheckType.GAMEOBJECT;
				}
				else {
					ct = _CheckType.LIFE;
				}
		return ct;
	}

	_StateCheckType GetStateCheckType ()
	{
		_StateCheckType sct;
		if (_InState == _StateType.TurnON || _InState == _StateType.TurnEnable || _InState == _StateType.Activated || _InState == _StateType.Born) {
			sct = _StateCheckType.TURN;
		}
		else {
			sct = _StateCheckType.KEEP;
		}
		return sct;
	}

	private void TurnThose()
	{
		foreach (Object obj in _ThenThose) {
			
		}
	}

}
*/