using UnityEngine;
using UnityEngine.UI;

public class SocialMediaPanel : MonoBehaviour
{
    public Text notifyText;

    //Let the player know they have successfully shared on social media
    public void ShareButton()
    {
        SFXManager.instance.PlayEffect(SoundEffectNames.DINGTWO);
        notifyText.text = "Sharing Successful!";
    }

    private void OnDisable()
    {
        notifyText.text = "";
    }
}
