using TMPro;
using UnityEngine;
using WaveSpawner.Enemies;
using WaveSpawner.Waves;

namespace WaveSpawner.UI
{
  public class WaveHud : MonoBehaviour
  {
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text _enemiesText;
    [SerializeField] private GameObject _gameCompletePanel;

    private WaveController _waveController;
    private AliveEnemiesTracker _tracker;

    public void Initialize(WaveController waveController, AliveEnemiesTracker tracker)
    {
      _waveController = waveController;
      _tracker = tracker;
      _gameCompletePanel.SetActive(false);

      _waveController.WaveStarted += HandleWaveStarted;
      _waveController.AllWavesCompleted += HandleAllWavesCompleted;
      _tracker.CountChanged += HandleAliveEnemiesChanged;

      HandleWaveStarted(_waveController.CurrentWaveNumber, _waveController.TotalWaves);
      HandleAliveEnemiesChanged(_tracker.Count);
    }

    private void OnDestroy()
    {
      if (_waveController != null)
      {
        _waveController.WaveStarted -= HandleWaveStarted;
        _waveController.AllWavesCompleted -= HandleAllWavesCompleted;
      }

      if (_tracker != null)
      {
        _tracker.CountChanged -= HandleAliveEnemiesChanged;
      }
    }

    private void HandleWaveStarted(int waveNumber, int totalWaves)
    {
      _waveText.text = $"Wave: {waveNumber}/{totalWaves}";
    }

    private void HandleAliveEnemiesChanged(int aliveEnemies)
    {
      _enemiesText.text = $"Enemies: {aliveEnemies}";
    }

    private void HandleAllWavesCompleted()
    {
      _gameCompletePanel.SetActive(true);
    }
  }
}