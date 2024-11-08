using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGoose : MonoBehaviour
{
    [SerializeField] private float _time;

    private WaitForSeconds _wait;
    //private Coroutine _coroutine;
    private IPoolAdder<EffectGoose> _poolAdder;

    private void Awake()
    {
        _wait = new WaitForSeconds (_time);
    }

    public void Init(IPoolAdder<EffectGoose> poolAdder) =>
    _poolAdder = poolAdder;

    public void StartTimerRemove() =>
       /*_coroutine = */StartCoroutine(StartEffectBoom());

    //private void StopTimerRemove()
    //{
    //    if (_coroutine != null)
    //        StopCoroutine(_coroutine);
    //}

    private IEnumerator StartEffectBoom()
    {
        //StopTimerRemove();

        yield return _wait;

        _poolAdder.AddToPool(this);
    }

}
