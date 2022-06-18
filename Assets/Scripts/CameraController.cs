using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private GameObject Playerobject;

	private Transform t;
	private Vector2 _centerofScreen;
	[SerializeField]
	private float distanceThreshold;
	[SerializeField]
	private Vector3 camOffset;
	public float cameraFollowSpeed;

	private bool isInDistance;

    public Transform background1, background2;
    private float bg_size;
	private Vector2 bg_size2;

	public Transform camTransform;


	[SerializeField]
	private Transform target;


	private Vector3 background1TargetPos = new Vector3();
	private Vector3 background2TargetPos = new Vector3();
	//private Vector3 cameraTargetPos = new Vector3();


	private void Awake()
	{
		t = Playerobject.GetComponent<Transform>();
		camTransform = GetComponent<Transform>();

	}

    private void Start()
    {
        bg_size = background1.GetComponent<BoxCollider2D>().size.y;
		//bg_size2.x = background2.GetComponent<BoxCollider2D>().size.x;
		//bg_size2.y = background2.GetComponent<BoxCollider2D>().size.y;

	}

    private void FixedUpdate()
	{
		CameraFollow();
		backGroundFollow();

    }

    //private void Update()
    //{

    //    camTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    //}


	private void CameraFollow2()
    {
		Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z);

		transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
    }

    private void CameraFollow()
	{
		//Get Center of Screen
		_centerofScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
		//_centerofScreen = Camera.main.ScreenToWorldPoint(new Vector2(bg_size2.x / 2, bg_size2.y / 2));
		Vector2 desiredPos = t.position + camOffset;

		//Get the distance between player and the center
		float distance;
		distance = Vector2.Distance(desiredPos, _centerofScreen);

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

    private void backGroundFollow()
    {
        if (transform.position.y >= background2.position.y)
        {
            background1.GetComponent<SpriteRenderer>().sprite = background2.GetComponent<SpriteRenderer>().sprite;
			//background1.position = new Vector3(background1.position.x, background2.position.y + bg_size, background1.position.z); //  - .37169f
			background1.position = SetPosition(background1TargetPos,background1.position.x, background2.position.y + bg_size, background1.position.z);
			switchBackGround();
        }

		if(transform.position.y < background1.position.y)
        {
			background2.position = SetPosition(background2TargetPos, background2.position.x, background1.position.y - bg_size, background2.position.z);
			switchBackGround();
		}
    }

    private void switchBackGround()
    {
        Transform temp = background1;
        background1 = background2;
        background2 = temp;
    }


	private Vector3 SetPosition(Vector3 pos, float x, float y, float z)
    {
		pos.x = x;
		pos.y = y;
		pos.z = z;
		return pos;
    }
}
