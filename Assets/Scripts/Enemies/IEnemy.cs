using System;

namespace WaveSpawner.Enemies
{
  public interface IEnemy
  {
    event Action<IEnemy> Died;
  }
}