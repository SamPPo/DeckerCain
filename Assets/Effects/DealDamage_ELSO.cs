using System.Linq;
using UnityEngine;
using Decker;

[CreateAssetMenu(fileName = "DealDamage_ELSO", menuName = "EffectLogic/DealDamage_ELSO")]
public class DealDamage_ELSO : EffectLogic_SO
{
    protected override void PlayEffect()
    {
        ThisTriggers = Trigger.OnDamage;
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
