using System.Collections;
using UnityEngine;
using NumberSystem;
using System;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public Item[] dailyRewards;
    public GameObject notificationPanel;

    public int timeBetweenAutoClicks;
    public int prestigeScorePlayedADayEffect = 1000;

    [HideInInspector]
    public string prestigeScore = "0";
    [HideInInspector]
    public DateTime lastDatePlayed = new DateTime(2019, 11, 24, 12, 00, 00);
    [HideInInspector]
    public bool collectedDailyReward = false;
    [HideInInspector]
    public int currentDailyReward;
    [HideInInspector]
    public string moneyPerClick = "1";
    [HideInInspector]
    public int numAutoClicks = 0;

    private UIManager UIM;

    private DateTime currentDate;
    private string money = "0";
    private string looseChange = "0";


    public string GetMoney() { return money; }
    public void SetMoney(string newMoney) { money = newMoney; }
    public string GetLooseChange() { return looseChange; }
    public void SetLooseChange(string newLooseChange) { looseChange = newLooseChange; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Get data from save
        SavingAndLoading.instance.LoadGame();

        UIM = UIManager.instance;

        //Update the UI
        UIM.UpdateMoney(money);
        UIM.UpdateLooseChange(looseChange);
        UIM.UpdateMoneyPerClick(moneyPerClick);
        UIM.UpdateAutoClicks(numAutoClicks);

        //Start automators running
        StartCoroutine(Automators());

        //Check if the player has played the game yet today
        currentDate = DateTime.Today;
        if (currentDate.Date > lastDatePlayed.Date)
        {
            collectedDailyReward = false;
            lastDatePlayed = currentDate;
            prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, prestigeScorePlayedADayEffect.ToString());
        }

        //Let the player know to collect their daily reward if they haven't yet
        if(!collectedDailyReward)
        {
            notificationPanel.SetActive(true);
        }
    }

    //Make each autoClick earn the user money on a timer
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
        UIM.UpdateAutoClicks(numAutoClicks);
    }

    //The amount the player's click will be worth if they upgrade;
    public int GetUpgradeStartClick()
    {
        return money.Length * money.Length * money.Length * money.Length;
    }

    //The number of automators the player will have if they upgrade
    public int GetUpgradeAutomators()
    {
        return money.Length * money.Length;
    }

    //Set the player's click and autoClicks to the upgrade amount
    //Reset all other variables (including purchases)
    //Update the UI
    public void Upgrade()
    {
        moneyPerClick = GetUpgradeStartClick().ToString();
        numAutoClicks = GetUpgradeAutomators();
        money = "0";
        UIM.ResetPurchases();
        UIM.UpdateMoneyPerClick(moneyPerClick);
        UIM.UpdateMoney(money);
        UIM.UpdateAutoClicks(numAutoClicks);
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, moneyPerClick);
        prestigeScore = NumberHandler.IncreaseNumber(prestigeScore, numAutoClicks.ToString());
    }
}

//Data to be saved
[Serializable]
public class GameStateData
{
    public string prestigeScore;
    public DateTime lastDatePlayed;
    public bool collectedDailyReward;
    public string money;
    public string looseChange;
    public string moneyPerClick;
    public int numAutoClicks;
    public int currentDailyReward;

    public GameStateData()
    {
        prestigeScore = GameState.instance.prestigeScore;
        lastDatePlayed = GameState.instance.lastDatePlayed;
        collectedDailyReward = GameState.instance.collectedDailyReward;
        money = GameState.instance.GetMoney();
        looseChange = GameState.instance.GetLooseChange();
        moneyPerClick = GameState.instance.moneyPerClick;
        numAutoClicks = GameState.instance.numAutoClicks;
        currentDailyReward = GameState.instance.currentDailyReward;
    }
}
