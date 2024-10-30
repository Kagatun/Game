using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGrowth
{
    public float Value { get; private set; }

    public void SetSlowSpeedGrowth() =>
        Value = 10f;

    public void SetMediumSpeedGrowth() =>
        Value = 5.0f;

    public void SetFastSpeedGrowth() =>
        Value = 1.5f;
}
