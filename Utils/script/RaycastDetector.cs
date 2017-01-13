using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class RaycastDetector : MonoBehaviour {

    protected SphereCollider cldThis;
    
	// Use this for initialization
	void Start () {
        cldThis = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public virtual GameObject Dectect(Ray r)
    {
        RaycastHit rc;
        bool bOK = cldThis.Raycast(r, out rc, 100000f);
        return transform.parent.gameObject;
    }
}
