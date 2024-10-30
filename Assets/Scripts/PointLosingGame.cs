using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PointLosingGame : MonoBehaviour
{
    //public event Action Ended;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose))
        {
            //Ended?.Invoke(); //подпишется на это событие Game который управляет стартом сцены.
            Debug.Log("Игра завершена, вы проиграли");
        }
    }
}
