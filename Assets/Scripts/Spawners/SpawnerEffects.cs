using System.Collections.Generic;
using UnityEngine;

public class SpawnerEffects : SpawnerObjects<EffectGoose>
{
    private List<Goose> _geese = new List<Goose>();

    private void OnDisable()
    {
        if (_geese != null)
        {
            foreach (var goose in _geese)
                goose.Defeated -= CreateEffect;
        }
    }

    public void AddGooseList(Goose goose)
    {
        if (!_geese.Contains(goose))
        {
            _geese.Add(goose);
            goose.Defeated += CreateEffect;
        }
    }

    private void CreateEffect(Transform transform)
    {
        EffectGoose effect = SpawnEffectGoose(transform);
        effect.StartTimerRemove();
    }

    public EffectGoose SpawnEffectGoose(Transform transform)
    {
        EffectGoose effectGoose = Get();
        effectGoose.Init(this);
        effectGoose.transform.position = transform.position;

        return effectGoose;
    }
}
