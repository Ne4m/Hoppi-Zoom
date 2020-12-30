using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private GameObject Playerobject;

	private Transform t;
	private Vector2 centerofScreen;
	[SerializeField]
	private float distanceThreshold;
	[SerializeField]
	private Vector3 camOffset;
	public float cameraFollowSpeed;

	private bool isInDistance;

	private void Awake()
	{
		t = Playerobject.GetComponent<Transform>();
	}

	private void FixedUpdate()
	{
		CameraFollow();
	}

	private void CameraFollow()
	{
		//Get Center of Screen
		centerofScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
		Vector3 desiredPos = t.position + camOffset;

		//Get the distance between player and the center
		float distance;
		distance = Vector2.Distance(desiredPos, centerofScreen);

		if (distance > distanceThreshold)
		{
			isInDistance = false;
		}else if (distance < 0.5f)
		{
			isInDistance = true;
		}

		if (!isInDistance)
		this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(this.transform.position.y, desiredPos.y, Time.fixedDeltaTime * cameraFollowSpeed), this.transform.position.z);
		//Debug.Log("IS IN THE DISTANCE " + isInDistance);
	}

}
