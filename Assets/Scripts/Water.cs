using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Goose goose))
        {
            ActiveEffect(goose);
            goose.SetStatusIsFloating(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose))
        {
            ActiveEffect(goose);
            goose.SetStatusIsFloating(false);
        }
    }

    private void SetNormalGoode(Goose goose)//Вызывать у гуся, который ныряет, в момент когда выныривает. И переопределить у него плавание
    {
        goose.Navigator.SetStatusPersonRunningPastBushes(true);
        goose.SetStatusIsFloating(false);
        goose.Navigator.OnAssignTarget();
    }

    private void ActiveEffect(Goose goose)
    {
        _effect.transform.position = goose.transform.position;
        _effect.Play();
    }
}
