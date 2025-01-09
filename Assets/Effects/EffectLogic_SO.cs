using Decker;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "EffectLogic_SO", menuName = "Scriptable Objects/EffectLogic_SO")]
public class EffectLogic_SO : ScriptableObject
{
    public void PlayEffect(EffectData ed)
    {
        //Debug.Log("Deal " + ed.magnitude + " damage to " + ed.target);
    }

}
