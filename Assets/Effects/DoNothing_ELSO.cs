using UnityEngine;

[CreateAssetMenu(fileName = "DoNothing_ELSO", menuName = "EffectLogic/DoNothing_ELSO")]
public class DoNothing_ELSO : EffectLogic_SO
{
    protected override void SetText()
    {
        text = "";
    }
}
