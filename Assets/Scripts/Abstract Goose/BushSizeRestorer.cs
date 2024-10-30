using UnityEngine;

public class BushSizeRestorer : MonoBehaviour
{
    private int _restoringGrowth = 5;
    private Bush _healedBush;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Bush bush))
        {
            if (bush != _healedBush)
            {
                _healedBush = bush;
                bush.Grow(_restoringGrowth);
            }
        }       
    }
}
