using UnityEngine;

public class SoldierGoose : Goose , IArmorable , INonStunable
{
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
    }
}
