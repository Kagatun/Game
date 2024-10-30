using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBeforeTransition
{
    public float Value { get; private set; }

    public void SetWithoutPause() =>
        Value = 0;

    public void SetSmallPause() =>
    Value = 1;

    public void SetAveragePause() =>
    Value = 2;

    public void SetLongPause() =>
    Value = 3;
}
