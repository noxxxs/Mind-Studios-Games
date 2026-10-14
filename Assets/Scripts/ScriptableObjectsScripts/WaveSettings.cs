using System.Collections.Generic;
using UnityEngine;

namespace WaveSpawner.Waves
{
  [CreateAssetMenu(fileName = "WaveSettings", menuName = "Wave Spawner/Wave Settings")]
  public class WaveSettings : ScriptableObject
  {
    [SerializeField] private List<WaveData> _waves = new() { new WaveData(), new WaveData(), new WaveData() };

    public IReadOnlyList<WaveData> Waves => _waves;
  }
}