using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsGoose : MonoBehaviour
{
    private static class AnimationParams
    {
        public static readonly int Fly = Animator.StringToHash("Fly");
        public static readonly int Sneak = Animator.StringToHash("Sneak");
        public static readonly int Sneak2 = Animator.StringToHash("Sneak2");
        public static readonly int Sneak3 = Animator.StringToHash("Sneak3");
        public static readonly int Run = Animator.StringToHash("Run");
        public static readonly int Run2 = Animator.StringToHash("Run2");
        public static readonly int Run3 = Animator.StringToHash("Run3");
        public static readonly int Run4 = Animator.StringToHash("Run4");
        public static readonly int Stun = Animator.StringToHash("Stun");
    }

    [SerializeField] private Animator _animator;

    private int[] _runAnimationsSpeedSlow = {
        AnimationParams.Sneak,
        AnimationParams.Sneak2,
        AnimationParams.Sneak3,
    };

    private int[] _runAnimationsSpeedMedium = {
        AnimationParams.Run,
        AnimationParams.Run2,
        AnimationParams.Run3,
    };

    public void TriggerRun(float currentSpeed, float speedSlow, float SpeedMedium, float SpeedFast)
    {
        if (currentSpeed == speedSlow)
        {
            int randomIndex = Random.Range(0, _runAnimationsSpeedSlow.Length);
            _animator.SetTrigger(_runAnimationsSpeedSlow[randomIndex]);
        }
        else if (currentSpeed == SpeedMedium)
        {
            int randomIndex = Random.Range(0, _runAnimationsSpeedMedium.Length);
            _animator.SetTrigger(_runAnimationsSpeedMedium[randomIndex]);
        }
        else if(currentSpeed == SpeedFast)
        {
            _animator.SetTrigger(AnimationParams.Run4);
        }
    }

    public void TriggerStun()
    {
        _animator.SetTrigger(AnimationParams.Stun);
    }

    //public void TriggerTakeDamage()
    //{
    //    _animator.SetTrigger(AnimationParams.Hit);
    //}
}
