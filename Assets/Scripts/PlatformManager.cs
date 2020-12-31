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
    [SerializeField]
    private float normalOnePlatformSpeed = 5f;
    [SerializeField]
    private float hardOnePlatformSpeed = 5f;


    #endregion

    private List<Transform> easyPlatformsOne = new List<Transform>();
    private List<Transform> normalPlatformsOne = new List<Transform>();
    private List<Transform> hardPlatformsOne = new List<Transform>();

    [HideInInspector]
    public float childrenRefreshTime =5f;

	private void Awake()
	{
        /*
        easyPlatformsOne = new Transform[2];
        normalPlatformsOne = new Transform[2];
        hardPlatformsOne = new Transform[2];
        */
        GetAllChildren();
        
    }
	// Start is called before the first frame update
	void Start()
    {
        GetEasyOneChildren();
        GetNormalOneChildren();
        GetHardOneChildren();
    }

    // Update is called once per frame
    void Update()
    {
        //GetEasyOne();
        MoveEasyOne();
        MoveNormalOne();
        MoveHardOne();
    }
    void GetAllChildren()
	{
        Transform child;
        for (int i = 0; i < transform.childCount; i++)
        {
            
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
        
        for (int i =0; i< easyPlatformOneParents.Count; i++)
		{
            //Debug.LogError("Parent Count "+easyPlatformOneParents.Count); //1
            //Debug.LogError("Child Count " + easyPlatformOneParents[i].childCount); //2
            for(int z = 0; z < easyPlatformOneParents[i].childCount; z++)
			{
                easyPlatformsOne.Add(easyPlatformOneParents[i].GetChild(z));
            }
		}
	}

    void GetChildsChild()
	{
        for(int i =0; i<easyPlatformOneParents.Count; i++)
		{
            
            for (int z = 0; z < easyPlatformOneParents[i].childCount; z++)
			{
                easyPlatformsOne.Add(easyPlatformOneParents[i].GetChild(z));
            }

        }
	}

	void GetNormalOneChildren()
	{
        if (normalPlatformOneParents.Count <= 0)
            return;

        for (int i = 0; i < normalPlatformOneParents.Count; i++)
        {
            for (int z = 0; z < normalPlatformOneParents[i].childCount; z++)
            {
                normalPlatformsOne.Add(normalPlatformOneParents[i].GetChild(z));
            }
        }
    }

    void GetHardOneChildren()
	{
        if (hardPlatformOneParents.Count <= 0)
            return;

        for (int i = 0; i < hardPlatformOneParents.Count; i++)
        {
            for (int z = 0; z < hardPlatformOneParents[i].childCount; z++)
            {
                hardPlatformsOne.Add(hardPlatformOneParents[i].GetChild(z));
            }
        }
    }


    private bool moveEasyOneIsClosingGap;
    void MoveEasyOne()
	{
        //bool isClosingGap;
        if (easyPlatformsOne[0] == null || easyPlatformsOne[1] == null)
		{
            GetEasyOneChildren();
            Debug.LogError("Platforms Retrieved");
        }
        if (easyPlatformsOne.Count > 2)
            Debug.LogError("Excessive Count");

        Vector3 left = easyPlatformsOne[0].position;
        Vector3 right = easyPlatformsOne[1].position;

        float distance = Vector2.Distance(left, right);
        Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(left);
        Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(right); // -Screen.width
        //Debug.Log("LEFT  " + leftScreenPos);
        //Debug.Log("RIGHT  " + rightScreenPos);

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

    private bool moveNormalOneIsClosingGap;
    void MoveNormalOne()
	{
        if (normalPlatformsOne[0] == null || normalPlatformsOne[1] == null || normalPlatformsOne == null)
        {
            GetNormalOneChildren();
            Debug.LogError("Platforms Retrieved");
        }
        Vector3 left = normalPlatformsOne[0].position;
        Vector3 right = normalPlatformsOne[1].position;

        float distance = Vector2.Distance(left, right);
        Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(left);
        Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(right); // -Screen.width
        //Debug.Log("LEFT  " + leftScreenPos);
        //Debug.Log("RIGHT  " + rightScreenPos);

        if (leftScreenPos.x < 100 && rightScreenPos.x - Screen.width < 100)
        {
            moveNormalOneIsClosingGap = true;
        }
        else if (distance < normalOneDistance)
        {
            moveNormalOneIsClosingGap = false;
        }

        if (moveNormalOneIsClosingGap)
        {
            left += new Vector3(normalOnePlatformSpeed * Time.fixedDeltaTime, 0, 0);
            right += new Vector3(normalOnePlatformSpeed * Time.fixedDeltaTime * -1, 0, 0);
        }
        else
        {
            left += new Vector3(normalOnePlatformSpeed * Time.fixedDeltaTime * -1, 0, 0);
            right += new Vector3(normalOnePlatformSpeed * Time.fixedDeltaTime, 0, 0);
        }

        normalPlatformsOne[0].position = left;
        normalPlatformsOne[1].position = right;
    }

    private bool moveHardOneIsClosingGap;
    void MoveHardOne()
    {
        if (hardPlatformsOne[0] == null || hardPlatformsOne[1] == null || hardPlatformsOne == null)
        {
            GetHardOneChildren();
            Debug.LogError("Platforms Retrieved");
        }
        Vector3 left = hardPlatformsOne[0].position;
        Vector3 right = hardPlatformsOne[1].position;

        float distance = Vector2.Distance(left, right);
        Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(left);
        Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(right); // -Screen.width
        //Debug.Log("LEFT  " + leftScreenPos);
        //Debug.Log("RIGHT  " + rightScreenPos);

        if (leftScreenPos.x < 100 && rightScreenPos.x - Screen.width < 100)
        {
            moveHardOneIsClosingGap = true;
        }
        else if (distance < normalOneDistance)
        {
            moveHardOneIsClosingGap = false;
        }

        if (moveHardOneIsClosingGap)
        {
            left += new Vector3(hardOnePlatformSpeed * Time.fixedDeltaTime, 0, 0);
            right += new Vector3(hardOnePlatformSpeed * Time.fixedDeltaTime * -1, 0, 0);
        }
        else
        {
            left += new Vector3(hardOnePlatformSpeed * Time.fixedDeltaTime * -1, 0, 0);
            right += new Vector3(hardOnePlatformSpeed * Time.fixedDeltaTime, 0, 0);
        }

        hardPlatformsOne[0].position = left;
        hardPlatformsOne[1].position = right;
    }


    /*
    IEnumerator GetAllChildrenCoroutine()
	{
        yield return new WaitForSeconds(childrenRefreshTime);
    }
    */
}
