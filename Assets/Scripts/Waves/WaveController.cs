using System;
using System.Collections;
using UnityEngine;
using WaveSpawner.Enemies;

namespace WaveSpawner.Waves
{
  public class WaveController : MonoBehaviour
  {
    private WaveSettings _settings;
    private IEnemySpawner _spawner;
    private AliveEnemiesTracker _tracker;
    private WaitUntil _waitForAllEnemiesDefeated;
    private Coroutine _runRoutine;

    public event Action<int, int> WaveStarted;

    public event Action AllWavesCompleted;

    public int CurrentWaveNumber { get; private set; }
    public int TotalWaves => _settings != null ? _settings.Waves.Count : 0;

    public void Initialize(WaveSettings settings, IEnemySpawner spawner, AliveEnemiesTracker tracker)
    {
      _settings = settings;
      _spawner = spawner;
      _tracker = tracker;
      _waitForAllEnemiesDefeated = new WaitUntil(() => _tracker.IsEmpty);
    }

    public void StartWaves()
    {
      if (_runRoutine != null)
      {
        return;
      }

      _runRoutine = StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
      int totalWaves = _settings.Waves.Count;

      for (int i = 0; i < totalWaves; i++)
      {
        WaveData wave = _settings.Waves[i];

        CurrentWaveNumber = i + 1;
        WaveStarted?.Invoke(CurrentWaveNumber, totalWaves);

        yield return SpawnWave(wave);
        yield return _waitForAllEnemiesDefeated;

        bool isLastWave = i == totalWaves - 1;
        if (!isLastWave)
        {
          yield return new WaitForSeconds(wave.DelayBeforeNextWave);
        }
      }

      _runRoutine = null;
      AllWavesCompleted?.Invoke();
    }

    private IEnumerator SpawnWave(WaveData wave)
    {
      var spawnDelay = new WaitForSeconds(wave.SpawnInterval);

      for (int i = 0; i < wave.EnemyCount; i++)
      {
        SpawnEnemy();

        bool isLastEnemy = i == wave.EnemyCount - 1;
        if (!isLastEnemy)
        {
          yield return spawnDelay;
        }
      }
    }

    private void SpawnEnemy()
    {
      IEnemy enemy = _spawner.Spawn();
      _tracker.Track(enemy);
    }
  }
}