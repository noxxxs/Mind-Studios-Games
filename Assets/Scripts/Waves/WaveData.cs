using System;
using UnityEngine;

namespace WaveSpawner.Waves
{
  [Serializable]
  public class WaveData
  {
    [SerializeField, Min(1)] private int _enemyCount = 5;
    [SerializeField, Min(0f)] private float _spawnInterval = 1f;
    [SerializeField, Min(0f)] private float _delayBeforeNextWave = 3f;

    public int EnemyCount => _enemyCount;
    public float SpawnInterval => _spawnInterval;
    public float DelayBeforeNextWave => _delayBeforeNextWave;
  }
}