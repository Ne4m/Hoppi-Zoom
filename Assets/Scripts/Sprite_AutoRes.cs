using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sprite_AutoRes : MonoBehaviour
{

    //public GameObject[] backgroundImage;

    [SerializeField]
    List<GameObject> spritesToResize = new List<GameObject>();

    [SerializeField] private TMP_Text debugtext;

    public static Sprite_AutoRes instance;

    private float fixedAspectRatio = 2.055556f;
    private float currentRatio;
    public float scaleRatio;

    private void Awake()
    {
        instance = this;

        UpdateRatio();
    }

    void Update()
    {
        scaleBackgroundIMG();

        UpdateRatio();
    }

    private void UpdateRatio()
    {
        currentRatio = (float)Screen.height / (float)Screen.width;
        scaleRatio = fixedAspectRatio / currentRatio;


        debugtext.text = $"H:{Screen.currentResolution.height} W:{Screen.currentResolution.width} \n" +
                         $"Current Ratio = {currentRatio}\n" +
                         $"Golden Ratio = {scaleRatio}";
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
