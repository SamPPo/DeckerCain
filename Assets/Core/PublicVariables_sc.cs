using UnityEngine;
using Decker;

public static class Pvsc
{
    public static float wat = 1.0f;    //Wait after turn duration
    public static float wbed = 1.0f;   //Wait for big effect duration
    public static float wsed = 1.0f;   //Wait for small effect duration
    public static float GlobalWaitMultiplier = 1.0f;

    public static float GetWaitTime(WaitTime t)
    {
        float g = GlobalWaitMultiplier;
        float f = t switch
        {
            WaitTime.Long => 2.0f * g,
            WaitTime.Medium => 1.0f * g,
            WaitTime.Short => 0.6f * g,
            WaitTime.Snap => 0.2f * g,
            _ => 1.0f * g,
        };
        return f;
    }
}
