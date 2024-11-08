using UnityEngine;

public class AnimationsDog : MonoBehaviour
{
    private static class AnimationParams
    {
        public static readonly int Bay = Animator.StringToHash("Bay");
    }

    [SerializeField] private Animator _animator;

    public void TriggerBay() =>
        _animator.SetTrigger(AnimationParams.Bay);
}
