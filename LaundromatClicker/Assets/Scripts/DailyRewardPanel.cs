using UnityEngine;
using UnityEngine.UI;

public class DailyRewardPanel : MonoBehaviour
{
    public ParticleSystem[] fireworks;
    public Text notifyText;

    private GameState GS;

    private Item todaysReward;

    private void Start()
    {
        GS = GameState.instance;

        //Update the daily reward according to the game state
        todaysReward = GS.dailyRewards[GS.currentDailyReward];
    }

    public void DailyRewardButton()
    {
        //If the player hasn't collected their daily reward yet
        if(!GS.collectedDailyReward)
        {
            //Provide them with the results of the reward according to its effect
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

            //Let the game state know they have collected it
            //And increment the reward counter so that tomorrow the reward will be different
            GS.collectedDailyReward = true;
            ++GS.currentDailyReward;
            if(GS.currentDailyReward >= GS.dailyRewards.Length)
            {
                GS.currentDailyReward = 0;
            }

            //Let them know that collecting the reward was successful
            notifyText.text = "Daily reward collected. Get another one tomorrow!";
            SFXManager.instance.PlayEffect(SoundEffectNames.FANFARETWO);

            foreach (ParticleSystem PS in fireworks)
            {
                PS.Play();
            }
        } else
        {
            //Remind the player they have already collected their daily reward
            notifyText.text = "You have already collected your daily reward!";
            SFXManager.instance.PlayEffect(SoundEffectNames.BUTTON);
        }
    }

    private void OnDisable()
    {
        notifyText.text = "";
    }
}
