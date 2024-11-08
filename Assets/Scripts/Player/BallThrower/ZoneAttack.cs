using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoneAttack : MonoBehaviour
{
    [SerializeField] private List<Goose> _geese = new List<Goose>();

    public event Action<Goose> SuitableGoose;

    private void OnDisable()
    {
        _geese.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose))
            _geese.Add(goose);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose))
            _geese.Remove(goose);
    }

    private void LostGoose(Goose goose)
    {

    }

    private void SetTarget()
    {
        //if (_geese.Count > 0)
            //FindSuitableTarget();
    }

    //private Goose FindSuitableTarget()
    //{
    //    Goose goose = null;

    //    if (goose != null && goose.CurrentHealth <= 0)
    //    {
    //        _geese.Remove(goose);
    //        goose = null;
    //    }

    //    foreach (Goose goose in _geese)
    //    {
    //        if (goose.CurrentHealth <= 0)
    //        {
    //            _geese.Remove(goose);

    //            if (goose.CurrentHealth <= 0)
    //                goose = null;
    //        }
    //    }

    //    if (_geese.Count == 0)
    //        return null;

    //    if (goose == null)
    //    {
    //        var healthyGeese = _geese.Where(goose => goose.CurrentHealth > 0).ToList();
    //        var activeGeese = healthyGeese.FirstOrDefault(goose => goose.ToggleVisibility.IsEnable);

    //        return activeGeese;
    //    }

    //    return null;
    //}

    //private void RemoveGoose(Goose goose)
    //{
    //    _geese.Remove(goose);

    //    if (goose == goose)
    //        goose = null;

    //    if (_geese.Count == 0)
    //        goose = null;
    //}
}
