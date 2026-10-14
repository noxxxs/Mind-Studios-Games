using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WaveSpawner.Enemies
{
  [RequireComponent(typeof(Collider))]
  public class Enemy : MonoBehaviour, IEnemy, IPointerClickHandler
  {
    private Action<Enemy> _release;
    private bool _isDead;

    public event Action<IEnemy> Died;

    public void Initialize(Action<Enemy> release)
    {
      _release = release;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (eventData.button != PointerEventData.InputButton.Left)
      {
        return;
      }

      Kill();
    }

    public void Kill()
    {
      if (_isDead)
      {
        return;
      }

      _isDead = true;

      Died?.Invoke(this);
      _release?.Invoke(this);
    }

    public void ResetState()
    {
      _isDead = false;
    }
  }
}