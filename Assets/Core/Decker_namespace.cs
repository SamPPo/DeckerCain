using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Decker
{
    public enum Faction
    {
        Player,
        Enemy,
        Neutral
    }
    public enum Trigger
    {
        none,
        OnCardPlay,
        OnCombatStart,
        OnCombatEnd,
        StartOfTurn,
        EndOfTurn,
        OnDeath,
        OnDamage,
        OnHeal,
        OnKill,
        OnSpend,
        OnCrit,
        OnMiss,
        OnCardAdd
    }

    public enum TriggerTarget
    {
        Self,
        Enemy,
        Ally,
        Any
    }

    public enum WaitTime
    {
        Long,
        Medium,
        Short,
        Snap
    }

    public enum Keyword
    {
        None,
        Spend,
        Miss,
        End
    }

    public enum PositionPreference
    {
        Top,
        Upper,
        Random,
        Lower,
        Bottom
    }

    public enum Targetting
    {
        Enemy,
        Self,
        Ally,
        All,
        RandomEnemy,
        RandomAlly,
        RandomAny
    }

    public enum CardPile
    {
        Deck,
        Discard,
        Display,
        Spent,
        Spawner
    }

    [Serializable]
    public class EffectPayload
    {
        public EffectLogic_SO effect;
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;
        public Trigger trigger;
        public TriggerTarget triggerTarget;
        public WaitTime triggerWaitTime;

        public EffectData MakeEffectData(GameObject o, EffectContainer_SO c)
        {
            EffectData ef = new()
            {
                magn = magnitude,
                targ = target,
                newc = newCards,
                trig = trigger,
                trigT = triggerTarget,
                wait = triggerWaitTime,
                ownerCharacter = o,
                ownerContainer = c
            };
            return ef;
        }
    }

    public struct EffectData
    {
        public int magn;
        public Targetting targ;
        public List<Card_SO> newc;
        public Trigger trig;
        public TriggerTarget trigT;
        public WaitTime wait;
        public GameObject ownerCharacter;
        public EffectContainer_SO ownerContainer;
    }

    public struct DTransform
    {
        public UnityEngine.Vector3 position;
        public UnityEngine.Quaternion rotation;
        public UnityEngine.Vector3 scale;

        public void MakeTransform(Transform t)
        {
            position = t.position;
            rotation = t.rotation;
            scale = t.localScale;
        }

        public void MakeCameraFacingTransform(Transform t)
        {
            position = t.position;
            scale = t.localScale;

            // Calculate the rotation to face the camera
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                rotation = UnityEngine.Quaternion.LookRotation(Camera.main.transform.up, -Camera.main.transform.forward);
            }
            else
            {
                rotation = t.rotation; // Fallback to the original rotation if no camera is found
            }
        }
    }
}
