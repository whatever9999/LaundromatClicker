using UnityEngine;
using UnityEngine.UI;

public class SocialMediaPanel : MonoBehaviour
{
    public Text notifyText;

    public void ShareButton()
    {
        notifyText.text = "Sharing Successful!";
    }

    private void OnDisable()
    {
        notifyText.text = "";
    }
}
