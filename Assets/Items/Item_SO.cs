using Decker;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Item_SO")]
public class Item_SO : EffectContainer_SO
{
    public void BindEffectsToTriggers()
    {
        foreach (EffectLogic_SO e in effectLogics)
        {
            e.BindToTriggerDelegates();
        }
    }
}
