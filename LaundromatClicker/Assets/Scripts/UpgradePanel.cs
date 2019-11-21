using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private Text upgradeText;

    private void Awake()
    {
        upgradeText = GetComponent<Text>();
    }

    private void OnEnable()
    {
        upgradeText.text = "If you upgrade all of your currently purchased items and money will be reset and your start click will be worth " + GameState.instance.GetUpgradeStartClick() + " and you will have " + GameState.instance.GetUpgradeAutomators() + " automators.";
    }

    public void UpgradeButton()
    {
        GameState.instance.Upgrade();
    }
}
