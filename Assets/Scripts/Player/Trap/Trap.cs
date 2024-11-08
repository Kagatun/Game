using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private AnimationsTrap _animations;

    private WaitForSeconds _waitRemove;
    private WaitForSeconds _waitHit;
    private bool isCharged = true;
    private int _damage = 1;
    private int _stun = 1;
    private float _time = 0.3f;
    private float _timeHit = 0.05f;

    private bool _isClick;

    private void Awake()
    {
        _waitRemove = new WaitForSeconds(_time);
        _waitHit = new WaitForSeconds(_timeHit);
    }

    private void OnDisable()
    {
        isCharged = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Goose goose) && isCharged)
            StartCoroutine(PrepareImpact(goose));
    }

    private void StrikeBlow(Goose goose)
    {
        goose.TakeStun(_stun);
        goose.TakeDamage(_damage);
        isCharged = false;
        _animations.TriggerCollision();
        StartCoroutine(StartRemove());
    }

    private IEnumerator StartRemove()
    {
        yield return _waitRemove;

        Destroy(gameObject);
        //метод ремове в пулл
    }

    private IEnumerator PrepareImpact(Goose goose)
    {
        yield return _waitHit;

        StrikeBlow(goose);
    }
}
