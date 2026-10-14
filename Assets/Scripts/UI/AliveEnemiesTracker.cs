using System;

namespace WaveSpawner.Enemies
{
  public class AliveEnemiesTracker
  {
    public event Action<int> CountChanged;

    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    public void Track(IEnemy enemy)
    {
      enemy.Died += HandleEnemyDied;
      SetCount(Count + 1);
    }

    private void HandleEnemyDied(IEnemy enemy)
    {
      enemy.Died -= HandleEnemyDied;
      SetCount(Count - 1);
    }

    private void SetCount(int value)
    {
      Count = value;
      CountChanged?.Invoke(Count);
    }
  }
}