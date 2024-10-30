using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushRed : Bush
{
    private int _healing = 3;

    protected override void Ability(Goose goose) => //����� ���� ������� ����� � ����
        goose.Heal(_healing);
}
