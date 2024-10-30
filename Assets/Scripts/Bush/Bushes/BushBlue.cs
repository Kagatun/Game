using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBlue : Bush
{
    protected override void Awake()//куст просто быстро растет
    {
        base.Awake();
        SetFastSpeedGrowth();
    }
}
