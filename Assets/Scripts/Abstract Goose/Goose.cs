using System;
using System.Collections;
using UnityEngine;

public abstract class Goose : MonoBehaviour
{
    [SerializeField] private ParticleSystem _clickEffect;
    [SerializeField] private ToggleVisibility _toggleVisibility;

    [SerializeField] protected AnimationsGoose AnimationsGoose;

    [field: SerializeField] public Navigator Navigator { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }

    protected GooseMover GooseMover;
    protected Health Health;
    protected PauseBeforeTransition PauseBeforeTransition;
    protected bool IsFly;
    protected float HeightMovement = 0;
    protected bool IsSmall;
    protected WaitForSeconds Wait;

    private Coroutine _coroutine;
    private int _health;
    private int _defaultHealth = 1;
    private float _startSpeed;
    private Vector3 _startSize;
    private IPoolAdder<Goose> _poolAdder;
    private bool _startStatusSize;
    private bool _isStun;
    private bool _hasSwims;

    public event Action<Transform> Defeated;

    public int CurrentHealth => Health.Value;

    public ToggleVisibility ToggleVisibility => _toggleVisibility;

    protected virtual void Awake()
    {
        GooseMover = GetComponent<GooseMover>();

        Health = new Health(_defaultHealth);
        GooseMover.SetSlowSpeed();

        PauseBeforeTransition = new PauseBeforeTransition();
        PauseBeforeTransition.SetLongPause();
        Wait = new WaitForSeconds(PauseBeforeTransition.Value);
    }

    private void OnEnable()
    {
        Navigator.ReadedTarget += SetTargetMovement;
        Health.Deceased += Die;
        GooseMover.TargetReached += WaitInBush;
        GooseMover.Stuned += SetStatusStun;
    }

    private void OnDisable()
    {
        Navigator.ReadedTarget -= SetTargetMovement;
        Health.Deceased -= Die;
        GooseMover.TargetReached -= WaitInBush;
        GooseMover.Stuned -= SetStatusStun;
    }

    public void Init(IPoolAdder<Goose> poolAdder) =>
        _poolAdder = poolAdder;

    public void ResetParameters()
    {
        Health.RestartHealth(_health);
        GooseMover.ResetSpeed(_startSpeed);
        transform.localScale = _startSize;
        IsSmall = _startStatusSize;
        _isStun = false;
        _hasSwims = false;
        _toggleVisibility.EnableDisplay();
    }

    public void IncreaseSpeedMovement(float speedBoost) =>
        GooseMover.IncreaseSpeedMovement(speedBoost);

    public void ReduceSizeByHalf()
    {
        if (IsSmall == false)
        {
            IsSmall = true;
            transform.localScale /= 2f;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (this is IArmorable == false)
            TakeHit(damage);
    }

    public void TakeCleanDamage(int damage) =>
        TakeHit(damage);

    public void TakeStun(float timeStun)
    {
        if (timeStun == 0)
            return;

        if (this is INonStunable == false)
            GooseMover.TakeStun(timeStun);
    }

    public void SetLittleHealth() =>
        Health.SetLittleHealth();

    public void SetStatusIsFloating(bool hasSwims)
    {
        _hasSwims = hasSwims;
        SwitchAnimationMove();
    }

    public void Heal(int heal) =>
        Health.Heal(heal);

    public void SwitchAnimationMove()
    {
        if (_isStun)
        {
            AnimationsGoose.TriggerStun(_hasSwims);
            return;
        }

        if (IsFly)
        {
            AnimationsGoose.TriggerFly();
        }
        else if (_hasSwims)
        {
            AnimationsGoose.TriggerSwim(GooseMover.CurrentSpeed, GooseMover.SlowSpeed, GooseMover.MediumSpeed, GooseMover.FastSpeed);
        }
        else
        {
            AnimationsGoose.TriggerRun(GooseMover.CurrentSpeed, GooseMover.SlowSpeed, GooseMover.MediumSpeed, GooseMover.FastSpeed);
        }
    }

    public virtual void Die()
    {
        Defeated?.Invoke(transform);
        _poolAdder.AddToPool(this);
    }

    protected void SetStartParameters()
    {
        _startSize = transform.localScale;
        _health = Health.Value;
        _startSpeed = GooseMover.CurrentSpeed;
        _startStatusSize = IsSmall;
    }

    protected virtual void SetTargetMovement(Transform transformTarget)
    {
        StopWait();
        GooseMover.GoToTarget(transformTarget, HeightMovement);
        _toggleVisibility.EnableDisplay();
        SwitchAnimationMove();
    }

    protected void MakeFly()
    {
        Navigator.SetStatusPersonRunningPastBushes(false);
        HeightMovement = 5;
        IsFly = true;
    }

    protected virtual void WaitInBush()
    {
        GooseMover.ResetTargetMovement();
        Navigator.ResetTargetMovement();
        _coroutine = StartCoroutine(StartWait());
    }

    private void TakeHit(int damage)
    {
        if (Health.Value > 1)
        {
            _clickEffect.transform.position = transform.position + new Vector3(0, 1, 0);
            _clickEffect.Play();
        }

        Health.TakeDamage(damage);
    }

    private void SetStatusStun(bool isStun)
    {
        _isStun = isStun;
        SwitchAnimationMove();
    }

    private void StopWait()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator StartWait()
    {
        _toggleVisibility.DisableDisplay();

        yield return Wait;

        Navigator.OnAssignTarget();
    }
}
