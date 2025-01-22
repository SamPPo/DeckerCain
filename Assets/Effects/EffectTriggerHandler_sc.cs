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
        switch (trigger)
        {
            case Trigger.none: break;
            case Trigger.OnCardPlay:
                TriggerHandler.onCardPlay += BindToTrigger;
                break;
            case Trigger.OnCombatStart:
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
            default:
                break;
        }
    }

    private void BindToTrigger(GameObject instigator = null, GameObject target = null)
    {
        if (!hasTriggered && !isBound)
        {
            var instigatorFaction = instigator.GetComponent<Character_sc>().faction;
            var ownerFaction = owner.GetOwner().GetComponent<Character_sc>().faction;
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
