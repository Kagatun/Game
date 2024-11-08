using UnityEngine;

public class NinjaGoose : Goose, IAbilityInvalidTarget
{
    private int _maxPercent = 100;
    private int _minPercent = 0;
    private int _percentageEvasion = 15;

    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetWithoutPause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetFastSpeed();
        Health.SetLittleHealth();

        SetStartParameters();
    }

    public override void TakeDamage(int damage)
    {
        int randomValue = Random.Range(_minPercent, _maxPercent);

        if (randomValue < _percentageEvasion)
            base.TakeDamage(damage);
    }
}
