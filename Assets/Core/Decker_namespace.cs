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

    [Serializable]
    public class EffectPayload
    {
        public Effect_sc effect;
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;

        public void SetData()
        {
            effect.SetEffectData(magnitude, target);
            if (newCards != null)
            {
                effect.SetNewCardsToAddData(newCards);
            }
        }
    }
}
