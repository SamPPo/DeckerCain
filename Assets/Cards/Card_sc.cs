using System.Collections;
using UnityEngine;
using Decker;

public class Card_sc : EffectContainer_sc
{
    private CardData cardData;
    private int effectIndex;

    public void PlayCard()
    {
        effectIndex = 0;
        var effectPayload = cardData.effectPayloads[effectIndex];
        effectPayload.PlayEffectPayload();
        StartCoroutine(WaitForEffectCompletion());
    }

    IEnumerator WaitForEffectCompletion()
    {
        yield return null;
    }
}
