using UnityEngine;
using UnityEngine.UI;
using NumberSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Automator[] automators;
    public Accelerator[] accelerators;
    public Text moneyText;
    public Text looseChangeText;
    public Text moneyPerClickText;

    public int numDecimalPlaces;

    private void Awake()
    {
        instance = this;

        foreach (Automator a in automators)
        {
            a.cost = a.startCost.ToString();
        }
        foreach (Accelerator a in accelerators)
        {
            a.cost = a.startCost.ToString();
        }
    }

    public void UpdateMoney(string money)
    {
        moneyText.text = NumberHandler.FormatNumber(money, numDecimalPlaces);
    }

    public void UpdateLooseChange(string looseChange)
    {
        looseChangeText.text = NumberHandler.FormatNumber(looseChange, numDecimalPlaces);
    }

    public void UpdateMoneyPerClick(string moneyPerClick)
    {
        moneyPerClickText.text = NumberHandler.FormatNumber(moneyPerClick, numDecimalPlaces) + " / click";
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
}

public enum MoneyTypes
{
    MONEY,
    LOOSECHANGE
}

public enum ItemTypes
{
    AUTOMATOR,
    ACCELERATOR
}

[System.Serializable]
public class Automator
{
    public enum AutomatorNames
    {
        STAFF,
        WASHERS,
        DRIERS,
        NONE
    }

    public AutomatorNames name;
    public int numberClicks;

    public MoneyTypes moneyType;
    public int startCost;
    public int costIncrease;
    [HideInInspector]
    public string cost;

    public void IncreasePrice()
    {
        cost = NumberHandler.IncreaseNumber(cost, costIncrease.ToString());
    }
}

[System.Serializable]
public class Accelerator
{
    public enum AcceleratorNames
    {
        BETTERMACHINES,
        BETTERSTAFF,
        BIGGERBUILDING,
        DETERGENT,
        FABRICSOFTNER,
        COLLECTANDDELIVER,
        BIGGERLAUNDRYBASKETS,
        CLEANINGCREW,
        NONE
    }

    public AcceleratorNames name;
    public int clickAdd;

    public MoneyTypes moneyType;
    public int startCost;
    public int costIncrease;
    [HideInInspector]
    public string cost;

    public void IncreasePrice()
    {
        cost = NumberHandler.IncreaseNumber(cost, costIncrease.ToString());
    }
}
