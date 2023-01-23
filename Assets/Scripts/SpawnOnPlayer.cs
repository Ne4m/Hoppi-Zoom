using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnPlayer : MonoBehaviour
{
    void Start()
    {
        Transform player = FindObjectOfType<PlayerController>().transform;
        transform.position = player.position;
    }

}
