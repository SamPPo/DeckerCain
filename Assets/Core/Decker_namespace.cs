using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

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
    public class EffectPayload
    {
        public Effect_sc effect;
        public int magnitude;
        public Targetting target;
        public List<Card_SO> newCards;
    }
}
