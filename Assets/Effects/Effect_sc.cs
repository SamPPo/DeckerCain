using Decker;
using System.Collections.Generic;
using UnityEngine;

public class Effect_sc
{
    private int magnitude;
    private Targetting target;
    private List<Card_SO> newCards;

    public void SetEffectData(int m, Targetting t)
    {
        magnitude = m;
        target = t;
    }

    public void SetNewCardsToAddData(List<Card_SO> c)
    {
        newCards = c;
    }

    public void PlayEffect()
    {
        DebugThing();
    }

    private void DebugThing()
    {
        Debug.Log("Deal " +magnitude+ " damage to " +target);
    }
}
