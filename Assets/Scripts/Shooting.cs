using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    LevelManager levelManager;
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    [Header("Shooting Variables")]
    [SerializeField] private float fireRate = 1.0f;
    [SerializeField] private float lastShot = 0.0f;
    [SerializeField] private int maxAmmo = 3;
    [SerializeField] private int currentAmmo = 3;

    [SerializeField] private Image progressBarContainer;
    [SerializeField] private Image progressBar;
    [SerializeField] private float ammoRechargeTime = 5f;
    [SerializeField] private int rechargedAmmoAmount = 1;

    private float lastRechargeTime;


    private RectTransform canvasRect;
    private string ammoReceiveMsg = "Replenished!";
    UIMessager messager;

    public static Shooting instance;

    private void Awake()
    {
        instance = this;
        ammoReceiveMsg = I18n.Fields["T_REPLENISHED"];
    }

    void Start()
    {
        levelManager = gameObject.GetComponent<LevelManager>();
        messager = gameObject.GetComponent<UIMessager>();

        currentAmmo = maxAmmo;

        canvasRect = progressBarContainer.GetComponent<RectTransform>();

        DisableRechargeBar();

    }


    public int timeI = 0;
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.touchCount == 2) && levelManager.playerControl.IsPlayerInCheckPoint() && !levelManager.playerControl.isPlayerDead())
        {
            if (Time.time > fireRate + lastShot)
            {
                Shoot();
            }
        }


        if (Perks.instance != null && Perks.instance.CanRechargeAmmo())
        {
            if (!IsFullAmmo())
            {
                if (levelManager.playerControl.getPoint() > 0)
                {
                    if (Time.time > 1f + lastRechargeTime)
                    {

                        progressBar.fillAmount += (1 / ammoRechargeTime);


                        if (progressBar.fillAmount > 0.9f)
                        {
                            if (!IsFullAmmo())
                            {
                                ammoReceiveMsg = I18n.Fields["T_RECHARGED"]; //"Recharged!";
                                AddAmmo(rechargedAmmoAmount);
                            }
                            DisableRechargeBar();
                        }


                        lastRechargeTime = Time.time;
                    }

                    EnableRechargeBar();
                }
            
            }
            else
            {
                DisableRechargeBar();
            }

            //if (canvasRect.gameObject.activeSelf)
            //{
            //    var canvasFollow = new Vector3(transform.position.x, transform.position.y - 0.4879446f, transform.position.z);
            //    var canvElemCanvasPosition = GetCanvasPositionOfWorld(canvasFollow);
            //    canvasRect.position = canvElemCanvasPosition;
            //}
        }
    }

    public void EnableRechargeBar()
    {
        canvasRect.gameObject.SetActive(true);
    }

    public void DisableRechargeBar()
    {
        canvasRect.gameObject.SetActive(false);
        progressBar.fillAmount = 0;
    }

    public Vector2 GetWorldPoisitionOfCanvas(RectTransform element)
    {

        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera.main, out var result);

        return result;
    }

    public Vector3 GetCanvasPositionOfWorld(Vector3 coords)
    {
        var result = RectTransformUtility.WorldToScreenPoint(Camera.main, coords);

        return result;
    }


    public void SetShootingPoint(Transform sp)
    {
        shootingPoint = sp;
    }
    public void SendShoot()
    {
        if (Time.time > fireRate + lastShot)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
       // if (currentAmmo <= 0) return;

        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        //FindObjectOfType<AudioManager>().Play("Shoot");

        AudioManager.instance.Play("Shoot");

        lastShot = Time.time;
        currentAmmo--;
    }


    public bool IsFullAmmo()
    {
        return (GetCurrentAmmo() >= GetMaxAmmo());
    }
    public void ReplenishAmmo()
    {
        currentAmmo = maxAmmo;

        messager.startMsg($"{I18n.Fields["T_AMMO_REPLENISHED"]}" , 2, Vector3.zero);
    }

    public void AddAmmo(int amount)
    {
        if (currentAmmo == maxAmmo) return;

        if(currentAmmo + amount > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        else currentAmmo += amount;


        messager.startMsg($"{amount} {I18n.Fields["T_AMMO_SHOOTING"]} {ammoReceiveMsg}", 2, Vector3.zero);
        ammoReceiveMsg = I18n.Fields["T_AMMO_RECEIVED"]; // Received!"
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public void SetCurrentAmmo(int amount)
    {
        this.currentAmmo = amount;
    }

    public void SetMaxAmmo(int amount)
    {
        if(Perks.instance != null)
        {
            amount += Perks.instance.ExtraAmmo;
        }

        this.maxAmmo = amount;
    }

    public int GetMaxAmmo()
    {
        return this.maxAmmo;
    }
}
