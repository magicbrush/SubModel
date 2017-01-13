using UnityEngine;
using System.Collections;



public class FanMesh : MonoBehaviour {

	private MeshFilter meshFilter;
	public Material fanMaterial;

	public float RadiusMin=0f;
	public float RadiusMax=1f;
	public float StartRadians=0f;
	public float EndRadians=2f*Mathf.PI;
	public int RadiusResolution=1;
	public int CircleResolution=36;
	public Vector2 UVScale = new Vector2 (1f, 1f);
	public Color baseColor;

	public Gradient RadiusGrad;
	public Gradient CircleGrad;

	public Vector3[] newVertices;
	public Vector2[] newUV;
	public int[] newTriangles;
	public Color[] newColors;

	public bool updatingMesh = false;

	void Start () {
		meshFilter = GetComponent<MeshFilter> ();
		if (meshFilter == null) {
			meshFilter = gameObject.AddComponent<MeshFilter> ();
		}
		meshFilter.mesh = new Mesh();

		MeshRenderer MR = GetComponent<MeshRenderer> ();
		if (MR == null) {
			MR = gameObject.AddComponent<MeshRenderer> ();
			MR.material = fanMaterial;
		}
		UpdateMesh ();
	}

	void Update () {
		if (updatingMesh) {
			UpdateMesh ();
		}
	}
		
	public void UpdateMesh ()
	{
		if (meshFilter == null)
			return;
		meshFilter.mesh.Clear ();
		float FanAngle = EndRadians - StartRadians;
		int FanResolution = Mathf.CeilToInt (CircleResolution * Mathf.Abs(FanAngle) / (2f * Mathf.PI));
		int vertNum = FanResolution * RadiusResolution;
		newVertices = new Vector3[vertNum];
		newUV = new Vector2[vertNum];
		newColors = new Color[vertNum];
		int fanRes = Mathf.FloorToInt (CircleResolution * 0.5f * Mathf.Abs (FanAngle) / Mathf.PI);
		newTriangles = new int[3 * RadiusResolution * fanRes];
		float AngleDelta = FanAngle / (FanResolution-1);
		float RadiusDelta = (RadiusMax - RadiusMin) / (RadiusResolution-1);
		for (short i = 0; i < FanResolution; i++) {
			for (short j = 0; j < RadiusResolution; j++) {
				float theta = StartRadians + i * AngleDelta;
				float radius = RadiusMin + j * RadiusDelta;
				float x = radius * Mathf.Cos (theta);
				float y = radius * Mathf.Sin (theta);
				float u = UVScale.x * theta / (2f * Mathf.PI);
				float v = UVScale.x * j / RadiusResolution;
				int vertId = i * RadiusResolution + j;
				newVertices [vertId] = new Vector3 (x, y, 0f);
				newUV [vertId] = new Vector2 (u, v);
				Color CBase = baseColor;
				Color C0 = RadiusGrad.Evaluate (
					(float)j / ((float)RadiusResolution-1f));
				Color C1 = CircleGrad.Evaluate (
					(float)i / ((float)FanResolution-1f));
				Color C2 = C0 * C1;
				newColors [vertId] = C2*CBase;
			}
		}
		int cornerId = 0;
		for (short i = 0; i < FanResolution - 1; i++) {
			for (short j = 0; j < RadiusResolution - 1; j++) {
				int vertId = i * RadiusResolution + j;
				newTriangles [cornerId] = vertId;
				newTriangles [cornerId + 1] = vertId + RadiusResolution + 1;
				newTriangles [cornerId + 2] = vertId + RadiusResolution;
				newTriangles [cornerId + 3] = vertId;
				newTriangles [cornerId + 4] = vertId + 1;
				newTriangles [cornerId + 5] = vertId + RadiusResolution + 1;
				cornerId += 6;
			}
		}
		meshFilter.mesh.vertices = newVertices;
		meshFilter.mesh.uv = newUV;
		meshFilter.mesh.SetTriangles (newTriangles, 0);		
		meshFilter.mesh.colors = newColors;
	}

    public void setDirectionAngle(float dirAngleRadians)
    {
        float fanHalf = 0.5f*(StartRadians - EndRadians);
        StartRadians = dirAngleRadians - fanHalf;
        EndRadians = dirAngleRadians + fanHalf;
    }
    public void setRadiusMinMax(float rmin,float rmax)
    {          
        RadiusMin = rmin;
        RadiusMax = rmax;
        RadiusMin = Mathf.Clamp(
            RadiusMin, 0.0f, float.PositiveInfinity);
        RadiusMax = Mathf.Clamp(
            RadiusMax, RadiusMin, float.PositiveInfinity);
    }

    public void SetBaseColor(Color cr)
    {
        baseColor = cr;
    }

	public float GetSpanAngleRadians()
	{
		return Mathf.Abs(EndRadians - StartRadians);
	}

	public float GetSpanAngleDegs()
	{
		return Mathf.Rad2Deg * GetSpanAngleRadians();
	}

	public void DisableDisplay()
	{
		MeshRenderer MR = GetComponent<MeshRenderer> ();
		MR.enabled = false;
	}
	public void EnableDisplay()
	{
		MeshRenderer MR = GetComponent<MeshRenderer> ();
		MR.enabled = true;
	}

}
