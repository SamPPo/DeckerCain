using Decker;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectContainer_SO : ScriptableObject
{
    public string displayName;
    public List<EffectPayload> effectPayloads;
    protected GameObject containerPfab;

    public Transform GetPfabTransform () 
    { 
        return containerPfab.transform != null ? containerPfab.transform : null;
    }

    protected List<EffectLogic_SO> effectLogics = new();
    public void AddEffectLogic(EffectLogic_SO e)
    { effectLogics.Add(e); }
}
