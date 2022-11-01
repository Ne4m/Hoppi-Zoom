using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms;
using System;

public class LevelManager : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb2D;
    private GameObject checkPoint_Prefab;
    private GameObject new_CheckPoint;
    private GameObject last_CheckPoint;

    [Header("Playable Area Out of Bounds Check")]
    private float screenWidth, screenHeight, screenDistance;
    [SerializeField] private GameObject playableArea;
    [SerializeField] private float playableAreaOffset = 0.35f;


    [Header("Misc")]
    [SerializeField] Button shootButton;
    public GameObject blurScreen;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float spawnDistance = 10;


    [Header("Currency Stuff")]
    [SerializeField] private int earnPercent;
    [SerializeField] private int totalCurrency;
    [SerializeField] private int currentCurrency = 0;

    [SerializeField]
    private GameObject playerEjectorArrow;

    #region Level Stuff
    [Header("Level Stuff")]
    [SerializeField] private int playerLevel;
    [SerializeField] private int perPointToLevelThreshold = 5;
    [SerializeField] private int levelMultiplier = 2;
    #endregion

    [Header("Text Boxes")]
    [SerializeField] private TMP_Text point_text;
    [SerializeField] private TMP_Text endgameScore_text;
    [SerializeField] private TMP_Text endgameEarnedGold_text;
    [SerializeField] private TMP_Text health_text;
    [SerializeField] private TMP_Text ammo_text;
    [SerializeField] private TMP_Text debug_text;


    [Header("Gold Paid Continue")]
    [SerializeField] private Button goldContinueButton;
    [SerializeField] private TMP_Text goldContinuePriceText;
    private int goldContinuePrice;


    //[SerializeField]
    //private int point = 0;

    [Header("Cloud Spawn Stuff")]
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
    UI_Animations uiAnimations;


    public static LevelManager instance;

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

        private int dmgReduction = 0;

        public playerInfo(float maxHealth, float speed, float rotateSpeed, int dmgReduction)
        {
            this.currentHealth = maxHealth;
            this.maxHealth = maxHealth;
            this.speed = speed;
            this.rotateSpeed = rotateSpeed;
            this.dmgReduction = dmgReduction;
        }

        public int DamageReduction
        {
            get => dmgReduction;
            set => dmgReduction = value;
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

        public void setPlayerDeadStatus(bool _isDead)
        {
            isDead = _isDead;
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

            if (currentHealth < 0) currentHealth = 0;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
        }

        public void heal(float amount)
        {
            if (amount > maxHealth) setHealth(maxHealth);
            else if (amount > 0 && amount < maxHealth) setHealth(currentHealth + amount);
        }

        public void percentageHeal(float percentage)
        {
            float tempHP = maxHealth;

            tempHP = currentHealth + (tempHP / 100) * percentage;

            setHealth(tempHP);
        }

        public void damage(float amount)
        {
            amount = amount - (amount * DamageReduction / 100);

            if (amount > maxHealth)
            {
                this.setHealth(0);
            }

            if (amount > 0 && amount < maxHealth)
            {
                setHealth(currentHealth - amount);

            }
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

    public void AddCurrentCurrency(int amount)
    {
        currentCurrency += amount;
        messager.startMsg($"{I18n.Fields["T_GOLD_EARNED"]} {amount} {I18n.Fields["T_GOLD"]}", 2f, Vector3.zero);
    }

    private void Awake()
    {

        playerControl = new playerInfo(SPrefs.GetFloat("playerHealth", 1000),
                                       SPrefs.GetFloat("playerSpeed", 1000),
                                       SPrefs.GetFloat("rotateSpeed", 100),
                                       Perks.instance.DamageReduction);

        playerControl.setPlayerCheckPoint(true);
        playerControl.setPoint(0);

        instance = this;

        totalCurrency = SPrefs.GetInt("gameCurrency", 0);


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

        shooting.SetMaxAmmo(SPrefs.GetInt("playerAmmo", 3));

        uiAnimations = UI_Animations.instance;

        if (goldContinueButton != null) goldContinueButton.onClick.AddListener(GoldContinueButton_Clicked);
        //Button btn = dieButton.GetComponent<Button>();
        // btn.onClick.AddListener(bringDeathMenu);
        //point_text = GameObject.Find("Player Point Text").GetComponent<TMP_Text>();

        playerControl.setPlayerDeadStatus(false);
        health_text.gameObject.SetActive(true);
        point_text.gameObject.SetActive(true);




        screenHeight = Screen.height;
        screenWidth = Screen.width;

        // InvokeRepeating("OutOfBoundsCheck", 1f, 1f);

        // Update Player Controller Variables On Start

        playerControl.setLevel(0);
        Debug.Log("Your Highest Score Was " + SPrefs.GetInt("highScore", 0));

        TutorialsManager.instance.PlayTutorial(0);


    }

    private void OutOfBoundsCheck()
    {

        //var result = RectTransformUtility.WorldToScreenPoint(Camera.main, playableArea.transform.position);
        ////var distance = MathF.Sqrt(MathF.Pow(result.x, 2) + MathF.Pow(result.y, 2));

        //var characterPos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        //var charDistance = Vector3.Distance(characterPos, result);
        // Screen Height 2960 Width: 1440


        //Debug.Log($"Character Pos {characterPos}, Distance to Mid Point {charDistance}\n" +
        //          $"Middle Point Screen Pos: {result}, Distance to mid {distance}");

        //if(charDistance > result.y)
        //{
        //    ResetPlayerPosition();
        //}

        // Vector3 cameraPos = Camera.main.transform.position;
        Vector2 screenSize;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f; //Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half, since we only need half that value for distance away from the camera to the edge
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        var playableAreaCollider = playableArea.GetComponent<BoxCollider2D>();
        playableAreaCollider.size = new Vector3((screenSize.x * 2) + playableAreaOffset, (screenSize.y * 6) + playableAreaOffset, 0f);



        // Debug.Log($"Bounds: {playableAreaCollider.size} || {playableAreaCollider.size.x} || {screenSize.x}");


        if (playableAreaCollider.bounds.Contains(player.transform.position))
        {
            //Debug.Log("IN BOUNDS!!");
        }
        else
        {
            //Debug.Log("OUT OF BOUNDS!!");
            ResetPlayerPosition();
        }
    }




    void Update()
    {
        OutOfBoundsCheck();

        //playerControl.addPoint(5);
        //Debug.Log("Current Point is: " + playerControl.getPoint().ToString());

        if (Input.GetKey(KeyCode.Y) && !playerControl.isPlayerDead())
        {
            SPrefs.DeleteKey("highScore");
            print("High Score Cleared");
        }

        point_text.text = ($"<size=85%>{I18n.Fields["T_SCORE_GAME"]}</size>\n<size=175%>{playerControl.getPoint().ToString()}</size>");

        playerLevel = playerControl.getLevel();
        checkDeathMenu();

        health_text.text = $"{I18n.Fields["T_HEALTH_GAME"]}: {playerControl.getHealth()}";
        ammo_text.text = $"{I18n.Fields["T_AMMO_GAME"]}: {shooting.GetCurrentAmmo()}";

        //                             $"SPEED: {playerControl.getPlayerSpeed()}\n" +



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
        if ((Input.GetKey(KeyCode.T) || Input.touchCount == 3) && !playerControl.isPlayerDead()) // Future Death Detection
        {
            if (Perks.instance.CheatsDeath)
            {
                CheatDeath();
            }
            else
            {
                bringDeathMenu();
            }
        }


        if (!playerControl.isPlayerDead() && playerControl.getHealth() <= 0)
        {
            if (Perks.instance.CheatsDeath)
            {
                CheatDeath();
            }else
            {
                bringDeathMenu();
            }

        }
    }

    private void CheatDeath()
    {
        PlayerConditions.instance.HealthbarShow = true;
        percHeal(100);
        Debug.Log("Death Cheated!");
        messager.startMsg("CHEATED DEATH!", 3, Vector3.zero);
        Perks.instance.CheatsDeath = false;
        playerControl.setPlayerDeadStatus(false);
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

            health_text.gameObject.SetActive(false);
            point_text.gameObject.SetActive(false);
            messager.HideMessage();
        }

        //death_text = GameObject.Find("Score Point").GetComponent<TMP_Text>();
        endgameScore_text.text = playerControl.getPoint().ToString();
        Debug.Log("Button Score:" + endgameScore_text.text);


        currentCurrency += (playerControl.getPoint() * earnPercent) / 100;
        endgameEarnedGold_text.text = currentCurrency.ToString();

        //debug_text.text = currentCurrency.ToString();

        Debug.Log($"Currency Earned {currentCurrency}");
        // debug_text.text = currentCurrency.ToString();


        if (GooglePlayServices.instance != null && GooglePlayServices.instance.GooglePlayConnection)
        {
            Social.ReportScore(playerControl.getPoint(), GPGSIds.leaderboard_score, (success) =>
            {
                if (success) Debug.Log("Leaderboard update successfull!");
                else Debug.Log("There was an error while updating the leaderboard!");
            });
        }

        // SAVE PLAYER STATS HERE
        if (playerControl.getPoint() > SPrefs.GetInt("highScore", 0))
        {
            SPrefs.SetInt("highScore", playerControl.getPoint());
            SPrefs.Save();
            Debug.Log("New Highscore!!! " + playerControl.getPoint());

            uiAnimations.SetHighScoreState(true);
        }
        else
        {
            AudioManager.instance.Play("Death");
            uiAnimations.SetHighScoreState(false);
        }

        var hpBar = GameObject.Find("HealthBar-Container");
        if (hpBar != null) hpBar.SetActive(false);

        uiAnimations.SwitchTitleImage();
        uiAnimations.SetAnimations();

        RefreshTotalCurrency();

        goldContinuePrice = (currentCurrency >= totalCurrency ? (currentCurrency / 2) : (totalCurrency / 2));
        goldContinuePriceText.text = goldContinuePrice.ToString();

        PauseGame();
    }

    public void DoubleEarnedCurrency()
    {
        currentCurrency *= 2;

        endgameEarnedGold_text.text = currentCurrency.ToString();

        goBackMainMenu();
    }

    public void onContinue()
    {

        ResetPlayerPosition();
        //player.position = lastCheckpoint;

        //shootButton.gameObject.SetActive(true);

        deathScreen.SetActive(false);
        blurScreen.SetActive(false);
        health_text.gameObject.SetActive(true);
        point_text.gameObject.SetActive(true);

        playerControl.setHealth(playerControl.getMaxHealth());
        playerControl.setPlayerDeadStatus(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
        playerEjectorArrow.SetActive(true);


        uiAnimations.ResetAnimations();
        ResumeGame();
    }

    public void ResetPlayerPosition()
    {
        Debug.Log("Resetting player position..");
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0f;
        rb2D.transform.position = lastCheckpoint;
    }

    private void GoldContinueButton_Clicked()
    {
        if (totalCurrency >= goldContinuePrice)
        {
            totalCurrency -= goldContinuePrice;
            SPrefs.SetInt("gameCurrency", totalCurrency);
            SPrefs.Save();
            onContinue();

        }
    }

    private void RefreshTotalCurrency()
    {
        totalCurrency = SPrefs.GetInt("gameCurrency", 0);
    }

    public void goBackMainMenu()
    {
        totalCurrency += currentCurrency;
        SPrefs.SetInt("gameCurrency", totalCurrency);
        SPrefs.Save();


        onContinue();
        restartGame();
        SceneManager.LoadScene("Main Menu");
    }


    public void restartGame()
    {
        currentCurrency = 0;

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
        if (new_CheckPoint != null) lastCheckpoint = new_CheckPoint.transform.position;

        spawn_NextCheckpoint();
        playerControl.addPoint(1);


        if (GooglePlayServices.instance != null && GooglePlayServices.instance.GooglePlayConnection)
        {

            Social.ReportProgress("CgkIuYDT178fEAIQBA", 100.0f, (bool success) => {
            });

        }


        checkLevelStatus();


        spawnClouds();
        platformSpawner.initiateSpawn();



        if (Perks.instance != null)
        {
            if (playerControl.getPoint() % Perks.instance.GetAmmoRewardThreshold() == 0)
            {
                shooting.AddAmmo(Perks.instance.GetAmmoReward());

            }
        }

        if(GiftSpawner.instance != null)
        {
            if(playerControl.getPoint() % GiftSpawner.instance.GiftSpawnThreshold == 0)
            {
                GiftSpawner.instance.Spawn();
            }

        }

        TutorialsManager.instance.PlayTutorial(1);

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
        new_CheckPoint.GetComponent<SpriteRenderer>().color = new Color32(207, 207, 207, 145);
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
