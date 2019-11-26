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
        SFXManager.instance.PlayEffect(SoundEffectNames.KACHING);
    }

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
