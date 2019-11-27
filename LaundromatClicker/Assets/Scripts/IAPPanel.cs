using UnityEngine;
using UnityEngine.UI;

public class IAPPanel : MonoBehaviour
{
    public Text notifyText;

    public int moneyAddedWithPurchase;
    public int looseChangeAddedWithPurchase;

    //Let the player know they have successfully made a purchase
    public void BuyMoneyButton()
    {
        notifyText.text = "Money bought!";
        GameState.instance.IncreaseMoney(moneyAddedWithPurchase);
        SFXManager.instance.PlayEffect(SoundEffectNames.KACHING);
    }

    //Let the player know they have successfully made a purchase
    public void BuyLooseChangeButton()
    {
        notifyText.text = "Loose change bought!";
        GameState.instance.IncreaseLooseChange(looseChangeAddedWithPurchase);
        SFXManager.instance.PlayEffect(SoundEffectNames.KACHING);
    }

    private void OnDisable()
    {
        notifyText.text = "";
    }
}
