using UnityEngine;
using System.Collections;

public class limitLifetime : MonoBehaviour {

    public float LifeDuration = 10.0f; 

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {

        LifeDuration -= Time.deltaTime;

        if(LifeDuration<=0.0f)
        {
            Destroy(gameObject);
        }
	}
}
