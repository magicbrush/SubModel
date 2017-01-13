using UnityEngine;
using System.Collections;

public class SpriteColorInterpolator : MonoBehaviour {

	public SpriteRenderer _sr;
	public Vector3[] _SamplePos;
	public Color[] _SampleColor;
	public Vector3 _InterpolationPos;
	private float[] _Weights;
	private float[] _DistsInv;
	private Color _iColor = new Color();

	// Use this for initialization
	void Start () {
		_Weights = new float[_SamplePos.Length];
		_DistsInv = new float[_SamplePos.Length];

		if (_sr == null) {
			_sr = GetComponent<SpriteRenderer> ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (_sr == null) 
			return;

		float _distInvSum = 0f;
		int idOne = -1;
		for (int i = 0; i < _DistsInv.Length; i++) {
			float d = (_InterpolationPos - _SamplePos [i]).magnitude;
			if (Mathf.Approximately (d,0f)) {
				idOne = i;
				break;
			}
			_DistsInv [i] = 1f/d;
			_distInvSum += d;
		}

		if (idOne >= 0) {
			for (int i = 0; i < _DistsInv.Length; i++) {
				_Weights [i] = 0f;
			}
			_Weights [idOne] = 1f;
		} else {
			for (int i = 0; i < _DistsInv.Length; i++) {
				_Weights [i] = _DistsInv [i] / _distInvSum;
			}
		}

		// interpolation:

		Vector4 Crv = new Vector4();
		for (int j = 0; j < _SampleColor.Length; j++) {
			Vector4 Crj = Color2Vec4(_SampleColor[j]);
			for (int i = 0; i < 4; i++) {
				Crv [i] += _Weights [j] * Crj [i];
			}
		}
		_sr.color = Vec4ToColor (Crv);
		//Debug.Log ("color:" + _sr.color);

	}
	private Vector4 Color2Vec4(Color c)
	{
		Vector4 v = new Vector4 ();
		v.x = c.r;
		v.y = c.g;
		v.z = c.b;
		v.w = c.a;
		return v;
	}

	private Color Vec4ToColor(Vector4 v)
	{
		Color c;
		c.r = v.x;
		c.g = v.y;
		c.b = v.z;
		c.a = v.w;
		return c;
	}
}
