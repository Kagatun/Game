using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBall : SpawnerObjects<Ball>
{
    public Ball SpawnBall(Transform spawnPoint, Vector3 direction)
    {
        Ball ball = Get();
        ball.Init(this);
        ball.transform.position = spawnPoint.position;
        ball.transform.forward = direction;

        return ball;
    }
}
