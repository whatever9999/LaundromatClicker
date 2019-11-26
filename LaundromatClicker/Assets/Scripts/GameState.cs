using System.Collections;
using UnityEngine;
using NumberSystem;
using System;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public int timeBetweenAutoClicks;
    [HideInInspector]
    public string prestigeScore = "0"; //EMPTY FOR LOAD

    public int prestigeScorePlayedADayEffect = 1000;

    private UIManager UIM;

    private DateTime currentDate;
    private DateTime lastDatePlayed = new DateTime(2019, 11, 24, 12, 00, 00); //EMPTY FOR LOAD
    public Item[] dailyRewards;
    [HideInInspector]
    public bool collectedDailyReward = false; //EMPTY FOR LOAD
    [HideInInspector]
    public int currentDailyReward;

    public GameObject notificationPanel;

    private string money = "0";
    public string GetMoney()
    {
        return money;
    }
    private string looseChange = "0";
    public string GetLooseChange()
    {
        return looseChange;
    }
    private string moneyPerClick = "1";
    private int numAutoClicks = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Get data from save

        UIM = UIManager.instance;

        UIM.UpdateMoney(money);
        UIM.UpdateLooseChange(looseChange);
        UIM.UpdateMoneyPerClick(moneyPerClick);

        StartCoroutine(Automators());

        currentDate = DateTime.Today;
        if(currentDate.Date > lastDatePlayed.Date)
        {
            collectedDailyReward = false;
            notificationPanel.SetActive(true);
            prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, prestigeScorePlayedADayEffect.ToString());
        }
    }

    public IEnumerator Automators()
    {
        for(int i = 0;  i < numAutoClicks; i++)
        {
            money = NumberHandler.IncreaseNumber(money, moneyPerClick);
        }
        UIM.UpdateMoney(money);
        yield return new WaitForSeconds(timeBetweenAutoClicks);
        StartCoroutine(Automators());
    }

    public void IncreaseMoney()
    {
        money = NumberHandler.IncreaseNumber(money, moneyPerClick);
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, moneyPerClick);
        UIM.UpdateMoney(money);
    }

    public void IncreaseMoney(string add)
    {
        money = NumberHandler.IncreaseNumber(money, add);
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, add);
        UIM.UpdateMoney(money);
    }

    public void IncreaseMoney(int add)
    {
        money = NumberHandler.IncreaseNumber(money, add.ToString());
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, add.ToString());
        UIM.UpdateMoney(money);
    }

    public void DecreaseMoney(string takeaway)
    {
        money = NumberHandler.DecreaseNumber(money, takeaway);
        UIM.UpdateMoney(money);
    }

    public void DecreaseMoney(int takeaway)
    {
        money = NumberHandler.DecreaseNumber(money, takeaway.ToString());
        UIM.UpdateMoney(money);
    }

    public void IncreaseLooseChange(string add)
    {
        looseChange = NumberHandler.IncreaseNumber(looseChange, add);
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, add);
        UIM.UpdateLooseChange(looseChange);
    }

    public void IncreaseLooseChange(int add)
    {
        looseChange = NumberHandler.IncreaseNumber(looseChange, add.ToString());
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, add.ToString());
        UIM.UpdateLooseChange(looseChange);
    }

    public void DecreaseLooseChange(string takeaway)
    {
        looseChange = NumberHandler.DecreaseNumber(looseChange, takeaway);
        UIM.UpdateLooseChange(looseChange);
    }

    public void DecreaseLooseChange(int takeaway)
    {
        looseChange = NumberHandler.DecreaseNumber(looseChange, takeaway.ToString());
        UIM.UpdateLooseChange(looseChange);
    }

    public void AddMoneyPerClick(int amount)
    {
        moneyPerClick = NumberHandler.IncreaseNumber(moneyPerClick, amount.ToString());
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, amount.ToString());
        UIM.UpdateMoneyPerClick(moneyPerClick);
    }

    public void AddMoneyPerClick(string amount)
    {
        moneyPerClick = NumberHandler.IncreaseNumber(moneyPerClick, amount);
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, amount);
        UIM.UpdateMoneyPerClick(moneyPerClick);
    }

    public void AddAutoClick(int amount)
    {
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, (amount * 100).ToString());
        numAutoClicks += amount;
    }

    public int GetUpgradeStartClick()
    {
        return money.Length * money.Length;
    }

    public int GetUpgradeAutomators()
    {
        return money.Length;
    }

    public void Upgrade()
    {
        moneyPerClick = (money.Length * money.Length).ToString();
        numAutoClicks = money.Length;
        money = "0";
        UIM.ResetPurchases();
        UIM.UpdateMoneyPerClick(moneyPerClick);
        UIM.UpdateMoney(money);
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, moneyPerClick);
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, numAutoClicks.ToString());
    }
}
