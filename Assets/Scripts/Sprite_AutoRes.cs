using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]

public class Sprite_AutoRes : MonoBehaviour
{

    //public GameObject[] backgroundImage;

    [SerializeField]
    List<GameObject> spritesToResize = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        scaleBackgroundIMG();
    }

    private void scaleBackgroundIMG()
    {

        for(int i=0; i<spritesToResize.Count; i++)
        {
            var sr = spritesToResize[i].GetComponent<SpriteRenderer>();
            if (sr == null) return;

            transform.localScale = new Vector3(1, 1, 1);

            var width = sr.sprite.bounds.size.x;
            var height = sr.sprite.bounds.size.y;

            var worldScreenHeight = Camera.main.orthographicSize * 2.0f;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


            spritesToResize[i].transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);
        }
    }

}
