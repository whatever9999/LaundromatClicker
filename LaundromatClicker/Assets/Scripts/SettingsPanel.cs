using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Color buttonEnabled;
    public Color buttonDisabled;

    private bool musicOn = true;

    //Turn the SFX on and off (stops sound effect prefabs from spawning)
    public void SFXButton(Image thisButton)
    {
        SFXManager.instance.ToggleSFX();
        ColourChange(thisButton);
    }

    //Use a snapshot to turn music and ambience on/off
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
        if (thisButton.color == buttonEnabled)
        {
            thisButton.color = buttonDisabled;
            
        }
        else
        {
            thisButton.color = buttonEnabled;
        }
    }
}
