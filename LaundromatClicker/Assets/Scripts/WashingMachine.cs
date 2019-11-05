using UnityEngine;

public class WashingMachine : MonoBehaviour
{
    public AnimationClip animationClip;

    private Animator animator;
    private ParticleSystem clothesParticleEffect;

    private bool beingClickedOn = false;
    private float clickIntervalToStopAnimating;
    private float currentClickInterval = 0;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        clothesParticleEffect = GetComponentInChildren<ParticleSystem>();
        clickIntervalToStopAnimating = animationClip.length;
    }

    public void Update()
    {
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
        beingClickedOn = true;
        clothesParticleEffect.Play();
    }
}
