using System.Collections.Generic;
using UnityEngine;

public class SpawnerBushes : SpawnerObjects<Bush>
{
    public Bush SpawnBush(Transform spawnPoint)
    {
        float minRandomRotationY = -210;
        float maxRandomRotationY = 210;

        Bush bush = Get();
        bush.transform.position = spawnPoint.position;
        bush.transform.rotation = Quaternion.Euler(0, Random.Range(minRandomRotationY, maxRandomRotationY), 0);

        return bush;
    }
}
