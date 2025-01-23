using Decker;
using UnityEngine;

public class EffectTriggerHandler_sc
{
    private EffectLogic_SO owner;
    private Trigger trigger;
    private TriggerTarget triggerTarget;

    private bool hasTriggered;
    private bool isBound;

    public void InitializeTriggerHandler(Trigger t, TriggerTarget tt, EffectLogic_SO o)
    {
        trigger = t;
        triggerTarget = tt;
        this.owner = o;
        hasTriggered = false;
        isBound = false;
    }

    public void TriggerEffect()
    {
        hasTriggered = true;
        owner.ActivateEffect();
    }

    public void BindTrigger()
    {
        Debug.Log("Bind to " + trigger.ToString());
        switch (trigger)
        {
            case Trigger.none: break;
            case Trigger.OnCardPlay:
                TriggerHandler.onCardPlay += BindToTrigger;
                break;
            case Trigger.OnCombatStart:
                TriggerHandler.onCombatStart += BindToTrigger;
                break;
            case Trigger.OnCombatEnd:
                break;
            case Trigger.StartOfTurn:
                break;
            case Trigger.EndOfTurn:
                break;
            case Trigger.OnDeath:
                break;
            case Trigger.OnDamage:
                TriggerHandler.onDamage += BindToTrigger;
                break;
            case Trigger.OnHeal:
                break;
            case Trigger.OnKill:
                break;
            case Trigger.OnSpend:
                break;
            case Trigger.OnCrit:
                break;
            case Trigger.OnMiss:
                break;
            case Trigger.OnCardAdd:
                break;
            case Trigger.OnShuffleDeck:
                break;
            default:
                break;
        }
    }

    public void UnbindTrigger()
    {
        TriggerHandler.UnbindFromHandler(this);
    }

    private void BindToTrigger(GameObject instigator = null, GameObject target = null)
    {
        if (!hasTriggered && !isBound)
        {
            var instigatorFaction = Faction.Neutral;
            var ownerFaction = Faction.Neutral;
            if (instigator != null)
                instigatorFaction = instigator.GetComponent<Character_sc>().faction;
            if (owner.GetOwner() != null)
                ownerFaction = owner.GetOwner().GetComponent<Character_sc>().faction;

            switch (triggerTarget)
            {
                case TriggerTarget.Self:
                    if (target == owner.GetOwner())
                        AcceptBinding();
                    break;

                case TriggerTarget.Enemy:

                    if (instigatorFaction != ownerFaction)
                        AcceptBinding();
                    break;

                case TriggerTarget.Ally:
                    if (instigatorFaction == ownerFaction)
                        AcceptBinding();
                    break;

                case TriggerTarget.Any:
                    AcceptBinding();
                    break;

                default:
                    break;
            }
        }
    }

    private void AcceptBinding()
    {
        isBound = true;
        TriggerHandler.resetTriggers += ResetTrigger;
        TriggerHandler.BindToEvent(this, trigger);
    }


    public void ResetTrigger()
    {
        TriggerHandler.resetTriggers -= ResetTrigger;
        hasTriggered = false;
        isBound = false;
    }
}
