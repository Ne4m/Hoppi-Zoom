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
    private Transform player;
    private Rigidbody2D rb2D;
    private GameObject checkPoint_Prefab;
    private GameObject new_CheckPoint;
    private GameObject last_CheckPoint;


    [SerializeField] Button shootButton;

    public GameObject blurScreen;

    [SerializeField]
    private GameObject deathScreen;

    [SerializeField]
    private float spawnDistance = 10;


    [SerializeField]
    private GameObject playerEjectorArrow;

    #region Level Stuff
    [Header ("Level Stuff")]
    [SerializeField] private int playerLevel;
    [SerializeField] private int perPointToLevelThreshold = 5;
    [SerializeField] private int levelMultiplier = 2;
    #endregion

    [Header ("Text Boxes")]
    [SerializeField] private TMP_Text point_text;
    [SerializeField] private TMP_Text death_text;
    [SerializeField] private TMP_Text health_text;

    private bool isINCP;

    //[SerializeField]
    //private int point = 0;

    [Header ("Cloud Spawn Stuff")]
    private GameObject[] cloudSpawnerCap = new GameObject[2];
    [SerializeField] private GameObject cloudSpawner;
    [SerializeField] private float cloudSpawnDistance;
    private GameObject newCloudSpawner;
    private GameObject lastCloudSpawned;


    private GameObject[] spawnedObjects = new GameObject[2];

    private Vector3 lastCheckpoint;

    PlatformSpawner platformSpawner;
    CameraController camCont;
    Shooting shooting;
    UIMessager messager;

    //public struct Test
    //{
    //    private float hp;
    //    public Test(float a)
    //    {
    //        this.hp = a;
    //    }
    //}

    public playerInfo playerControl;

    public class playerInfo
    {
        private bool isInCP;
        private bool isDead;
        private int point;
        private int highScore;
        private int level;
        private float currentHealth;
        private float maxHealth;

        private float speed;
        private float rotateSpeed;

        public playerInfo(float maxHealth, float speed, float rotateSpeed)
        {
            this.currentHealth = maxHealth;
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.rotateSpeed = rotateSpeed;
        }

        public bool IsPlayerInCheckPoint()
        {
            return isInCP;
        }


        public void setPlayerCheckPoint(bool c)
        {
            this.isInCP = c;
        }

        public bool getPlayerCheckpoint()
        {
            return this.isInCP;
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

        public void levelUp()
        {
            level++;
        }

        public float getPlayerSpeed()
        {
            return this.speed;
        }

        public void setPlayerSpeed(float speed)
        {
            this.speed = speed;
        }

        public float getPlayerPointerSpeed()
        {
            return this.rotateSpeed;
        }
        public void setPlayerPointerSpeed(float speed)
        {
            this.rotateSpeed = speed;
        }


        // Change level stuff
        public int getLevel()
        {
            return level;
        }

        // HP Stuff
        public float getHealth()
        {
            return currentHealth;
        }

        public float getMaxHealth()
        {
            return maxHealth;
        }
        
        public void setHealth(float hp)
        {
            currentHealth = hp;

            if(currentHealth < 0) currentHealth = 0;
            if(currentHealth > maxHealth) currentHealth = maxHealth;
        }

        public void heal(float amount)
        {
            if (amount > maxHealth) setHealth(maxHealth);
            else if (amount > 0 && amount < maxHealth) setHealth(currentHealth+amount);
        }

        public void percentageHeal(float percentage)
        {
            float tempHP = maxHealth;

            tempHP = currentHealth + (tempHP / 100) * percentage;

            setHealth(tempHP);
        }

        public void damage(float amount)
        {
            if(amount > maxHealth)
            {
                this.setHealth(0);
            }

            if(amount > 0 && amount < maxHealth) setHealth(currentHealth-amount);
        }

        // Game Restart
        public void restartPlayerStats()
        {
            this.point = 0;
            this.level = 0;
            this.isInCP = true;
            this.currentHealth = maxHealth;
        }
    }



    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        playerControl = new playerInfo(PlayerPrefs.GetFloat("playerHealth", 1000),
                                       PlayerPrefs.GetFloat("playerSpeed", 1000),
                                       PlayerPrefs.GetFloat("rotateSpeed", 100));

        playerControl.setPlayerCheckPoint(true);
        playerControl.setPoint(0);

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        rb2D = player.GetComponent<Rigidbody2D>();
        checkPoint_Prefab = Resources.Load("Checkpoint_Bar") as GameObject;

        platformSpawner = gameObject.GetComponent<PlatformSpawner>();
        camCont = gameObject.GetComponent<CameraController>();
        shooting = gameObject.GetComponent<Shooting>();
        messager = gameObject.GetComponent<UIMessager>();

        shooting.SetMaxAmmo(PlayerPrefs.GetInt("playerAmmo", 1));

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

        // Update Player Controller Variables On Start

        playerControl.setLevel(0);
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

        playerLevel = playerControl.getLevel();
        checkDeathMenu();

        health_text.text = ($"HP: {playerControl.getHealth()}\n" +
                            $"SPEED: {playerControl.getPlayerSpeed()}\n" +
                            $"AMMO: {shooting.GetCurrentAmmo()}");


    }

    public void getHit(float amount)
    {
        playerControl.damage(amount);
    }
    public void getHeal(float amount)
    {
        playerControl.heal(amount);
    }

    public void percHeal(float perc)
    {
        playerControl.percentageHeal(perc);
    }

    public void checkDeathMenu()
    {
        if (Input.GetKey(KeyCode.T) && !playerControl.isPlayerDead()) // Future Death Detection
        {
            bringDeathMenu();
        }


        if(!playerControl.isPlayerDead() && playerControl.getHealth() <= 0)
        {
            bringDeathMenu();
        }
    }

    public void bringDeathMenu()
    {
        
        playerControl.setPlayerDeadStatus(true);
        player.GetComponent<SpriteRenderer>().enabled = false;
        if (playerEjectorArrow != null) playerEjectorArrow.SetActive(false);
        //shootButton.gameObject.SetActive(false);

        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
            blurScreen.SetActive(true);
        }

        death_text = GameObject.Find("Score Point").GetComponent<TMP_Text>();
        death_text.text = playerControl.getPoint().ToString();
        Debug.Log("Button Score:" + death_text.text);


        // SAVE PLAYER STATS HERE
        if (playerControl.getPoint() > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", playerControl.getPoint());
            PlayerPrefs.Save();
            Debug.Log("New Highscore!!! " + playerControl.getPoint());

        }

        PauseGame();
    }

    public void onContinue()
    {
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0f;
        rb2D.transform.position = lastCheckpoint;
        //player.position = lastCheckpoint;

        //shootButton.gameObject.SetActive(true);

        deathScreen.SetActive(false);
        blurScreen.SetActive(false);

        playerControl.setHealth(playerControl.getMaxHealth());
        playerControl.setPlayerDeadStatus(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
        playerEjectorArrow.SetActive(true);

        ResumeGame();
    }

    public void goBackMainMenu()
    {
        onContinue();
        restartGame();
        SceneManager.LoadScene("Main Menu");
    }

    public void restartGame()
    {
        playerControl.setPlayerDeadStatus(false);
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
            blurScreen.SetActive(false);
        }
        player.GetComponent<SpriteRenderer>().enabled = true;
        if (playerEjectorArrow != null) playerEjectorArrow.SetActive(true);


        playerControl.restartPlayerStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //.buildIndex
    }


    public void player_EnteredCheckpoint()
    {
        if(new_CheckPoint != null) lastCheckpoint = new_CheckPoint.transform.position;

        spawn_NextCheckpoint();
        
        playerControl.addPoint(1);

        checkLevelStatus();


        spawnClouds();
        platformSpawner.initiateSpawn();




        if (playerControl.getPoint() % 3 == 0)
        {
            shooting.AddAmmo(1);

        }
    }


    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void spawnClouds()
    {
        // WTF
        if (lastCloudSpawned != null)
        {
            Destroy((GameObject)lastCloudSpawned);
        }

        Vector3 addedDist = transform.position + new Vector3(0, cloudSpawnDistance, 0);
        newCloudSpawner = Instantiate(cloudSpawner, addedDist, Quaternion.identity);
        lastCloudSpawned = newCloudSpawner;
    }


    private void checkLevelStatus()
    {
        if (playerControl.getPoint() >= perPointToLevelThreshold)
        {
            playerControl.levelUp();
            perPointToLevelThreshold *= levelMultiplier;
        }
    }



    void spawn_NextCheckpoint()
    {


        new_CheckPoint = Instantiate(checkPoint_Prefab) as GameObject;
        new_CheckPoint.transform.position = new Vector3(0, new_CheckPoint.transform.position.y + spawnDistance, 0);
        new_CheckPoint.tag = "Checkpoint";
        new_CheckPoint.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,125);
        new_CheckPoint.GetComponent<SpriteRenderer>().sortingOrder = -1;



        if (spawnedObjects[0] == null)
        {
            spawnedObjects[0] = new_CheckPoint;
            //just started man

        }
        else if (spawnedObjects[1] == null)
        {
            new_CheckPoint.transform.position = new Vector3(0, spawnedObjects[0].transform.position.y + spawnDistance, 0);
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
