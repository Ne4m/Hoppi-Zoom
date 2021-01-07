﻿using UnityEngine;

namespace Platforms
{
	[RequireComponent(typeof(PlatformMovement))]
	public class MovePlatform : MonoBehaviour
	{
		private PlatformMovement _platformMovement;
		[Header("Platform Variables")]
		[SerializeField]
		private float moveDistance;
		[SerializeField]
		private float cornerDistance;
		[SerializeField]
		private float platformSpeed;

		[Header("Platform #4")] [SerializeField]
		private float transitionTime;

		[SerializeField] private float stayTime;
		private void Start()
		{
			_platformMovement = gameObject.GetComponent<PlatformMovement>();
			if (_platformMovement is null) return;
			_platformMovement.GetPlatformInfo();
			_platformMovement.GetChildren(stayTime);
			_platformMovement.ArrangeDistanceAtStart(cornerDistance);
		}

		private void FixedUpdate()
		{
			_platformMovement.MoveChildren(platformSpeed, moveDistance,transitionTime);
		}

	}
}
