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
    private int _growDefault = 1;
    private bool _startStatus;

    public event Action<Bush> LevelChanged;
    public event Action<Bush> LevelChangedDown;

    public int CurrentAbilityPoints { get; private set; } = 5;
    public int MinLevelValue { get; private set; } = 0;
    public int MaxLevelValue { get; private set; } = 5;
    public int CurrentLevel { get; private set; }
    public bool IsPrunable { get; private set; } = true;

    protected virtual void Awake()
    {
        _gooseDetector = GetComponent<GooseDetector>();
        _startAbilityPoints = CurrentAbilityPoints;

        _speedGrowth = new SpeedGrowth();
        _speedGrowth.SetMediumSpeedGrowth();
        _startSpeed = _speedGrowth.Value;

        _startStatus = IsPrunable;
        _waitGrowth = new WaitForSecondsRealtime(_startSpeed);

        CurrentLevel = MaxLevelValue;
    }

    private void OnEnable()
    {
        _gooseDetector.Collided += ApplyAbility;
    }

    private void OnDisable()
    {
        _gooseDetector.Collided -= ApplyAbility;
    }

    public void Grow(int levels)
    {
        CurrentLevel += levels;
        CurrentLevel = Mathf.Clamp(CurrentLevel, MinLevelValue, MaxLevelValue);
        UpdateSize();
    }

    public void Shrink(int levels)
    {
        if (IsPrunable)
        {
            CurrentLevel -= levels;
            CurrentLevel = Mathf.Clamp(CurrentLevel, MinLevelValue, MaxLevelValue);
            UpdateSize();
            LevelChangedDown?.Invoke(this);
            StartCoroutine(GrowBack());
        }
    }

    public void SetStatusPrunableTrue() =>
    IsPrunable = true;

    public void SetStatusPrunableFalse() =>
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
        CurrentAbilityPoints = _startAbilityPoints;
        _waitGrowth = new WaitForSecondsRealtime(_startSpeed);
        IsPrunable = _startStatus;
        UpdateSize();
    }

    public void MakeOrdinary(int damage)
    {
        CurrentAbilityPoints -= damage;
        CurrentAbilityPoints = Mathf.Clamp(CurrentAbilityPoints, MinLevelValue, MaxLevelValue);

        if (CurrentAbilityPoints == MinLevelValue)
        {
            SetMediumSpeedGrowth();
            SetStatusPrunableTrue();
        }

        UpdateSize();
    }

    public void ApplyAbility(Goose goose)
    {
            if (goose.Navigator.CurrentBush == this && goose is IAbilityInvalidTarget == false && goose.Navigator.IsBushNeeded == true)
                if (GetApplication() && CurrentLevel == MaxLevelValue)
                    Ability(goose);
    }

    protected virtual void Ability(Goose goose) { }

    private void UpdateSize() =>
       LevelChanged?.Invoke(this);

    private bool GetApplication()
    {
        float percentFeil = UnityEngine.Random.Range(MinLevelValue, MaxLevelValue);

        if (percentFeil < CurrentAbilityPoints)
            return true;

        return false;
    }

    private IEnumerator GrowBack()
    {
        yield return _waitGrowth;

        Grow(_growDefault);
    }
}