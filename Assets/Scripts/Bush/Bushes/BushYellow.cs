using System.Collections.Generic;
using UnityEngine;

public class BushYellow : Bush
{
    private List<Bush> _bushes = new List<Bush>();

    protected override void Awake()
    {
        base.Awake();
        SetSlowSpeedGrowth();
    }
 
    protected override void Ability(Goose goose)
    {
        goose.ReduceSizeByHalf(); //��������� ���� � �������
        goose.SetLittleHealth();//������ �������� 1
    }
}