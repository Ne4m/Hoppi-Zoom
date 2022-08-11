using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SetLocale : MonoBehaviour
{
    [SerializeField] private Button selfBtn;

    void Start()
    {
        selfBtn = GetComponent<Button>();

        var locale = selfBtn.transform.name;

        if (selfBtn != null) selfBtn.onClick.AddListener(() =>
        {
            SPrefs.SetString("chosenLanguage", locale);
            I18n.ReloadLanguage();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

}
