using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour
{
    public AudioMixer audioMixer;
    private bool musicOn = true;

    public void SFXButton(Image thisButton)
    {
        SFXManager.instance.ToggleSFX();
        ColourChange(thisButton);
    }

    public void MusicButton(Image thisButton)
    {
        ToggleMusic();

        if(musicOn)
        {
            audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { audioMixer.FindSnapshot("MusicOn") }, new float[] { 100f }, 0);
        } else
        {
            audioMixer.TransitionToSnapshots(new AudioMixerSnapshot[] { audioMixer.FindSnapshot("MusicOff") }, new float[] { 100f }, 0);
        }
        
        ColourChange(thisButton);
    }

    private void ToggleMusic()
    {
        musicOn = !musicOn;
    }

    private void ColourChange(Image thisButton)
    {
        if (thisButton.color == Color.white)
        {
            thisButton.color = Color.black;
        }
        else
        {
            thisButton.color = Color.white;
        }
    }
}
