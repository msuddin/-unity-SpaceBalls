using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;

    List<Transform> waypoints;
    int wayPointsIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[wayPointsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (wayPointsIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[wayPointsIndex].transform.position;
            var movementToThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementToThisFrame);

            if (transform.position == targetPosition)
            {
                wayPointsIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
