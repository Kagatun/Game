using UnityEngine;

public class SprinterGoose : Goose
{
    private int _boostSpeed = 4;

    protected override void Awake()
    {
        base.Awake();

        PauseBeforeTransition.SetAveragePause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
        GooseMover.SetSlowSpeed();
        Health.SetBigHealth();

        SetStartParameters();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        IncreaseSpeedMovement(_boostSpeed);
        SwitchAnimationMove();
    }
}
