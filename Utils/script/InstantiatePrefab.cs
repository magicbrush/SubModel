using UnityEngine;
using System.Collections;

public class InstantiatePrefab : MonoBehaviour {

	public GameObject _Prefab;
	public Transform _Parent;
	public bool _SetActivity = true;
	public bool _Activity = true;
	public bool _InitPosInLocal = true;
	public Vector3 _InitPos = new Vector3(0, 0f,0f);

	public void Instantiate()
	{
		if(_Prefab!=null)
		{
			GameObject newObj = GameObject.Instantiate(_Prefab);
			Transform parent = (_Parent == null) ? transform : _Parent;
			newObj.transform.SetParent (parent);
			//Debug.Log ("Instantiated!");

			if (!float.IsNaN (_InitPos.x)) {
				if (_InitPosInLocal) {
					newObj.transform.position = transform.position + _InitPos;
				} else {
					newObj.transform.position = _InitPos;
				}
			}

			if (_SetActivity) {
				newObj.SetActive (_Activity);
			}
		}
	}
}
