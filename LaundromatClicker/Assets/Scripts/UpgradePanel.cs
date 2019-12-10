using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public ParticleSystem[] confetti;
    public Text upgradeText;

    //Let the player know what will happen if they upgrade
    private void OnEnable()
    {
        upgradeText.text = "If you upgrade all of your currently purchased items and money will be reset and your start click will be worth " + GameState.instance.GetUpgradeStartClick() + " and you will have " + GameState.instance.GetUpgradeAutomators() + " automators.";
    }

    //Let the player know the upgrade has taken place
    public void UpgradeButton()
    {
        foreach(ParticleSystem PS in confetti)
        {
            PS.Play();
        }
        GameState.instance.Upgrade();
        SFXManager.instance.PlayEffect(SoundEffectNames.FANFARE);
        upgradeText.text = "If you upgrade all of your currently purchased items and money will be reset and your start click will be worth " + GameState.instance.GetUpgradeStartClick() + " and you will have " + GameState.instance.GetUpgradeAutomators() + " automators.";
    }
}
