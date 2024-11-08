using UnityEngine;

public class VisualPointFence : VisualPoint
{
    [SerializeField] private AnimationsFence _animationsFence;

    public override void StartAnimationVisualPoint() =>
        _animationsFence.TriggerActiveVisualPoint();
}
