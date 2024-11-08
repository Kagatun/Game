using UnityEngine;

public class Dog : MonoBehaviour
{
   [SerializeField] private AnimationsDog _animationsDog;

    public void StartBarking() =>
        _animationsDog.TriggerBay();
}
