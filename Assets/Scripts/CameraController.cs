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

	private void Awake()
	{
		t = Playerobject.GetComponent<Transform>();
	}

    private void Start()
    {
        bg_size = background1.GetComponent<BoxCollider2D>().size.y;
    }

    private void FixedUpdate()
	{
		CameraFollow();
        backGroundFollow();

    }

	private void CameraFollow()
	{
		//Get Center of Screen
		_centerofScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
		Vector3 desiredPos = t.position + camOffset;

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
            background1.position = new Vector3(background1.position.x, background2.position.y + bg_size - .37169f, background1.position.z);
            switchBackGround();
        }
    }

    private void switchBackGround()
    {
        Transform temp = background1;
        background1 = background2;
        background2 = temp;
    }


}
