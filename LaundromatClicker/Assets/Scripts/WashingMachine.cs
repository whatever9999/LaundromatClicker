using UnityEngine;

public class WashingMachine : MonoBehaviour
{
    public AnimationClip animationClip;

    private ParticleSystem[] particleEffects;
    private Animator animator;

    private bool beingClickedOn = false;
    private float clickIntervalToStopAnimating;
    private float currentClickInterval = 0;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        particleEffects = GetComponentsInChildren<ParticleSystem>();
        clickIntervalToStopAnimating = animationClip.length;
    }

    public void Update()
    {
        //If the mashine is being clicked on then animate it
        //Stop animating it when it is not being clicked on
        if(beingClickedOn)
        {
            currentClickInterval += Time.deltaTime;
            animator.SetBool("BeingClickedOn", true);
        }

        if(currentClickInterval > clickIntervalToStopAnimating)
        {
            beingClickedOn = false;
            animator.SetBool("BeingClickedOn", false);
            currentClickInterval = 0;
        }
    }

    public void OnMouseDown()
    {
        //Animate and use particle effects when clicked on
        beingClickedOn = true;
        foreach (ParticleSystem PS in particleEffects)
        {
            PS.Play();
        }
        SFXManager.instance.PlayEffect(SoundEffectNames.BUBBLE);
        GameState.instance.IncreaseMoney();
    }
}
