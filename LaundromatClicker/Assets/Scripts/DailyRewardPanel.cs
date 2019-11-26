using UnityEngine;
using UnityEngine.UI;

public class DailyRewardPanel : MonoBehaviour
{
    public Text notifyText;
    public ParticleSystem[] fireworks;

    private Item todaysReward;
    private GameState GS;

    private void Start()
    {
        GS = GameState.instance;

        todaysReward = GS.dailyRewards[GS.currentDailyReward];
    }

    public void DailyRewardButton()
    {
        if(!GS.collectedDailyReward)
        {
            switch (todaysReward.effect)
            {
                case Item.Effect.ADDMONEY:
                    GS.IncreaseMoney(todaysReward.effectStrength);
                    break;
                case Item.Effect.ADDAUTOMATORS:
                    GS.AddAutoClick(todaysReward.effectStrength);
                    break;
                case Item.Effect.ADDLOOSECHANGE:
                    GS.IncreaseLooseChange(todaysReward.effectStrength);
                    break;
            }

            GS.collectedDailyReward = true;
            ++GS.currentDailyReward;

            if(GS.currentDailyReward >= GS.dailyRewards.Length)
            {
                GS.currentDailyReward = 0;
            }

            notifyText.text = "Daily reward collected. Get another one tomorrow!";
            SFXManager.instance.PlayEffect(SoundEffectNames.FANFARETWO);

            foreach (ParticleSystem PS in fireworks)
            {
                PS.Play();
            }
        } else
        {
            notifyText.text = "You have already collected your daily reward!";
            SFXManager.instance.PlayEffect(SoundEffectNames.BUTTON);
        }
    }

    private void OnDisable()
    {
        notifyText.text = "";
    }
}
