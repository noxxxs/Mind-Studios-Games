using UnityEngine;

namespace WaveSpawner.Enemies
{
  public class EnemySpawner : MonoBehaviour, IEnemySpawner
  {
    [SerializeField] private Vector3 _spawnAreaSize = new(10f, 0f, 6f);

    private IEnemyFactory _enemyFactory;

    public void Initialize(IEnemyFactory enemyFactory)
    {
      _enemyFactory = enemyFactory;
    }

    public IEnemy Spawn()
    {
      return _enemyFactory.Create(GetRandomPosition());
    }

    private Vector3 GetRandomPosition()
    {
      Vector3 half = _spawnAreaSize * 0.5f;
      var localPosition = new Vector3(
          Random.Range(-half.x, half.x),
          Random.Range(-half.y, half.y),
          Random.Range(-half.z, half.z));

      return transform.TransformPoint(localPosition);
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.red;
      Gizmos.matrix = transform.localToWorldMatrix;
      Gizmos.DrawWireCube(Vector3.zero, _spawnAreaSize);
    }
  }
}