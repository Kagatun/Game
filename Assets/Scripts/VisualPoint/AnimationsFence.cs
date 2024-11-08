using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsFence : MonoBehaviour
{
    private static class AnimationParams
    {
        public static readonly int Open = Animator.StringToHash("Open");
    }

    [SerializeField] private Animator _animator;

    public void TriggerActiveVisualPoint() =>
        _animator.SetTrigger(AnimationParams.Open);
}
