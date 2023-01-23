using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyGold : MonoBehaviour
{
    Button buyButton;

    [SerializeField] private float price;
    [SerializeField] private int quantity;
    [SerializeField] private int bonus;

    private TMP_Text priceText;
    private TMP_Text quantityText;

    private void Start()
    {
        buyButton = GetComponent<Button>();

        priceText = transform.GetChild(0).GetComponent<TMP_Text>();
        quantityText = transform.GetChild(1).GetComponent<TMP_Text>();

        priceText.text = "$" + price.ToString();
        quantityText.text = quantity.ToString() + (bonus > 0 ? $" + {bonus}" : ""); 

        if (buyButton != null) buyButton.onClick.AddListener(() =>
        {
            Debug.Log($"Clicked {price} Button with bonus of {bonus}");

            var totalQuant = quantity + bonus;
            TransactionManager.instance.MakeTransaction(price, totalQuant);
        } );
    }


}
