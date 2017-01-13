using UnityEngine;
using System.Collections;

public class drawRigidbody2D : MonoBehaviour {

	const float k_Frequency = 50.0f;
	const float k_Damper = 1.0f;
	const float k_Drag = 10.0f;
	const float k_AngularDrag = 5.0f;
	const float k_Distance = 0.2f;
	const bool k_AttachToCenterOfMass = false;

	private SpringJoint2D m_SpringJoint;

	private void Update()
	{
		// Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}

		var mainCamera = FindCamera();

		Ray MouseRay = mainCamera.ScreenPointToRay (Input.mousePosition);
		float zdepth = 0.0f;
		float k = (zdepth - MouseRay.origin.z) / MouseRay.direction.z;
		Vector2 PosScreen = MouseRay.origin + k * MouseRay.direction;
		// We need to actually hit an object
		
		//RaycastHit2D hit = new RaycastHit2D();
		RaycastHit2D hit = Physics2D.Raycast(PosScreen, Vector2.left,0.5f);
		
		if (hit.collider == null)
		{
			return;
		}
		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
		{
            Debug.Log("no rigidbody!");
			return;
		}

		if (!m_SpringJoint)
		{
			var go = new GameObject("Rigidbody dragger");
			Rigidbody2D body = go.AddComponent<Rigidbody2D>();
			m_SpringJoint = go.AddComponent<SpringJoint2D>();
			body.isKinematic = true;
		}

		m_SpringJoint.transform.position = hit.point;
		m_SpringJoint.anchor = Vector3.zero;

		m_SpringJoint.frequency = k_Frequency;
		m_SpringJoint.dampingRatio = k_Damper;
		m_SpringJoint.distance = k_Distance;
		m_SpringJoint.connectedBody = hit.rigidbody;
		m_SpringJoint.autoConfigureDistance = false;


		StartCoroutine("DragObject", k);
	}


	private IEnumerator DragObject(float distance)
	{
		var oldDrag = m_SpringJoint.connectedBody.drag;
		var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
		m_SpringJoint.connectedBody.drag = k_Drag;
		m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;
		var mainCamera = FindCamera();
		while (Input.GetMouseButton(0))
		{
			var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			m_SpringJoint.transform.position = ray.GetPoint(distance);
			yield return null;
		}
		if (m_SpringJoint.connectedBody)
		{
			m_SpringJoint.connectedBody.drag = oldDrag;
			m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
			m_SpringJoint.connectedBody = null;
		}
	}

	private Camera FindCamera()
	{
		if (GetComponent<Camera>())
		{
			return GetComponent<Camera>();
		}

		return Camera.main;
	}
}
