using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Terminator : MonoBehaviour {

    private static Terminator tm;

    public GameObject _visualEffect;    
    public Dictionary<GameObject, GameObject> _killList = 
        new Dictionary<GameObject, GameObject>();

	// Use this for initialization
	void Start () {
        if (tm != null) Destroy(this);
        tm = this;	
	}
	
	// Update is called once per frame
	void Update () {  
        foreach(KeyValuePair<GameObject,GameObject> gg in _killList)
        { 
            if(gg.Value == null)
            {
                Destroy(gg.Key);
                _killList.Remove(gg.Key);
                break;
            }            
        }                
    }

    public static Terminator TerminatorInstance()
    {
        if (tm == null) tm = new Terminator();        
        return tm;
    }
    public static void terminate(List<GameObject> tgts)
    {
        foreach(GameObject gb in tgts)
        {
            terminate(gb);
        }
    }

    public static void terminate(GameObject g)
    {
        if (g == null) return;

        Terminator T = TerminatorInstance();
        if (T._killList.ContainsKey(g)) return;

        Transform TF = g.transform;
        GameObject VFX =
            Instantiate(T._visualEffect, TF.position, Quaternion.identity)
            as GameObject;
        VFX.transform.parent = g.transform;
        TerminatorInstance()._killList.Add(g, VFX);
    }
}
