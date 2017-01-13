using UnityEngine;
using System.Collections;

public class ColorPrimitiveTrail : MonoBehaviour {

	private TrailRenderer TR;

	private ColorPrimitive CP;

	public Material PresetMaterial;

	public float time = 10.0f;

	public float widthCoef = 1.0f;

	// Use this for initialization
	void Start () {
		CP = GetComponentInParent<ColorPrimitive> ();

		TR = GetComponent<TrailRenderer> ();
		TR.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		TR.receiveShadows = false;
		TR.material = PresetMaterial;
		TR.useLightProbes = false;
		TR.reflectionProbeUsage = 0;
		TR.time = time;

	}
	
	// Update is called once per frame
	void Update () {

		if (CP == null)
			return;
		Color Cr = CP.GetDisplayColor ();

		TR.startWidth = widthCoef * Mathf.Sqrt (CP.GetSum ()) * 0.01f;
		TR.endWidth = 0.0f;
		TR.material.SetColor ("_Color", Cr);

	
	}
}
