using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProhibitCircumcision : MonoBehaviour
{
    private Bush _bush;
    private bool _startStatusBush;

    private void OnDisable()
    {
        if (_bush != null)
        {
            RestoreStatus(_bush.IsPrunable);
            _bush = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Bush bush))
        {
            _startStatusBush = bush.IsPrunable;
            _bush = bush;
            bush.SetStatusPrunableFalse();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Bush bush))
            RestoreStatus(_startStatusBush);
    }

    private void RestoreStatus(bool status)
    {
        if (status == true)
        {
            _bush.SetStatusPrunableTrue();
        }
        else
        {
            _bush.SetStatusPrunableFalse();
        }
    }
}
