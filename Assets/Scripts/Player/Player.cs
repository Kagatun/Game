using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Money { get; private set; }

    public void AddMoney(int price) =>
        Money += price;

    public void SpendMoney(int price) =>
        Money -= price;
}
