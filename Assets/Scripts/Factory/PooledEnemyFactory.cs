using System;
using UnityEngine;
using UnityEngine.Pool;

namespace WaveSpawner.Enemies
{
  public class PooledEnemyFactory : IEnemyFactory, IDisposable
  {
    private readonly Enemy _enemyPrefab;
    private readonly Transform _root;
    private readonly ObjectPool<Enemy> _pool;

    public PooledEnemyFactory(Enemy enemyPrefab, Transform root, int defaultCapacity, int maxSize)
    {
      _enemyPrefab = enemyPrefab;
      _root = root;

      _pool = new ObjectPool<Enemy>(
          createFunc: InstantiateEnemy,
          actionOnGet: OnEnemyGet,
          actionOnRelease: OnEnemyRelease,
          actionOnDestroy: OnEnemyDestroy,
          collectionCheck: true,
          defaultCapacity: defaultCapacity,
          maxSize: maxSize);
    }

    public IEnemy Create(Vector3 position)
    {
      Enemy enemy = _pool.Get();
      enemy.transform.SetPositionAndRotation(position, Quaternion.identity);
      enemy.gameObject.SetActive(true);

      return enemy;
    }

    public void Dispose()
    {
      _pool.Dispose();
    }

    private Enemy InstantiateEnemy()
    {
      Enemy enemy = UnityEngine.Object.Instantiate(_enemyPrefab, _root);
      enemy.gameObject.SetActive(false);
      enemy.Initialize(_pool.Release);

      return enemy;
    }

    private void OnEnemyGet(Enemy enemy)
    {
      enemy.ResetState();
    }

    private void OnEnemyRelease(Enemy enemy)
    {
      enemy.gameObject.SetActive(false);
    }

    private void OnEnemyDestroy(Enemy enemy)
    {
      if (enemy != null)
      {
        UnityEngine.Object.Destroy(enemy.gameObject);
      }
    }
  }
}