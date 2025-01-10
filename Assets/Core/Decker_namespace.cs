using System;
using System.Collections.Generic;

namespace Decker
{
    public enum Trigger
    {
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
    public class ItemData
    {
        public string itemName;
        public List<EffectPayload> effectPayloads;
    }

    [Serializable]
    public class EffectPayload
    {
        public EffectLogic_SO effect;
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;
        public Trigger trigger;
    }

    public struct EffectData
    {
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;
        public Trigger trigger;
    }
}
