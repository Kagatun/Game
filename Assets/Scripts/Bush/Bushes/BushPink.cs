using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushPink : Bush
{
    protected override void Awake() //нельзя обрезать куст
    {
        base.Awake();
        SetStatusPrunableFalse();
    }
}
