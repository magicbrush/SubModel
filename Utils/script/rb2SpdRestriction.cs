using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
internal class rb2SpdRestriction : MonoBehaviour {

	public float _spdMax = 1f;
	public float _aSpdMax = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		float spd = rb.velocity.magnitude;


		if (spd > _spdMax) {
			rb.velocity = rb.velocity * spd / _spdMax;
		}

		float aSpd = rb.angularVelocity;
		if (Mathf.Abs(aSpd) > _aSpdMax) {
			rb.angularVelocity = Mathf.Clamp (rb.angularDrag, -_aSpdMax, _aSpdMax);
		}
	
	}
}
