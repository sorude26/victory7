using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController
{
    public static event Action<float> OnShake = default;    

    public static void PlayShake(float time)
    {
        OnShake?.Invoke(time);
    }
    public static void ResetEvent()
    {
        OnShake = null;
    }
}
