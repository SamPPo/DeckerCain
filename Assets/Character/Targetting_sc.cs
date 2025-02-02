using UnityEngine;
using Decker;
using System.Collections.Generic;
using System;
using System.Linq;

public class Targetting_sc : MonoBehaviour
{
    private Faction ownFaction;

    public List<GameObject> GetTargets(Targetting t)
    {
        ownFaction = gameObject.GetComponent<Character_sc>().faction;
 
        List<GameObject> targets = t switch
        {
            Targetting.Enemy => FindEnemyTarget(),// Logic to get an enemy target
            Targetting.Self => FindSelfTarget(),// Logic to get self target
            Targetting.Ally => FindAllyTarget(),// Logic to get an ally target
            Targetting.All => FindAllTargets(),// Logic to get all targets (could be a list or array)
            _ => throw new ArgumentOutOfRangeException(nameof(t), t, null),
        };
        if (targets.Any())
            return targets;
        else
            return null;
    }

    private List<GameObject> FindEnemyTarget()
    {
        List<GameObject> possibleTargets = new();
        if (GameMaster_sc.GetCharacters().Any())
        {
            foreach (var c in GameMaster_sc.GetCharacters())
            {
                if (c.GetComponent<Character_sc>().faction != ownFaction && c.GetComponent<Character_sc>().Targetable)
                    possibleTargets.Add(c);
            }
        }
        else
            return null;

        GameObject closestTarget = null;
        float closestDistance = float.MaxValue;
        Vector3 currentPosition = transform.position;

        foreach (var target in possibleTargets)
        {
            float distance = Vector3.Distance(currentPosition, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }
        return new List<GameObject> { closestTarget };
    }

    private List<GameObject> FindSelfTarget()
    {
        List<GameObject> targets = new() {gameObject};
        return targets;
    }

    private List<GameObject> FindAllyTarget()
    {
        Debug.Log("Targetting_sc: Targetting behaviour unimplemented!");
        return null; // Placeholder
    }

    private List<GameObject> FindAllTargets()
    {
        Debug.Log("Targetting_sc: Targetting behaviour unimplemented!");
        return null; // Placeholder
    }
}
