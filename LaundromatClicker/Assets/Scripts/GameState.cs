using System.Collections;
using UnityEngine;
using NumberSystem;

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public int timeBetweenAutoClicks;

    private UIManager UIM;

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
        UIM = UIManager.instance;

        UIM.UpdateMoney(money);
        UIM.UpdateLooseChange(looseChange);
        UIM.UpdateMoneyPerClick(moneyPerClick);

        StartCoroutine(Automators());
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
        UIM.UpdateMoney(money);
    }

    public void IncreaseMoney(string add)
    {
        money = NumberHandler.IncreaseNumber(money, add);
        UIM.UpdateMoney(money);
    }

    public void IncreaseMoney(int add)
    {
        money = NumberHandler.IncreaseNumber(money, add.ToString());
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
        money = NumberHandler.IncreaseNumber(looseChange, add);
        UIM.UpdateLooseChange(money);
    }

    public void IncreaseLooseChange(int add)
    {
        money = NumberHandler.IncreaseNumber(looseChange, add.ToString());
        UIM.UpdateLooseChange(money);
    }

    public void DecreaseLooseChange(string takeaway)
    {
        money = NumberHandler.DecreaseNumber(looseChange, takeaway);
        UIM.UpdateLooseChange(money);
    }

    public void DecreaseLooseChange(int takeaway)
    {
        money = NumberHandler.DecreaseNumber(looseChange, takeaway.ToString());
        UIM.UpdateLooseChange(money);
    }

    public void AddMoneyPerClick(int amount)
    {
        moneyPerClick = NumberHandler.IncreaseNumber(moneyPerClick, amount.ToString());
        UIM.UpdateMoneyPerClick(moneyPerClick);
    }

    public void AddMoneyPerClick(string amount)
    {
        moneyPerClick = NumberHandler.IncreaseNumber(moneyPerClick, amount);
        UIM.UpdateMoneyPerClick(moneyPerClick);
    }

    public void AddAutoClick(int amount)
    {
        numAutoClicks += amount;
    }
}
