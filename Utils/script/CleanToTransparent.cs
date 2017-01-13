using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class CleanToTransparent : MonoBehaviour {

	private Material mat;

	public bool _cleanAlpha = false;
	public float _alpha = 1.0f;
	public Camera _cam;
	public Texture2D tx;
	public Color _clearColor = new Color(1f,1f,1f,0f);
	// Use this for initialization
	void Start () {
		mat = new Material(
			"Shader \"Hidden/Clear Alpha\" {" +
			"Properties { _Alpha(\"Alpha\", Float)=1.0 } " +
			"SubShader {" +
			"    Pass {" +
			"        ZTest Always Cull Off ZWrite Off" +
			"        ColorMask A" +
			"        SetTexture [_Dummy] {" +
			"            constantColor(0,0,0,[_Alpha]) combine constant }" +
			"    }" +
			"}" +
			"}"
		);

		tx = new Texture2D (1024, 1024);
		//tx.format = TextureFormat.ARGB32;
		tx.filterMode = FilterMode.Bilinear;
		tx.wrapMode = TextureWrapMode.Clamp;

		for (int i = 0; i < tx.width; i++) {
			for (int j = 0; j < tx.height; j++) {
				tx.SetPixel (i, j, _clearColor);
			}
		}
		tx.Apply ();

	}
	
	// Update is called once per frame
	void Update () {

		if (_cleanAlpha && _cam!=null) {

			_cam.targetTexture.DiscardContents ();
			_cleanAlpha = false;

		}
	}
}
