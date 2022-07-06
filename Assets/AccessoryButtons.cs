using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccessoryButtons : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private TMP_Text description;

    private Image characterHead;
    private Image characterBody;
    private Button applyButton;

    public bool isHat;

    private void Start()
    {
        applyButton = GetComponent<Button>();

        characterHead = character.transform.GetChild(0).GetComponent<Image>();
        characterBody = character.transform.GetChild(1).GetComponent<Image>();

        if (applyButton != null) applyButton.onClick.AddListener(ButtonClicked);
    }


    private void ButtonClicked()
    {
        string spriteName = applyButton.GetComponent<Image>().sprite.name;

        if (isHat)
        {
            characterHead.color = new Color32(255, 255, 255, 255);
            characterHead.sprite = Resources.Load<Sprite>($"Accessories/Hats/{spriteName}");

            characterHead.GetComponent<RectTransform>().pivot = characterHead.sprite.pivot / characterHead.sprite.rect.size;

            description.text = spriteName.ToUpper();

            PlayerPrefs.SetString("PlayerHat", spriteName);
        }
        else
        {
            characterBody.color = new Color32(255, 255, 255, 255);
            characterBody.sprite = Resources.Load<Sprite>($"Accessories/Body/{spriteName}");

            characterBody.GetComponent<RectTransform>().pivot = characterBody.sprite.pivot / characterBody.sprite.rect.size;

            description.text = spriteName.ToUpper();

            PlayerPrefs.SetString("PlayerBody", spriteName);
        }

        PlayerPrefs.Save();
    }

}
