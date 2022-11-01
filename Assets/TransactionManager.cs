using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionManager : MonoBehaviour
{

    public static TransactionManager instance;

    private ShopMenuController controller;

    private void Awake()
    {
        instance = this;

        TryGetComponent(out ShopMenuController ctrl);
        controller = ctrl;
    }

    
    public void MakeTransaction(float price, int quantity)
    {
        if (CheckTransaction(price))
        {
            int currency = SPrefs.GetInt("gameCurrency", 0);
            currency += quantity;
            SPrefs.SetInt("gameCurrency", currency);
            SPrefs.Save();

            controller.UpdateText();
        }
    }

    private bool CheckTransaction(float price)
    {
        if(price <= 0)
        {
            return false;
        }

        return true;
    }
}
