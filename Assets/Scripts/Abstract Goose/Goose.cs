using System.Collections;
using UnityEngine;

public abstract class Goose : MonoBehaviour
{
    //[SerializeField] private ParticleSystem _effectDie;
    //[SerializeField] private ParticleSystem _effectStun;

    [SerializeField] private ToggleVisibility _toggleVisibility;
    [SerializeField] private AnimationsGoose _animationsGoose;

    [field: SerializeField] public Bush TargetBush { get; private set; }
    [field: SerializeField] public int MinimumBushLevel { get; private set; } = 5;
    [field: SerializeField] public int Cost { get; private set; }

    protected GooseMover GooseMover;
    protected DetectorBush DetectorBush;
    protected Health Health;
    protected PauseBeforeTransition PauseBeforeTransition;

    private Coroutine _coroutine;
    private WaitForSecondsRealtime _wait;
    private int _health;
    private float _startPause;
    private float _startSpeed;
    private Vector3 _startSize;
    private IPoolAdder<Goose> _poolAdder;
    private GeeseNavigator _geeseNavigator;

    public Transform TargetMovement { get; private set; }
    public bool IsSmall { get; private set; }

    protected virtual void Awake()
    {
        DetectorBush = GetComponent<DetectorBush>();
        GooseMover = GetComponent<GooseMover>();

        Health = new Health();
        GooseMover.SetSlowSpeed();

        PauseBeforeTransition = new PauseBeforeTransition();
        PauseBeforeTransition.SetLongPause();
        _wait = new WaitForSecondsRealtime(PauseBeforeTransition.Value);
        _startPause = PauseBeforeTransition.Value;
    }

    private void OnEnable()
    {
        Health.Deceased += Die;
        GooseMover.TargetReached += WaitInBush;
    }

    private void OnDisable()
    {
        Health.Deceased -= Die;
        GooseMover.TargetReached -= WaitInBush;
    }

    public void Init(IPoolAdder<Goose> poolAdder) =>
        _poolAdder = poolAdder;

    public void SetNavigator(GeeseNavigator geeseNavigator)
    {
        _geeseNavigator = geeseNavigator;
        _geeseNavigator.OnAssignTarget(this);
        _geeseNavigator.AddGoose(this);
    }

    public void ResetParameters()
    {
        Health.RestartHealth(_health);
        GooseMover.ResetSpeed(_startSpeed);
        transform.localScale = _startSize;
        IsSmall = false;
        TargetBush = null;
    }

    public void IncreaseSpeedMovement(float speedBoost) =>
        GooseMover.IncreaseSpeedMovement(speedBoost);

    public void SetSmallerSize()
    {
        if (IsSmall == false)
        {
            IsSmall = true;
            transform.localScale /= 2f;
        }
    }

    public virtual void TakeDamage(int damage) =>
        Health.TakeDamage(damage);

    public void TakeStun(float timeStun)
    {
        GooseMover.TakeStun(timeStun);
        //_effectStun.Play();
    }

    public void SetTargetMovement(Transform transformTarget)
    {
        TargetMovement = transformTarget;
        GooseMover.GoToTarget(TargetMovement);
        _toggleVisibility.EnableDisplay();
        _animationsGoose.TriggerRun(GooseMover.CurrentSpeed, GooseMover.SlowSpeed, GooseMover.MediumSpeed, GooseMover.FastSpeed);
    }

    public void SetBush(Bush bush) =>
        TargetBush = bush;

    public void SetLittleHealth() =>
        Health.SetLittleHealth();

    public void Heal(int heal) =>
        Health.Heal(heal);

    public void StopWait()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    protected virtual void Die()
    {
        //_effectDie.Play();
        _poolAdder.AddToPool(this);
        _geeseNavigator.RemoveGoose(this);
    }

    protected void SetStartParameters()
    {
        _startSize = transform.localScale;
        _health = Health.Value;
        _startSpeed = GooseMover.CurrentSpeed;
    }

    private void WaitInBush()
    {
        TargetMovement = null;
        _coroutine = StartCoroutine(StartWait());
    }

    private IEnumerator StartWait()
    {
        _toggleVisibility.DisableDisplay();

        yield return _wait;

        _geeseNavigator.OnAssignTarget(this);
    }
}
