using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushPink : Bush
{
    protected override void Awake() //������ �������� ����
    {
        base.Awake();
        SetStatusPrunableFalse();
    }
}
