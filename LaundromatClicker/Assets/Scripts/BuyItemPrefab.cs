using UnityEngine;
using UnityEngine.UI;
using NumberSystem;

public class BuyItemPrefab : MonoBehaviour
{
    public ItemTypes itemType;
    public Accelerator.AcceleratorNames accelerator;
    public Automator.AutomatorNames automator;

    private GameState GS;
    private UIManager UIM;

    private Automator thisAutomator;
    private Accelerator thisAccelerator;

    private string totalCost;

    enum CostMultipliers
    {
        ONE = 1,
        FIVE = 5,
        TEN = 10
    }

    public Text cost;
    private int costMultiplier = 1;

    private void OnEnable()
    {
        GS = GameState.instance;
        UIM = UIManager.instance;

        switch (itemType)
        {
            case ItemTypes.AUTOMATOR:
                foreach (Automator a in UIM.automators)
                {
                    if (automator == a.name)
                    {
                        thisAutomator = a;
                    }
                }
                break;
            case ItemTypes.ACCELERATOR:
                foreach (Accelerator a in UIM.accelerators)
                {
                    if (accelerator == a.name)
                    {
                        thisAccelerator = a;
                    }
                }
                break;
        }

        UpdateBuyButton();
    }

    public void BuyButton()
    {
        switch (itemType)
        {
            case ItemTypes.AUTOMATOR:
                switch (thisAutomator.moneyType)
                {
                    case MoneyTypes.MONEY:
                        bool? result = NumberHandler.CompareNumbers(GS.GetMoney(), totalCost);
                        if (result == true || result == null)
                        {
                            MakePurchase();
                        }
                        break;
                    case MoneyTypes.LOOSECHANGE:
                        result = NumberHandler.CompareNumbers(GS.GetLooseChange(), totalCost);
                        if (result == true || result == null)
                        {
                            MakePurchase();
                        }
                        break;
                }
                break;
            case ItemTypes.ACCELERATOR:
                switch (thisAccelerator.moneyType)
                {
                    case MoneyTypes.MONEY:
                        bool? result = NumberHandler.CompareNumbers(GS.GetMoney(), totalCost);
                        if (result == true || result == null)
                        {
                            MakePurchase();
                        }
                        break;
                    case MoneyTypes.LOOSECHANGE:
                        result = NumberHandler.CompareNumbers(GS.GetLooseChange(), totalCost);
                        if (result == true || result == null)
                        {
                            MakePurchase();
                        }
                        break;
                }
                break;
        }
    }

    public void MakePurchase()
    {
        switch (itemType)
        {
            case ItemTypes.AUTOMATOR:
                switch (thisAutomator.moneyType)
                {
                    case MoneyTypes.MONEY:
                        GS.DecreaseMoney(totalCost);
                        GS.AddAutoClick(thisAutomator.numberClicks);
                        thisAutomator.IncreasePrice();
                        break;
                    case MoneyTypes.LOOSECHANGE:
                        GS.DecreaseLooseChange(totalCost);
                        GS.AddAutoClick(thisAutomator.numberClicks);
                        thisAutomator.IncreasePrice();
                        break;
                }
                break;
            case ItemTypes.ACCELERATOR:
                switch (thisAccelerator.moneyType)
                {
                    case MoneyTypes.MONEY:
                        GS.DecreaseMoney(totalCost);
                        GS.AddMoneyPerClick(thisAccelerator.clickAdd);
                        thisAccelerator.IncreasePrice();
                        break;
                    case MoneyTypes.LOOSECHANGE:
                        GS.DecreaseLooseChange(totalCost);
                        GS.AddMoneyPerClick(thisAccelerator.clickAdd);
                        thisAccelerator.IncreasePrice();
                        break;
                }
                break;
        }

        UpdateBuyButton();
    }

    public void AmountInOneButton(Text thisButton)
    {
        switch (costMultiplier)
        {
            case (int)CostMultipliers.ONE:
                costMultiplier = (int)CostMultipliers.FIVE;
                thisButton.text = "x" + costMultiplier;
                break;
            case (int)CostMultipliers.FIVE:
                costMultiplier = (int)CostMultipliers.TEN;
                thisButton.text = "x" + costMultiplier;
                break;
            case (int)CostMultipliers.TEN:
                costMultiplier = (int)CostMultipliers.ONE;
                thisButton.text = "x" + costMultiplier;
                break;
        }

        UpdateBuyButton();
    }

    public void UpdateBuyButton()
    {
        switch (itemType)
        {
            case ItemTypes.AUTOMATOR:
                string automatorCost = thisAutomator.cost;

                for (int i = 0; i < costMultiplier - 1; i++)
                {
                    automatorCost = NumberHandler.IncreaseNumber(automatorCost, thisAutomator.cost);
                }

                cost.text = automatorCost;
                totalCost = automatorCost;
                break;
            case ItemTypes.ACCELERATOR:
                string acceleratorCost = thisAccelerator.cost;

                for (int i = 0; i < costMultiplier - 1; i++)
                {
                    acceleratorCost = NumberHandler.IncreaseNumber(acceleratorCost, thisAccelerator.cost);
                }

                cost.text = acceleratorCost;
                totalCost = acceleratorCost;
                break;
        }
    }
}
