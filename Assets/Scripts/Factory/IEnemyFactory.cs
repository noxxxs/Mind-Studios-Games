using UnityEngine;

namespace WaveSpawner.Enemies
{
  public interface IEnemyFactory
  {
    IEnemy Create(Vector3 position);
  }
}