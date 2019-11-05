using UnityEngine;
using UnityEngine.Audio;

public enum SoundEffectNames
{
    DIG,
    PICKUPCOIN,
    INVAN,
    TIMERBEEP,
    SKELETONFROMGRAVE,
    OOF
}

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public SoundEffect[] soundEffects;

    public GameObject SFXPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(instance);
        }
    }

    /*
     * When a sound effect is played the enum name for it is passed in
     * This is used in a for loop to find it from the list of SFXs available
     * A SFX prefab (AudioSource) is then spawned and its clip is set to the correct clip
     * The clip is then played and the prefab is destroyed once the clip has finished
     */
    public void PlayEffect(SoundEffectNames name)
    {
        for(int i = 0; i < soundEffects.Length + 1; i++)
        {
            if(soundEffects[i].name == name)
            {
                GameObject currentSFX = Instantiate(SFXPrefab);
                AudioSource currentAS = currentSFX.GetComponent<AudioSource>();

                currentAS.clip = soundEffects[i].clip;
                currentAS.Play();

                Destroy(currentSFX, currentAS.clip.length);
                
                break;
            }
        }
    }
}

[System.Serializable]
public class SoundEffect
{
    public SoundEffectNames name;
    public AudioClip clip;
}