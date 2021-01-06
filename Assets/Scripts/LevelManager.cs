using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class LevelManager : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb2D;
    private GameObject checkPoint_Prefab;
    private GameObject new_CheckPoint;
    private GameObject last_CheckPoint;

    [SerializeField]
    private GameObject deathScreen;

    [SerializeField]
    private float spawnDistance = 5;


    [SerializeField]
    private GameObject playerEjectorArrow;

    public TMP_Text point_text;
    public TMP_Text death_text;

    private bool isINCP;

    //[SerializeField]
    //private int point = 0;

    private GameObject[] spawnedObjects = new GameObject[2];


    public struct GameInfo
    {
        private bool isINCP;
        private bool isDead;
        private int point;
        private int highScore;
        private int level;
        private int health;
        private int body, reflexes, intelligence, technical_ability, cool, constitution;


        public bool IsPlayerInCheckPoint()
        {
            return isINCP;
        }


        public void setPlayerCheckPoint(bool c)
        {
            this.isINCP = c;
        }

        public void setPlayerDeadStatus(bool isDead)
        {
            this.isDead = isDead;
        }

        public bool isPlayerDead()
        {
            return isDead;
        }

        // Point & Level Stuff
        public void addPoint(int point)
        {
            this.point += point;
        }

        public void setPoint(int point)
        {
            this.point = point;
        }

        public int getPoint()
        {
            return this.point;
        }

        public void setLevel(int level)
        {
            this.level = level;

        }


        // Change level stuff
        public int getLevel()
        {
            return level;
        }

        // HP Stuff
        public int getHealth()
        {
            return health;
        }
        
        public void setHealth(int hp)
        {
            health = hp;
        }

        public void heal(int amount)
        {
            if (amount > 100) this.health = 100;
            else if (amount > 0 && amount < 100) this.health += amount;
        }

        public void damage(int amount)
        {
            if(amount > 100)
            {
                health = 0;
                // Kill player end game
            }

            if(amount > 0 && amount < 100) this.health -= amount;
        }

        // Game Restart
        public void restartPlayerStats()
        {
            this.point = 0;
            this.level = 0;
            this.isINCP = true;
            this.health = 100;
        }
    }

    public GameInfo playerControl;

    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        playerControl.setPlayerCheckPoint(true);
        playerControl.setPoint(0);
    }

    // Start is called before the first frame update
    void Start()
    {

        player = GetComponent<Transform>();
        rb2D = player.GetComponent<Rigidbody2D>();
        checkPoint_Prefab = Resources.Load("Checkpoint_Bar") as GameObject;

        //Button btn = dieButton.GetComponent<Button>();
       // btn.onClick.AddListener(bringDeathMenu);
        //point_text = GameObject.Find("Player Point Text").GetComponent<TMP_Text>();

        playerControl.setPlayerDeadStatus(false);

        //Social.localUser.Authenticate(
        //        (bool success) =>
        //        {

        //        }
        //    );

        //playerControl.setPoint(PlayerPrefs.GetInt("point", 0));

        

        Debug.Log("Your Highest Score Was " + PlayerPrefs.GetInt("highScore", 0));
    }




    void Update()
    {

        //playerControl.addPoint(5);
        //Debug.Log("Current Point is: " + playerControl.getPoint().ToString());

        if (Input.GetKey(KeyCode.Y) && !playerControl.isPlayerDead())
        {
            PlayerPrefs.DeleteKey("highScore");
            print("High Score Cleared");
        }

            point_text.text = ("Score: " + playerControl.getPoint().ToString());

        checkDeathMenu();
    }
    
    public void checkDeathMenu()
    {
        if (Input.GetKey(KeyCode.T) && !playerControl.isPlayerDead()) // Future Death Detection
        {
            bringDeathMenu();
        }
    }

    public void bringDeathMenu()
    {
        
        playerControl.setPlayerDeadStatus(true);
        player.GetComponent<SpriteRenderer>().enabled = false;
        if (playerEjectorArrow != null) playerEjectorArrow.SetActive(false);
        if (deathScreen != null) deathScreen.SetActive(true);

        death_text = GameObject.Find("Death Score").GetComponent<TMP_Text>();
        death_text.text = "Score: " + playerControl.getPoint().ToString();
        Debug.Log("Button Score:" + death_text.text);


        // SAVE PLAYER STATS HERE
        if (playerControl.getPoint() > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", playerControl.getPoint());
            PlayerPrefs.Save();
            Debug.Log("New Highscore!!! " + playerControl.getPoint());

        }
    }

    public void goBackMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void restartGame()
    {
        playerControl.setPlayerDeadStatus(false);
        if (deathScreen != null) deathScreen.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
        if (playerEjectorArrow != null) playerEjectorArrow.SetActive(true);


        playerControl.restartPlayerStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //.buildIndex
    }


    public void player_EnteredCheckpoint()
    {
        spawn_NextCheckpoint();

        playerControl.addPoint(1);

        
    }

    public void getPlayerPos()
    {

    }

    public void setPlayerPos()
    {
        player.transform.position = new Vector3(player.transform.position.x, new_CheckPoint.transform.position.y, 0);
    }


    void spawn_NextCheckpoint()
    {

        new_CheckPoint = Instantiate(checkPoint_Prefab) as GameObject;
  //      if(new_CheckPoint.transform.position != checkPoint_Prefab.transform.position)
		//{
  //          new_CheckPoint.transform.position = checkPoint_Prefab.transform.position;
  //      }
        new_CheckPoint.transform.position = new Vector3(0, new_CheckPoint.transform.position.y + spawnDistance, 0);
        
        if (spawnedObjects[0] == null)
        {
            spawnedObjects[0] = new_CheckPoint;
            //just started man
            
        }
        else if (spawnedObjects[1] == null)
        {
            new_CheckPoint.transform.position = new Vector3 (0, spawnedObjects[0].transform.position.y+spawnDistance,0) ;
            spawnedObjects[1] = new_CheckPoint;
        }
        else if (spawnedObjects[0] != null)
        {
            Destroy(spawnedObjects[0]);

            new_CheckPoint.transform.position = new Vector3(0, spawnedObjects[1].transform.position.y + spawnDistance, 0);

            spawnedObjects[0] = spawnedObjects[1];
            spawnedObjects[1] = new_CheckPoint;
        }
        
    }
}
