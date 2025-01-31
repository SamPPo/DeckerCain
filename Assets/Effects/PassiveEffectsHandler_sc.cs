using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Decker;

public class PassiveEffectsHandler_sc
{
    private GameObject owner;
    private Dictionary<EffectType, List<PassiveEffect_sc>> activeEffects = new();

    public void ReceiveEffect(EffectType type, ModifiableEffectData effectData, bool instigator)
    {
        // Find all matching entries
        if (activeEffects.TryGetValue(type, out var effects))
        {
            foreach (var effect in effects)
            {
                if (instigator)
                    effect.ModifyEffectInstigator(effectData);
                else
                    effect.ModifyEffectReceiver(effectData);
            }
        }
    }

    public void AddEffect(PassiveEffect_sc effect, EffectType type)
    {
        if (!activeEffects.ContainsKey(type))
        {
            activeEffects[type] = new List<PassiveEffect_sc>();
        }
        activeEffects[type].Add(effect);
    }

    public void RemoveEffect(PassiveEffect_sc effect, EffectType type)
    {
        if (activeEffects.TryGetValue(type, out var effects))
        {
            effects.Remove(effect);
            if (effects.Count == 0)
            {
                activeEffects.Remove(type);
            }
        }
    }
}

public class PassiveEffect_sc
{
    public void ModifyEffectInstigator(ModifiableEffectData effectData)
    {

    }

    public void ModifyEffectReceiver(ModifiableEffectData effectData)
    {

    }
}
