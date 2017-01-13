using UnityEngine;
using System.Collections;

public class LifeLimit : MonoBehaviour {

	public float LeftTime = 10.0f;
	
	// Update is called once per frame
	void Update () {
		LeftTime -= Time.deltaTime;
		if (LeftTime <= 0.0f)
			Destroy (gameObject);
	}
}
