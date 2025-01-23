using Decker;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectContainer_SO : ScriptableObject
{
    public string displayName;
    public List<EffectPayload> effectPayloads;
    protected GameObject containerPfab;
    protected GameObject owner;
    public void SetOwner(GameObject o) { owner = o; }

    public Transform GetPfabTransform () 
    {
        if (containerPfab != null)
        {
            return containerPfab.transform;
        }
        else if (owner != null)
        {
            return owner.transform;
        }
        else
        {
            Debug.Log("EffectContainer_SO: No Pfab transform or owner found!");
            return null;
        }
    }

    protected List<EffectLogic_SO> effectLogics = new();
    public void AddEffectLogic(EffectLogic_SO e)
    { effectLogics.Add(e); }
}
