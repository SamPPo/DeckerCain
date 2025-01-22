using System.Linq;
using UnityEngine;
using Decker;

[CreateAssetMenu(fileName = "DealDamage_ELSO", menuName = "EffectLogic/DealDamage_ELSO")]
public class DealDamage_ELSO : EffectLogic_SO
{
    protected override void SetText()
    {
        text = "Deal " + magnitude + " damage";
    }

    protected override void PlayEffectInherited(int loopCount, GameObject target)
    {
        target.GetComponent<Attributes_sc>().DealDamage(magnitude);
    }
}
