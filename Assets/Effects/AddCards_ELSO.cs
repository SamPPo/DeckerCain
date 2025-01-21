using System.Linq;
using UnityEngine;
using Decker;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "AddCards_ELSO", menuName = "EffectLogic/AddCards_ELSO")]
public class AddCards_ELSO : EffectLogic_SO
{
    private List<KeyValuePair<Card_SO, GameObject>> spawnedCards;

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
        text = "Add " + cards + " to target's deck";
    }

    protected override void PlayEffect()
    {
        ThisTriggers = Trigger.OnCardAdd;
        var targets = GetTargets();
        if (targets.Any())
        {
            int i = 0;
            spawnedCards = new();
            foreach (var t in targets)
            {
                foreach (var c in newCards)
                {
                    var newCard = GameMaster_sc.InstantiateAndInitializePresetCard(c, t);
                    spawnedCards.Add(new KeyValuePair<Card_SO, GameObject>(newCard, t));
                    newCard.InstantiateCardPfabToTransform(ownerContainer.GetPfabTransform(), true, new(Pvsc.cardExtents.x + (i * (0.85f * Pvsc.cardExtents.x)), i * Pvsc.cardExtents.y, i * Pvsc.cardExtents.y));
                    newCard.MoveCardToPile(CardPile.Deck, triggerWaitTime, 0.7f);
                    i++;
                }
            }
        }
    }

    protected override void PlayFinish()
    {
        foreach (var kvp in spawnedCards)
        {
            Card_SO card = kvp.Key;
            GameObject target = kvp.Value;
            target.GetComponent<Character_sc>().AddCardToDeck(card);
        }
    }
}
