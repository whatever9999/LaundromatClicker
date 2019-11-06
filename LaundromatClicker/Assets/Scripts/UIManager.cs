using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject helpPanel;
    public Text moneyText;
    public Text looseChangeText;

    public void Start()
    {
        Debug.Log("START");
        UpdateLooseChange(123);
        Debug.Log("FIRST");
        UpdateMoney(125444323432);
        Debug.Log("SECOND");
    }

    public void HelpButton()
    {
        helpPanel.SetActive(true);
    }

    public void HelpExitButton()
    {
        helpPanel.SetActive(false);
    }

    public void UpdateMoney(BigInteger money)
    {
        moneyText.text = NumWordsWrapper(money);
    }

    public void UpdateLooseChange(BigInteger looseChange)
    {
        looseChangeText.text = NumWordsWrapper(looseChange);
    }

    static string NumWordsWrapper(BigInteger n)
    {
        Debug.Log("NUMWORDS");
        if (n == 0)
            return "0";

        string words = FormatNumber(n);
        return words;
    }

    static string FormatNumber(BigInteger n)
    {
        Debug.Log("FORMATNUM");
        string[] units = { " million", " billion", " trillion", " quadrillion", " quintillion", " sextillion", " septillion", " octillion", " nonillion", " decillion", " undecillion", " duodecillion", " tredecillion", " quattuordecillion", " quindecillion", " sexdecillion", " septendecillion", " octodecillion", " novemdecillion", " vigintillion", " unvigintillion", " duovigintillion", " tresvigintillion", " quattuorvigintillion", " quinquavigintillion", " sesvigintillion", " septemvigintillion", " octovigintillion", " novemvigintillion", " trigintillion", " untrigintillion", " duotrigintillion", " duotrigintillion", " trestrigintillion", " quattuortrigintillion", " quinquatrigintillion", " sestrigintillion", " septentrigintillion", " octotrigintillion", " noventrigintillion", " quadragintillion" };

        bool highnumber = false;
        int bignumber = 1000000;
        int unitIndex = -1;
        string unit = "";

        if (n >= bignumber)
        {
            highnumber = true;

            while (n >= bignumber)
            {
                bignumber *= 1000; unitIndex++;
            }

            n /= (bignumber / 1000);
            unit = units[unitIndex];
        }

        float floatN = (float)(double)n;

        if (unit == "") {
            return floatN.ToString();
        }

        if (highnumber == true)
        {
            floatN = Mathf.Round(floatN * 100f) / 100f;
        }

        return floatN.ToString() + unit;
    }
}
