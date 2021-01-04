using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


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


    // Start is called before the first frame update
    void Start()
    {

        player = GetComponent<Transform>();
        rb2D = player.GetComponent<Rigidbody2D>();
        checkPoint_Prefab = Resources.Load("Checkpoint_Bar") as GameObject;

        point_text = GameObject.Find("Player Point Text").GetComponent<TMP_Text>();

        playerControl.setPlayerDeadStatus(false);
    }

    private void Awake()
    {

        playerControl.setPlayerCheckPoint(true);
        playerControl.setPoint(0);
    }


    void Update()
    {

        //playerControl.addPoint(5);
        //Debug.Log("Current Point is: " + playerControl.getPoint().ToString());

        point_text.text = ("Score: " + playerControl.getPoint().ToString());

        if (Input.GetKey(KeyCode.T) && !playerControl.isPlayerDead())
        {
            playerControl.setPlayerDeadStatus(true);
            player.GetComponent<SpriteRenderer>().enabled = false;
            if(playerEjectorArrow != null) playerEjectorArrow.SetActive(false);
            if(deathScreen != null) deathScreen.SetActive(true);

            death_text = GameObject.Find("Death Score").GetComponent<TMP_Text>();
            death_text.text = "Score: " + playerControl.getPoint().ToString();
        }
    }


    public void restartGame()
    {
        playerControl.setPlayerDeadStatus(false);
        if (deathScreen != null) deathScreen.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
        if (playerEjectorArrow != null) playerEjectorArrow.SetActive(true);

        // SAVE PLAYER STATS HERE
        playerControl.restartPlayerStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void player_EnteredCheckpoint()
    {
        spawn_NextCheckpoint();

        playerControl.addPoint(1);

        
    }


    void spawn_NextCheckpoint()
    {
        new_CheckPoint = Instantiate(checkPoint_Prefab) as GameObject;
        new_CheckPoint.transform.position = transform.position;
        new_CheckPoint.transform.position = new Vector3(0, new_CheckPoint.transform.position.y + 5.25f, 0);


        if (spawnedObjects[0] == null)
        {
            spawnedObjects[0] = new_CheckPoint;
        }
        else if (spawnedObjects[1] == null)
        {
            spawnedObjects[1] = new_CheckPoint;
        }
        else if (spawnedObjects[0] != null)
        {
            Destroy(spawnedObjects[0]);

            spawnedObjects[0] = spawnedObjects[1];
            spawnedObjects[1] = new_CheckPoint;
        }
    }
}
