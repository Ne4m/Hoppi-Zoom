using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class PlatformSpawnManager : MonoBehaviour
    {
        private int _point;
        
        private float _easySpawnProbability = 100;
        private float _normalSpawnProbability = 100;
        private float _hardSpawnProbability = 100;

        private int _totalPlatforms = 4;

        private Vector3 _lastSpawnLocation;
        private List<GameObject> lastSpawned = new List<GameObject>();

        [SerializeField] private Vector3 spawnOffset;

        [SerializeField] private int probabilitiyDecrease;
        private int _easySpawnThreshold;
        private int _normalSpawnThreshold;
        private int _hardSpawnThreshold;

        [SerializeField] private GameObject[] easyPlatforms = new GameObject[4];
        [SerializeField] private GameObject[] normalPlatforms = new GameObject[4];
        [SerializeField] private GameObject[] hardPlatforms = new GameObject[4];

         // #region Platforms
         //
         // [SerializeField] private GameObject[] PlatformOneSet;
         // [SerializeField] private GameObject[] PlatformTwoSet;
         // [SerializeField] private GameObject[] PlatformThreeSet;
         // [SerializeField] private GameObject[] PlatformFourSet;
         //
         //
         // #endregion
        

        private void Awake()
        {
            //levelManager = GetComponent<LevelManager>();
        }

        private void Start()
        {
            _lastSpawnLocation = new Vector3(0,2,0);
            //GetDifficultyAndSpawn();
            StartCoroutine(SpawnFirstTwo(easyPlatforms));
        }

        private bool _isAtCheckpoint;

        // private void GetAllChildren()
        // {
        //     Transform child;
        //     for (int i = 0; i < transform.childCount; i++)
        //     {
        //     
        //         child = transform.GetChild(i);
        //         if (child.tag.Contains("Easy"))
        //         {
        //             if (child.tag.Contains("1"))
        //             {
        //                 _easyPlatformsOne.Add(child);
        //                 
        //             }else if (child.tag.Contains("2"))
        //             {
        //                 _easyPlatformsTwo.Add(child);
        //             }
        //             else if (child.tag.Contains("3"))
        //             {
        //                 _easyPlatformsThree.Add(child);
        //             }
        //             else if (child.tag.Contains("4"))
        //             {
        //                 _easyPlatformsFour.Add(child);
        //             }
        //         }
        //         else if (child.tag.Contains("Normal"))
        //         {
        //             if (child.tag.Contains("1"))
        //             {
        //                 _normalPlatformsOne.Add(child);
        //                 
        //             }else if (child.tag.Contains("2"))
        //             {
        //                 _normalPlatformsTwo.Add(child);
        //             }
        //             else if (child.tag.Contains("3"))
        //             {
        //                 _normalPlatformsThree.Add(child);
        //             }
        //             else if (child.tag.Contains("4"))
        //             {
        //                 _normalPlatformsFour.Add(child);
        //             }
        //         }
        //         else if (child.tag.Contains("Hard"))
        //         {
        //             if (child.tag.Contains("1"))
        //             {
        //                 _hardPlatformsOne.Add(child);
        //                 
        //             }else if (child.tag.Contains("2"))
        //             {
        //                 _hardPlatformsTwo.Add(child);
        //             }
        //             else if (child.tag.Contains("3"))
        //             {
        //                 _hardPlatformsThree.Add(child);
        //             }
        //             else if (child.tag.Contains("4"))
        //             {
        //                 _hardPlatformsFour.Add(child);
        //             }
        //         }
        //         else
        //         {
        //             //Debug.LogError("There are platforms that are not identified");
        //         }
        //     }
        // }
        

        public void GetDifficultyAndSpawn()
        {
            float chance;
            chance = Random.Range(0,100);

            if (chance<_easySpawnProbability)
            {
                //spawn easy
                SpawnPlatform(easyPlatforms);
            }
            else if (chance< _normalSpawnProbability)
            {
                //spawn normal
                SpawnPlatform(normalPlatforms);
            }
            else if(chance < _hardSpawnProbability)
            {
                //spawn hard
                SpawnPlatform(hardPlatforms);
            }
            else
            {
                //Go to the Next Theme 
                _easySpawnProbability = 100;
                _normalSpawnProbability = 100;
                _hardSpawnProbability = 100;
            }

        }

        private int _lastDestroyed;
        void SpawnPlatform(GameObject[] desiredSpawn)
        {
            int random = Random.Range(1, _totalPlatforms);
            lastSpawned.Add(Instantiate(desiredSpawn[random], _lastSpawnLocation + spawnOffset,quaternion.identity, this.transform));
            SetDifficulty();
            if (this.transform.childCount > 3)
            {
                DestroyLastSpawn(lastSpawned[_lastDestroyed++]);
            }
            
        }
        
        void SetDifficulty()
        {
            //_point = levelManager.playerControl.getPoint();
            _lastSpawnLocation = lastSpawned[lastSpawned.Count-1].transform.position;

            if (lastSpawned[lastSpawned.Count-1].tag.Contains("Easy"))
            {
                _easySpawnProbability -= probabilitiyDecrease;
                Debug.Log("EASY "+ _easySpawnProbability);
            }
            else if (lastSpawned[lastSpawned.Count-1].tag.Contains("Normal"))
            {
                _normalSpawnProbability -= probabilitiyDecrease;
                Debug.Log("NORMAL "+ _normalSpawnProbability);
            }
            else if (lastSpawned[lastSpawned.Count-1].tag.Contains("Hard"))
            {
                _hardSpawnProbability -= probabilitiyDecrease;
                Debug.Log("Hard "+ _hardSpawnProbability);
            }
            
            
        }
        
        IEnumerator SpawnFirstTwo(GameObject[] forcedSpawn)
        {
            while (this.transform.childCount < 2)
            {
                
                int random = Random.Range(1, _totalPlatforms);
                lastSpawned.Add(Instantiate(forcedSpawn[random], new Vector3(0,2,0),quaternion.identity, this.transform));
                yield return new WaitForSeconds(1);
                SpawnPlatform(easyPlatforms);
                yield return new WaitForSeconds(1);
                StopCoroutine(SpawnFirstTwo(easyPlatforms));
            }
            
        }

        private void DestroyLastSpawn(GameObject destroyObject)
        {
            Destroy(destroyObject);
        }

    }
}
