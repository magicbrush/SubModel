using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class rigidbody2DRestriction : MonoBehaviour {

    public float _maxSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 vel = rb.velocity;
        if(vel.magnitude>=_maxSpeed)
        {
            rb.velocity = 
                rb.velocity.normalized * _maxSpeed;
        }
	
	}
}
