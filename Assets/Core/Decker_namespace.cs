using System;
using System.Collections.Generic;

namespace Decker
{
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
    public class CardData
    {
        public string cardName;
        public List<EffectPayload> effectPayloads;
        public List<Keyword> keywords;
        public PositionPreference positionPreference;
    }

    public struct EffectData
    {
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;
    }

    [Serializable]
    public class EffectPayload
    {
        public EffectLogic_SO effect;
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;

        public void PlayEffectPayload()
        {
            EffectData ed = new()
            {
                magnitude = this.magnitude,
                target = this.target,
                newCards = this.newCards
            };
            effect.PlayEffect(ed);
        }

    }
}
