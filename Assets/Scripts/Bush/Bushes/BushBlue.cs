using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushBlue : Bush
{
    protected override void Awake()//���� ������ ������ ������
    {
        base.Awake();
        SetFastSpeedGrowth();
    }
}
