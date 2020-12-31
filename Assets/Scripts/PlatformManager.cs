using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public struct PlatformInfo
	{
        public int difficulty;
        public int totalPlatformCount;
        /* 0-Easy
         * 1-Normal
         * 2-Hard
        */
	}

    #region Platform #1 Parents
    private List<Transform> easyPlatformOneParents = new List<Transform>();
    private List<Transform> normalPlatformOneParents = new List<Transform>();
    private List<Transform> hardPlatformOneParents = new List<Transform>();
    #endregion

    #region Difficulty
    [SerializeField]
    private float easyOneDistance;
    [SerializeField]
    private float normalOneDistance;
    [SerializeField]
    private float hardOneDistance;
    //------------------------------
    [SerializeField]
    private float easyOnePlatformSpeed = 5f;


    #endregion

    private Transform[] easyPlatformsOne;
    private Transform[] normalPlatformsOne;
    private Transform[] hardPlatformsOne;

    [HideInInspector]
    public float childrenRefreshTime =5f;

	private void Awake()
	{
        GetAllChildren();
        easyPlatformsOne = new Transform[2];
        normalPlatformsOne = new Transform[2];
        hardPlatformsOne = new Transform[2];
    }
	// Start is called before the first frame update
	void Start()
    {
        
        GetEasyOneChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
        //GetEasyOne();
        MoveEasyOne();
    }
    void GetAllChildren()
	{
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child;
            child = transform.GetChild(i);
            if (child.tag.Contains("Easy"))
            {
                if(child.tag.Contains("1"))
                easyPlatformOneParents.Add(child);
            }
            else if (child.tag.Contains("Normal"))
            {
                if (child.tag.Contains("1"))
                normalPlatformOneParents.Add(child);
            }
            else if (child.tag.Contains("Hard"))
            {
                if (child.tag.Contains("1"))
                hardPlatformOneParents.Add(child);
            }
            else
            {
                //Debug.LogError("There are platforms that are not identified");
            }
        }

    }


    void GetEasyOneChildren()
	{
        if (easyPlatformOneParents.Count <= 0)
            return;
        
        for (int i =0; i< easyPlatformOneParents.Count+1; i++)
		{
            for(int z = 0; z < easyPlatformOneParents[i].childCount; z++)
			{
                easyPlatformsOne[0] = easyPlatformOneParents[i].GetChild(0);
                easyPlatformsOne[1] = easyPlatformOneParents[i].GetChild(1);
            }
		}
	}

    private bool moveEasyOneIsClosingGap;
    void MoveEasyOne()
	{
        //bool isClosingGap;

        if (easyPlatformsOne[0] == null || easyPlatformsOne[1] == null || easyPlatformsOne == null)
		{
            GetEasyOneChildren();
            Debug.LogError("Platforms Retrieved");
        }
        Vector3 left = easyPlatformsOne[0].position;
        Vector3 right = easyPlatformsOne[1].position;

        float distance = Vector2.Distance(left, right);
        Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(left);
        Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(right); // -Screen.width
        Debug.Log("LEFT  " + leftScreenPos);
        Debug.Log("RIGHT  " + rightScreenPos);

        if(leftScreenPos.x < 100 && rightScreenPos.x - Screen.width < 100)
		{
            moveEasyOneIsClosingGap = true; 
        }
        else if(distance < easyOneDistance)
		{
            moveEasyOneIsClosingGap = false;	
		}
		
        if (moveEasyOneIsClosingGap)
		{
            left += new Vector3(easyOnePlatformSpeed * Time.fixedDeltaTime, 0, 0);
            right += new Vector3(easyOnePlatformSpeed * Time.fixedDeltaTime * -1, 0, 0);
		}
		else
		{
            left += new Vector3(easyOnePlatformSpeed * Time.fixedDeltaTime * -1, 0, 0);
            right += new Vector3(easyOnePlatformSpeed * Time.fixedDeltaTime, 0, 0);
        }

        easyPlatformsOne[0].position = left;
        easyPlatformsOne[1].position = right;
    }

    void GetMostLeftSide()
	{
        //Gets the most left side of the visible screen

	}

    void GetMostRightSide()
    {
        //Gets the most right side of the visible screen

    }

    /*
    IEnumerator GetAllChildrenCoroutine()
	{
        yield return new WaitForSeconds(childrenRefreshTime);
    }
    */
}
