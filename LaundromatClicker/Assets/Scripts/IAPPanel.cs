using UnityEngine;
using UnityEngine.UI;

public class IAPPanel : MonoBehaviour
{
    public Text notifyText;
    public int moneyAddedWithPurchase;
    public int looseChangeAddedWithPurchase;

    public void BuyMoneyButton()
    {
        notifyText.text = "Money bought!";
        GameState.instance.IncreaseMoney(moneyAddedWithPurchase);
    }

    public void BuyLooseChangeButton()
    {
        notifyText.text = "Loose change bought!";
        GameState.instance.IncreaseLooseChange(looseChangeAddedWithPurchase);
    }

    private void OnDisable()
    {
        notifyText.text = "";
    }
}
