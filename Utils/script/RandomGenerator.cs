using UnityEngine;
using System.Collections;

public class RandomGenerator : MonoBehaviour {

	public GameObject Prototype;
	public GameObject Parent;
	public Rect ZoneRect;
	public float PoissionLamda = 1.0f;
	public int MaxNum = 200;

	private ArrayList Objects = new ArrayList();

	public float checkNullPeriod = 1.0f;
	private float lastCheckTime;

	public Vector3 initialVel = new Vector3 (0.0f, 0f, 0f);

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
		Vector3 pos = new Vector3 (
			Random.Range (ZoneRect.xMin, ZoneRect.xMax), Random.Range (ZoneRect.yMin, ZoneRect.yMax), 0);
		GameObject Obj = Instantiate (Prototype, pos, Quaternion.identity) as GameObject;
		Obj.transform.SetParent (Parent.transform);
		Obj.name += Time.time;
		Objects.Add (Obj);

		Rigidbody2D rb = Obj.GetComponent<Rigidbody2D> ();
		rb.velocity = initialVel;
	}
}
