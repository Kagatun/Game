using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BushWhite : Bush
{
    protected override void Ability(Goose goose) => //�������� ��� � ���� ������� ������ ����� � ����
         goose.Navigator.OnAssignTarget();
}
