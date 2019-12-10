using UnityEngine;
using System.Collections;

public class RandomItem : MonoBehaviour
{
    public Item[] items;

    public float timeToTellUserEffect;
    public float timeBetweenFlashes = 0.45f;
    public float timeToWaitToFlash = 2;

    private GameState GS;

    private SpriteRenderer SR;

    private Item thisItem;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GS = GameState.instance;
    }

    private IEnumerator Flash()
    {
        yield return new WaitForSeconds(timeToWaitToFlash);
        while(enabled)
        {
            SR.enabled = false;
            yield return new WaitForSeconds(timeBetweenFlashes);
            SR.enabled = true;
            yield return new WaitForSeconds(timeBetweenFlashes);
        }
    }

    //When the player clicks on the box its effect occurs
    private void OnMouseDown()
    {
        Table.instance.timerRunning = true;

        switch (thisItem.effect)
        {
            case Item.Effect.ADDMONEY:
                GS.IncreaseMoney(thisItem.effectStrength);
                UIManager.instance.ChangeRandomItemText("+ " + thisItem.effectStrength + " money", timeToTellUserEffect);
                break;
            case Item.Effect.ADDAUTOMATORS:
                GS.AddAutoClick(thisItem.effectStrength);
                UIManager.instance.ChangeRandomItemText("+ " + thisItem.effectStrength + " automators", timeToTellUserEffect);
                break;
            case Item.Effect.ADDLOOSECHANGE:
                GS.IncreaseLooseChange(thisItem.effectStrength);
                UIManager.instance.ChangeRandomItemText("+ " + thisItem.effectStrength + " loose change", timeToTellUserEffect);
                break;
        }

        SFXManager.instance.PlayEffect(SoundEffectNames.DINGTHREE);

        gameObject.SetActive(false);
    }

    //When the box is enabled it gets an item type
    private void OnEnable()
    {
        StartCoroutine(Flash());
        int rand = Random.Range(0, items.Length);

        foreach (Item i in items)
        {
            if ((int)i.effect == rand)
            {
                thisItem = i;
            }
        }
    }
}

[System.Serializable]
public class Item
{
    public enum Effect
    {
        ADDMONEY,
        ADDAUTOMATORS,
        ADDLOOSECHANGE
    }

    public Effect effect;

    public int effectStrength;
}