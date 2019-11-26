using UnityEngine;
using UnityEngine.UI;

public class PrestigeScorePanel : MonoBehaviour
{
    public Text prestigeText;

    private void OnEnable()
    {
        prestigeText.text = "Your Prestige Score: " + NumberSystem.NumberHandler.FormatNumber(GameState.instance.prestigeScore, 2);
    }
}
