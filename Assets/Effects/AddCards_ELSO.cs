using System.Linq;
using UnityEngine;
using Decker;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "AddCards_ELSO", menuName = "EffectLogic/AddCards_ELSO")]
public class AddCards_ELSO : EffectLogic_SO
{
    protected override void SetText()
    {
        string cards = "";
        int i = 0;
        foreach (var c in newCards)
        {
            if (i < newCards.Count - 2)
                cards += c.displayName + ", ";
            else if (i == newCards.Count - 2)
                cards += c.displayName + " and ";
            else
                cards += c.displayName;
            i++;
        }
        text = "<b>Add</b> " + cards + " to target's deck";
    }

    protected override void PrePlayEffectInherited()
    {
        int i = 0;
        activationCountDelta *= newCards.Count;
        foreach (var t in targets)
        {
            foreach (var c in newCards)
            {
                var newCard = GameMaster_sc.InstantiateAndInitializePresetCard(c, t);
                newCard.InstantiateCardPfabToTransform(ownerContainer.GetPfabTransform(), true, new(Pvsc.cardExtents.x + (i * (0.85f * Pvsc.cardExtents.x)), i * Pvsc.cardExtents.y, i * Pvsc.cardExtents.y));
                newCard.MoveCardToPile(CardPile.Deck, triggerWaitTime, 0.7f);
                i++;
            }
        }
    }

    protected override void PlayEffectInherited(int loopCount, GameObject target)
    {
        int i = loopCount % newCards.Count;
        var c = newCards[i];
        var newCard = GameMaster_sc.InstantiateAndInitializePresetCard(c, target);
        target.GetComponent<Character_sc>().AddCardToDeck(newCard);
    }
}
