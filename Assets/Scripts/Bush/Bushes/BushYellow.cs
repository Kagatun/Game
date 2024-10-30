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
        goose.SetSmallerSize(); //уменьшает гуся в размере
        goose.SetLittleHealth();//делает здоровье 1
    }
}