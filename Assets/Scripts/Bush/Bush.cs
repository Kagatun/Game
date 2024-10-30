using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bush : MonoBehaviour
{
    private GooseDetector _gooseDetector;
    private SpeedGrowth _speedGrowth;
    private WaitForSecondsRealtime _waitGrowth;
    private float _startSpeed;
    private int _startAbilityPoints;
    private bool _startStatus;

    public event Action<Bush> LevelChanged;
    public event Action<Bush> LevelChangedDown;

    public int AbilityPoints { get; private set; } = 5;
    public int MinLevelBush { get; private set; } = 0;
    public int MaxLevelBush { get; private set; } = 5;
    public int CurrentLevel { get; private set; }
    public bool IsPrunable { get; private set; } = true;

    protected virtual void Awake()
    {
        _gooseDetector = GetComponent<GooseDetector>();
        _startAbilityPoints = AbilityPoints;

        _speedGrowth = new SpeedGrowth();
        _speedGrowth.SetMediumSpeedGrowth();
        _startSpeed = _speedGrowth.Value;

        _startStatus = IsPrunable;
        _waitGrowth = new WaitForSecondsRealtime(_startSpeed);

        CurrentLevel = MaxLevelBush;
    }

    private void OnEnable()
    {
        _gooseDetector.Collided += ApplyAbility;
    }

    private void OnDisable()
    {
        _gooseDetector.Collided -= ApplyAbility;
    }

    public void Grow(int levels) // Публичный метод для роста куста
    {
        CurrentLevel += levels;
        CurrentLevel = Mathf.Clamp(CurrentLevel, MinLevelBush, MaxLevelBush);
        UpdateSize();
    }

    public void Shrink(int levels) // Публичный метод для уменьшения куста
    {
        if (IsPrunable)
        {
            CurrentLevel -= levels;
            CurrentLevel = Mathf.Clamp(CurrentLevel, MinLevelBush, MaxLevelBush);
            UpdateSize();
            LevelChangedDown?.Invoke(this);
            StartCoroutine(GrowBack());
        }
    }

    public void SetStatusPrunableTrue() =>//публичный так как гусь будет делать так чтоб куст можно было подрезать когда гусь вышел из куста
    IsPrunable = true;

    public void SetStatusPrunableFalse() =>//публичный так как гусь будет делать так чтоб куст нельзя было подрезать пока гусь в этом кусте
        IsPrunable = false;

    public void SetSlowSpeedGrowth()
    {
        _speedGrowth.SetSlowSpeedGrowth();
        _waitGrowth = new WaitForSecondsRealtime(_speedGrowth.Value);
    }

    public void SetMediumSpeedGrowth()
    {
        _speedGrowth.SetMediumSpeedGrowth();
        _waitGrowth = new WaitForSecondsRealtime(_speedGrowth.Value);
    }

    public void SetFastSpeedGrowth()
    {
        _speedGrowth.SetFastSpeedGrowth();
        _waitGrowth = new WaitForSecondsRealtime(_speedGrowth.Value);
    }

    public void RefreshAbility()
    {
        AbilityPoints = _startAbilityPoints;
        _waitGrowth = new WaitForSecondsRealtime(_startSpeed);
        IsPrunable = _startStatus;
    }

    public void MakeOrdinary(int damage)
    {
        int minAbilityPoints = 0;
        int maxAbilityPoints = 5;

        AbilityPoints -= damage;
        AbilityPoints = Mathf.Clamp(AbilityPoints, minAbilityPoints, maxAbilityPoints);

        if (AbilityPoints <= 0)
        {
            SetMediumSpeedGrowth();
            SetStatusPrunableTrue();
        }

        UpdateSize();
    }

    public void ApplyAbility(Goose goose)
    {
            if (goose.TargetBush == this && goose is IAbilityInvalidTarget == false && goose is IRunPast == false)
                if (GetApplication() && CurrentLevel == MaxLevelBush)
                    Ability(goose);
    }

    protected virtual void Ability(Goose goose) { }

    private void UpdateSize() =>
       LevelChanged?.Invoke(this);

    private bool GetApplication()
    {
        float maxRandomPoint = 5f;
        float percentFeil = UnityEngine.Random.Range(0, maxRandomPoint);

        if (percentFeil <= AbilityPoints)
            return true;

        return false;
    }

    private IEnumerator GrowBack()
    {
        int growDefault = 1;

        yield return _waitGrowth;

        Grow(growDefault);
    }
}