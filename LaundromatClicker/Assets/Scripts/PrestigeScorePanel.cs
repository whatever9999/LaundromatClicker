using UnityEngine;
using UnityEngine.UI;

public class PrestigeScorePanel : MonoBehaviour
{
    public Text prestigeText;

    //Tell the player their prestige score
    private void OnEnable()
    {
        prestigeText.text = "Your Prestige Score: " + NumberSystem.NumberHandler.FormatNumber(GameState.instance.prestigeScore, 2);
    }
}
