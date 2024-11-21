using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
	public Transform target;

	public float distance = 5.0f;
	public float xSpeed = 8.0f;
	public float ySpeed = 12.0f;
	public float scrollSpeed = 5f;


	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = 2f;
	public float distanceMax = 60f;

	float x = 50.0f;
	float y = 150.0f;


	void Start()
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}

	void LateUpdate()
	{

		if (target)
		{
			if (Input.GetMouseButton(1))
			{
				if (EventSystem.current.IsPointerOverGameObject())
					return;

				x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			}

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);

			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, distanceMin, distanceMax);

			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			transform.rotation = rotation;
			transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	public void SetValues(OrbitCameraData data)
	{
		if (data.target != target && data.target != null) { target = data.target; }
		if (data.distance != distance) { distance = data.distance; }
		if (data.xSpeed != xSpeed) { xSpeed = data.xSpeed; }
		if (data.ySpeed != ySpeed) { ySpeed = data.ySpeed; }
		if (data.scrollSpeed != scrollSpeed) { scrollSpeed = data.scrollSpeed; }
		if (data.yMinLimit != yMinLimit) { yMinLimit = data.yMinLimit; }
		if (data.yMaxLimit != yMaxLimit) { yMaxLimit = data.yMaxLimit; }
		if (data.distanceMin != distanceMin) { distanceMin = data.distanceMin; }
		if (data.distanceMax != distanceMax) { distanceMax = data.distanceMax; }
	}

	[System.Serializable]
	public class OrbitCameraData
	{
		public Transform target;
		public float distance = 5.0f;
		public float xSpeed = 8.0f;
		public float ySpeed = 12.0f;
		public float scrollSpeed = 5f;

		public float yMinLimit = -20f;
		public float yMaxLimit = 80f;

		public float distanceMin = 2f;
		public float distanceMax = 60f;
	}
}
