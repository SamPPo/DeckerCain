using System.Linq;
using UnityEngine;
using Decker;
using NUnit.Framework;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AddCards_ELSO", menuName = "EffectLogic/AddCards_ELSO")]
public class AddCards_ELSO : EffectLogic_SO
{
    private List<KeyValuePair<Card_SO, GameObject>> spawnedCards;

    protected override void PlayEffect()
    {
        ThisTriggers = Trigger.OnCardAdd;
        var targets = GetTargets();
        if (targets.Any())
        {
            spawnedCards = new();
            foreach (var t in targets)
            {
                foreach (var c in newCards)
                {
                    var newCard = GameMaster_sc.InstantiateAndInitializePresetCard(c, t);
                    spawnedCards.Add(new KeyValuePair<Card_SO, GameObject>(newCard, t));
                    t.GetComponent<Character_sc>().AddCardToDeck(newCard);
                }
            }
        }
    }

    protected override void PlayFinish()
    {
        ThisTriggers = Trigger.OnCardAdd;
        var targets = GetTargets();
        if (targets.Any())
        {
            foreach (var t in targets)
            {
                foreach (var c in newCards)
                {
                    var newCard = GameMaster_sc.InstantiateAndInitializePresetCard(c, t);
                    spawnedCards.Add(new KeyValuePair<Card_SO, GameObject>(newCard, t));
                    t.GetComponent<Character_sc>().AddCardToDeck(newCard);
                }
            }
        }
    }
}
