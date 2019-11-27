using UnityEngine;
using UnityEngine.UI;
using NumberSystem;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject[] purchasePanels;
    public Automator[] automators;
    public Accelerator[] accelerators;
    public Text moneyText;
    public Text looseChangeText;
    public Text moneyPerClickText;
    public Text randomItemText;

    public int numDecimalPlaces;

    private BuyItemPrefab[] buttons;

    //Set the automator cost to the start cost (In awake so that if there is save data the start data will be written over)
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

    //Get the button prefabs from the scene so that when panels are reset they can be reset too
    private void Start()
    {
        foreach (GameObject go in purchasePanels)
        {
            go.SetActive(true);
        }

        buttons = GameObject.FindObjectsOfType<BuyItemPrefab>();

        foreach(GameObject go in purchasePanels)
        {
            go.SetActive(false);
        }
    }

    //Reset purchasables and update buttons
    public void ResetPurchases()
    {

        foreach (Automator a in automators)
        {
            a.cost = a.startCost.ToString();
            a.numberClicks = a.startNumberClicks;
        }
        foreach (Accelerator a in accelerators)
        {
            a.cost = a.startCost.ToString();
            a.numberClicks = a.startNumberClicks;
        }

        foreach(BuyItemPrefab b in buttons)
        {
            b.UpdateBuyButton();
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
        SFXManager.instance.PlayEffect(SoundEffectNames.BUTTON);
        panel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        SFXManager.instance.PlayEffect(SoundEffectNames.BUTTON);
        panel.SetActive(true);
    }

    //The coroutine needs to be started in the UIManager as if it is started by the random item it will stop running when the random item is disabled
    public void ChangeRandomItemText(string newText, float timeOnScreen)
    {
        StartCoroutine(RandomItemText(newText, timeOnScreen));
    }

    public IEnumerator RandomItemText(string newText, float timeOnScreen)
    {
        randomItemText.text = newText;
        yield return new WaitForSeconds(timeOnScreen);
        randomItemText.text = "";
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
    public MoneyTypes moneyType;

    public int clickAdd;
    public int startNumberClicks;
    public int startCost;
    public int costIncrease;

    [HideInInspector]
    public int numberClicks;
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
    public MoneyTypes moneyType;

    public int clickAdd;
    public int startNumberClicks;
    public int startCost;
    public int costIncrease;

    [HideInInspector]
    public int numberClicks;
    [HideInInspector]
    public string cost;

    public void IncreasePrice()
    {
        cost = NumberHandler.IncreaseNumber(cost, costIncrease.ToString());
    }
}

//Data to be saved
[System.Serializable]
public class PurchasesData
{
    public Automator[] automators;
    public Accelerator[] accelerators;

    public PurchasesData()
    {
        automators = UIManager.instance.automators;
        accelerators = UIManager.instance.accelerators;
    }
}