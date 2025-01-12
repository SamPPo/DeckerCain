using System;
using System.Collections.Generic;
using System.Numerics;

namespace Decker
{
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
        OnMiss
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

    [Serializable]
    public class EffectPayload
    {
        public EffectLogic_SO effect;
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;
        public Trigger trigger;
        public WaitTime triggerWaitTime;

        public EffectData MakeEffectData()
        {
            EffectData ef = new()
            {
                magn = magnitude,
                targ = target,
                newc = newCards,
                trig = trigger,
                wait = triggerWaitTime
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
    }

    public struct DTransform
    {
        public UnityEngine.Vector3 position;
        public UnityEngine.Quaternion rotation;
        public UnityEngine.Vector3 scale;
    }
}
