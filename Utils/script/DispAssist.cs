using UnityEngine;
using System.Collections;

public class DispAssist : MonoBehaviour {
	public int lineCount = 100;
	public float radius= 1.0f;
	public Material lineMaterial;

	public float _z = -20.0f;

	void Start()
	{
		CreateLineMaterial();
	}

	void Update()
	{
		
	}

	public void OnWillRenderObject()
	{
		//DrawGLLines ();
	}
	public void OnPreCull()
	{
		
	}

	public void OnBecameVisible()
	{
		
	}

	public void OnPreRender()
	{
		
	}

	// Will be called after all regular rendering is done
	public void OnRenderObject() {
		DrawGLLines ();

	}

	public void OnPostRender()
	{
		
	}

	public void OnRenderImage()
	{
		
	}

	void CreateLineMaterial() {
		if (!lineMaterial) {
			// simple colored things.
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			lineMaterial = new Material(shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			lineMaterial.SetInt("_ZWrite", 0);
		}
	}

	void DrawGLLines ()
	{
		// Apply the line material
		lineMaterial.SetPass (0);
		GL.PushMatrix ();
		// match our transform
		GL.MultMatrix (transform.localToWorldMatrix);
		// Draw lines
		GL.Begin (GL.LINES);
		for (int i = 0; i < lineCount; ++i) {
			float a = i / (float)lineCount;
			float angle = a * Mathf.PI * 2;
			// Vertex colors change from red to green
			GL.Color (new Color (a, 1 - a, 0, 0.8F));
			// One vertex at transform position
			GL.Vertex3 (0, 0, _z);
			// Another vertex at edge of circle
			GL.Vertex3 (Mathf.Cos (angle) * radius, Mathf.Sin (angle) * radius, _z);
		}
		GL.End ();
		GL.PopMatrix ();
	}
}
