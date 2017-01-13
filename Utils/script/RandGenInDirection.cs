using UnityEngine;
using System.Collections;

public class RandGenInDirection : MonoBehaviour {

	public GameObject Reference;
	public GameObject Parent;
	public GameObject Prototype;
	public Vector2 Direction = new Vector2 (0f, 1f);
	public float PoissionLamda = 1.0f;
	public float MaxDist = 13.0f;
	public float MinDist = 3.0f;

	public int MaxNum = 15;
	private ArrayList Objects = new ArrayList();
	public float checkNullPeriod = 1.0f;
	private float lastCheckTime;

	// Use this for initialization
	void Start () {
		if (Parent == null) {
			Parent = this.gameObject;
		}
		lastCheckTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		float elapsedTime = Time.time - lastCheckTime;
		if (elapsedTime > checkNullPeriod) {
			for (int i = Objects.Count-1; i >0; i--)
			{
				GameObject obj = Objects [i] as GameObject;
				bool isNull = (obj == null);
				if (isNull) Objects.RemoveAt (i);
			}
		}

		if(Objects.Count >= MaxNum)
			return;

		// poission stochastic process
		float Prob = PoissionLamda * Time.deltaTime * 
			Mathf.Exp (-PoissionLamda * Time.deltaTime);
		if(Random.Range (0f, 1f)> Prob)
			return;

		GenerateObject ();
	
	}

	void GenerateObject ()
	{
		Rigidbody2D rb = 
			Reference.GetComponent<Rigidbody2D> ();
		Vector3 PRef = 
			(Reference == null) ? Vector3.zero:
			Reference.transform.position;
		Vector3 Dir = Direction;
		Vector3 pos = 
			PRef + 
			Dir * (Random.Range (MinDist, MaxDist) 
				+ rb.velocity.magnitude * 2.0f);

		GameObject Obj = Instantiate (
			Prototype, 
			pos, Quaternion.identity) as GameObject;
		
		Obj.transform.SetParent (Parent.transform);
		Obj.name += Time.time;
		Objects.Add (Obj);
	}
}
