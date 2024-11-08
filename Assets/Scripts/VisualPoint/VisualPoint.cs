using UnityEngine;

public abstract class VisualPoint : MonoBehaviour
{
    public bool IsActive {  get; private set; }

    public virtual void StartAnimationVisualPoint() { }

    public void MakeActiveStatus() =>
        IsActive = true;
}
