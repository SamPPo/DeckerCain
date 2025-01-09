using System.Collections;
using UnityEngine;
using Decker;

public class Card_sc : EffectContainer_sc
{
    private CardData cardData;
    private int effectIndex;

    public void SetEffectData(CardData e) 
    { 
        this.cardData = e;
        foreach (var f in e.effectPayloads)
        {
            f.SetData();
        }
    }

    public void PlayCard()
    {
        effectIndex = 0;
        var effectPayload = cardData.effectPayloads[effectIndex];
        effectPayload.effect.PlayEffect();
        StartCoroutine(WaitForEffectCompletion());
    }

    IEnumerator WaitForEffectCompletion()
    {
        yield return null;
    }
}
