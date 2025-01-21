using UnityEngine;
using Decker;

public static class Pvsc
{
    public static float wat = 1.0f;    //Wait after turn duration
    public static float wbed = 1.0f;   //Wait for big effect duration
    public static float wsed = 1.0f;   //Wait for small effect duration
    public static float GlobalWaitMultiplier = 1.0f;

    public static Vector3 cardExtents = new(2f, 0.1f, 3f);

    public static float GetWaitTime(WaitTime t)
    {
        float g = GlobalWaitMultiplier;
        float f = t switch
        {
            WaitTime.Long => 1.6f * g,
            WaitTime.Medium => 0.8f * g,
            WaitTime.Short => 0.4f * g,
            WaitTime.Snap => 0.2f * g,
            _ => 1.6f * g,
        };
        return f;
    }
}
