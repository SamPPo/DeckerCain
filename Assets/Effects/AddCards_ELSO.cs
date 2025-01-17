using System.Linq;
using UnityEngine;
using Decker;

[CreateAssetMenu(fileName = "AddCards_ELSO", menuName = "EffectLogic/AddCards_ELSO")]
public class AddCards_ELSO : EffectLogic_SO
{
    protected override void PlayEffect()
    {
        ThisTriggers = Trigger.OnCardAdd;
        var targets = GetTargets();
        if (targets.Any())
        {
            foreach (var t in targets)
            {
                t.GetComponent<Attributes_sc>().DealDamage(magnitude);
            }
        }
    }
}
