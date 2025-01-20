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
        OnRoundStart,
        OnRoundEnd,
        StartOfTurn,
        EndOfTurn,
        OnPlay,
        OnDraw,
        OnDiscard,
        OnDeath,
        OnDamage,
        OnHeal,
        OnKill,
        OnSpend,
        OnCrit,
        OnMiss,
        OnCardAdd
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
        All
    }

    public enum CardPile
    {
        Deck,
        Discard,
        Display,
        Spent
    }

    [Serializable]
    public class EffectPayload
    {
        public EffectLogic_SO effect;
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;
        public Trigger trigger;
        public WaitTime triggerWaitTime;

        public EffectData MakeEffectData(GameObject o)
        {
            EffectData ef = new()
            {
                magn = magnitude,
                targ = target,
                newc = newCards,
                trig = trigger,
                wait = triggerWaitTime,
                owner = o
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
        public WaitTime wait;
        public GameObject owner;
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
