using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformThree : MonoBehaviour
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
        //platformMovement = GetComponent<PlatformMovement>();
        platformMovement = gameObject.AddComponent(typeof(PlatformMovement)) as PlatformMovement;

        platformMovement.GetChildren(3);
		platformMovement.GetPlatformInfo(3);

		platformMovement.ArrangeDistanceAtStart(cornerDistance);
	}

	private void FixedUpdate()
	{
		platformMovement.MoveChildren(platformSpeed, moveDistance);
	}
}
