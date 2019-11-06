using UnityEngine;
using UnityEngine.UI;
using NumberSystem;

public class UIManager : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject menuPanel;
    public GameObject dailyRewardPanel;
    public GameObject settingsPanel;
    public GameObject acceleratorPanel;
    public GameObject moneyPurchasePanel;
    public GameObject socialMediaPanel;
    public GameObject prestigePanel;
    public GameObject automatorPanel;
    public GameObject upgradePanel;

    public Text moneyText;
    public Text looseChangeText;
    public Text moneyPerClickText;

    public int numDecimalPlaces;

    public void Start()
    {
        UpdateMoney("0");
        UpdateLooseChange("0");
    }

    public void UpdateMoney(string money)
    {
        moneyText.text = NumberHandler.FormatNumber(money, numDecimalPlaces);
    }

    public void UpdateLooseChange(string looseChange)
    {
        looseChangeText.text = NumberHandler.FormatNumber(looseChange, numDecimalPlaces);
    }

    public void UpdateMoneyPerClick(int moneyPerClick)
    {
        moneyPerClickText.text = moneyPerClick.ToString();
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
