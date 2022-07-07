using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    UIMessager messager;

    void Start()
    {
        levelManager = gameObject.GetComponent<LevelManager>();
        messager = gameObject.GetComponent<UIMessager>();

        currentAmmo = maxAmmo;
    }

  
    void Update()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.touchCount == 2) && levelManager.playerControl.IsPlayerInCheckPoint() && !levelManager.playerControl.isPlayerDead())
        {
            if (Time.time > fireRate + lastShot)
            {
                Shoot();
            }
        }

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
        if (currentAmmo <= 0) return;

        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        FindObjectOfType<AudioManager>().Play("Shoot");

        lastShot = Time.time;
        currentAmmo--;
    }


    public void ReplenishAmmo()
    {
        currentAmmo = maxAmmo;

        messager.startMsg($"All Ammo Replenished!", 2, Vector3.zero);
    }

    public void AddAmmo(int amount)
    {
        if (currentAmmo == maxAmmo) return;

        currentAmmo += amount;

        messager.startMsg($"{amount} Ammo Received!", 2, Vector3.zero);
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
        this.maxAmmo = amount;
    }

    public int GetMaxAmmo()
    {
        return this.maxAmmo;
    }
}
