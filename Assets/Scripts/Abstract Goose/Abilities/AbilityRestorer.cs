using UnityEngine;

public class AbilityRestorer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Bush bush))
        {
            bush.RefreshAbility();
            //Звук опрыскивания добавить
        }
    }
}
