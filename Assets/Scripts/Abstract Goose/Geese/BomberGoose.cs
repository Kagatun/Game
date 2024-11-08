using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberGoose : Goose
{
    [SerializeField] private ParticleSystem _effectExplosion;

    private bool _isReadyExplode;
    private int _maxPercent = 100;
    private int _minPercent = 0;
    private int _percentageExplosion = 20;

    protected override void Awake()
    {
        base.Awake();

        SetStartParameters();
    }

    protected override void WaitInBush()
    {
        base.WaitInBush();

        if (_isReadyExplode == false)
        {
            int randomValue = Random.Range(_minPercent, _maxPercent);

            if (randomValue < _percentageExplosion)
                _isReadyExplode = true;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (_isReadyExplode)
        {
            var explosion = Instantiate(_effectExplosion, transform.position, Quaternion.identity);
        }
    }
}
