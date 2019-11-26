using UnityEngine;
using UnityEngine.UI;

public class PrestigeScorePanel : MonoBehaviour
{
    public Text prestigeText;

    private void OnEnable()
    {
        prestigeText.text = "Your Prestige Score: " + GameState.instance.prestigeScore;
    }
}
