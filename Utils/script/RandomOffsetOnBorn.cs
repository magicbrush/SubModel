using UnityEngine;
using System.Collections;

public class RandomOffsetOnBorn : MonoBehaviour {

	public Transform _Origin;

	public Vector2 DistRange = new Vector3(0.5f,1.5f);
	public Vector2 AngleDegRange = new Vector3(-180f,180f);

	// Use this for initialization
	void Start () {
		RandOffset ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RandOffset ()
	{
		float dist = Random.Range (DistRange.x, DistRange.y);
		float angRad = Mathf.Deg2Rad * Random.Range (AngleDegRange.x, AngleDegRange.y);
		Vector2 Offset = dist * new Vector2 (Mathf.Cos (angRad), Mathf.Sin (angRad));
		Vector3 posOrigin = _Origin.position;
		Vector3 posOffset = posOrigin + (Vector3)Offset;
		transform.position = posOffset;
	}
}
