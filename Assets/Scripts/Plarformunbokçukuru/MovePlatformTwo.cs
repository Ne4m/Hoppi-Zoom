using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformTwo : MonoBehaviour
{
	PlatformMovement platformMovement;
	[SerializeField]
	private float moveDistance;
	[SerializeField]
	private float cornerDistance;
	[SerializeField]
	private float platformSpeed;
	private void Start()
	{
		platformMovement = GetComponent<PlatformMovement>();

		platformMovement.GetChildren(2);
		platformMovement.GetPlatformInfo(2);

		platformMovement.ArrangeDistanceAtStart(cornerDistance);
	}

	private void FixedUpdate()
	{
		platformMovement.MoveChildren(platformSpeed, moveDistance);
	}
}
