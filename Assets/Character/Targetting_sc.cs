using UnityEngine;
using Decker;
using System.Collections.Generic;
using System;

public class Targetting_sc : MonoBehaviour
{
    public List<GameObject> GetTarget(Targetting t)
    {
                     ///CONTINUE THIS!!!!

        GameObject targetObject = t switch
        {
            Targetting.Enemy => FindEnemyTarget(),// Logic to get an enemy target
            Targetting.Self => this.gameObject,// Logic to get self target
            Targetting.Ally => FindAllyTarget(),// Logic to get an ally target
            Targetting.All => FindAllTargets(),// Logic to get all targets (could be a list or array)
            _ => throw new ArgumentOutOfRangeException(nameof(t), t, null),
        };
        return null;
    }
    private GameObject FindEnemyTarget()
    {
        // Implement logic to find and return an enemy target
        return null; // Placeholder
    }

    private GameObject FindAllyTarget()
    {
        // Implement logic to find and return an ally target
        return null; // Placeholder
    }

    private GameObject FindAllTargets()
    {
        // Implement logic to find and return all targets
        return null; // Placeholder
    }
}
