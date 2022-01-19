using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static int money;
    public static MoneyManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        GetMoney();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static int GetMoney()
    {
        money = PlayerPrefs.GetInt("money", 0);

        if (CanvasManager.instance != null)
            CanvasManager.instance.touristCounter.SetText(money.ToString());


        return money;
    }

    public static void SetMoney(int newMoney)
    {
        PlayerPrefs.SetInt("money", newMoney);
        money = GetMoney();
    }

    public static void Increment(int newMoney)
    {
        PlayerPrefs.SetInt("money", (money + newMoney));
        money = GetMoney();
    }
    public static void Decrement(int spendMoney)
    {
        PlayerPrefs.SetInt("money", (money - spendMoney));
        money = GetMoney();
    }
}
