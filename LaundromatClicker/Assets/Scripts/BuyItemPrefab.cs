using UnityEngine;
using UnityEngine.UI;
using NumberSystem;

public class BuyItemPrefab : MonoBehaviour
{
    public ItemTypes itemType;
    public Accelerator.AcceleratorNames accelerator;
    public Automator.AutomatorNames automator;
    public Text cost;

    private GameState GS;
    private UIManager UIM;

    private Automator thisAutomator;
    private Accelerator thisAccelerator;

    private string totalCost;
    private int costMultiplier = 1;

    //So the player can buy purchasables in bulk
    enum CostMultipliers
    {
        ONE = 1,
        FIVE = 5,
        TEN = 10
    }

    //The prefab finds the purchasable linked to it and updates the buy button to reflect it
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
        //If the item is an automator or accelerator
        switch (itemType)
        {
            case ItemTypes.AUTOMATOR:
                switch (thisAutomator.moneyType)
                {
                    //If it is bought with money or loose change
                    case MoneyTypes.MONEY:
                        //Check if there is enough money
                        bool? result = NumberHandler.CompareNumbers(GS.GetMoney(), totalCost);
                        //If there is then purchase the item
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
        //If the item is an automator or accelerator
        switch (itemType)
        {
            case ItemTypes.AUTOMATOR:
                //If it is bought with money or loose change
                switch (thisAutomator.moneyType)
                {
                    case MoneyTypes.MONEY:
                        //Take away the cost from the player's money and make the effect of the item take place
                        //Then increase the price of the item (in order to make it still purchasable)
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
        SFXManager.instance.PlayEffect(SoundEffectNames.DINGONE);
    }

    //Update the button that shows the bulk number of items the player is buying
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

    //Make the button show how much the item costs (including multipliers if it's being bought in bulk)
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
