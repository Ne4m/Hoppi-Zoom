using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private SpriteRenderer sr;

    public Sprite[] characterSkins;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        characterSkins = Resources.LoadAll<Sprite>("Characters");

        sr.sprite = characterSkins[PlayerPrefs.GetInt("LastSelectedCharacterIndex", 0)];
    }

}
