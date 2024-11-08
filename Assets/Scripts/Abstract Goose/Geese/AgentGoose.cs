using UnityEngine;

public class AgentGoose : Goose
{
    private int _maxPercent = 100;
    private int _minPercent = 0;
    private int _percentageEvasion = 10;


    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetAveragePause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetMediumSpeed();
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
