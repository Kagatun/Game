using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTrap : MonoBehaviour
{
    private static class AnimationParams
    {
        public static readonly int Collision = Animator.StringToHash("Collision");
    }

    [SerializeField] private Animator _animator;

    public void TriggerCollision() =>
        _animator.SetTrigger(AnimationParams.Collision);
}
