using System;
using UnityEngine;
using WaveSpawner.Enemies;
using WaveSpawner.UI;
using WaveSpawner.Waves;

namespace WaveSpawner.Bootstrap
{
  public class GameBootstrap : MonoBehaviour
  {
    [Header("Configs")]
    [SerializeField] private WaveSettings _waveSettings;

    [Header("Enemy pool")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _enemiesRoot;
    [SerializeField, Min(1)] private int _poolDefaultCapacity = 10;
    [SerializeField, Min(1)] private int _poolMaxSize = 50;

    [Header("Scene objects")]
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private WaveController _waveController;
    [SerializeField] private WaveHud _waveHud;

    private IDisposable _enemyFactoryDisposable;
    private bool _isInitialized;

    private void Awake()
    {
      var enemyFactory = new PooledEnemyFactory(_enemyPrefab, _enemiesRoot, _poolDefaultCapacity, _poolMaxSize);
      _enemyFactoryDisposable = enemyFactory;

      var aliveEnemiesTracker = new AliveEnemiesTracker();

      _enemySpawner.Initialize(enemyFactory);
      _waveController.Initialize(_waveSettings, _enemySpawner, aliveEnemiesTracker);
      _waveHud.Initialize(_waveController, aliveEnemiesTracker);

      _isInitialized = true;
    }

    private void Start()
    {
      if (_isInitialized)
      {
        _waveController.StartWaves();
      }
    }

    private void OnDestroy()
    {
      _enemyFactoryDisposable?.Dispose();
    }
  }
}