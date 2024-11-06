using System;
using UnityEngine;

namespace Common.Components
{
    public sealed class HealthComponent : MonoBehaviour, IDamageable
    {
        public event Action OnDeath;
        public event Action<int> OnTakeDamage;
        
        [SerializeField] 
        private int _hitPoints;
        
        [SerializeField]
        private int _minHitPoints;
        
        public void TakeDamage(int damage)
        {
            _hitPoints = Mathf.Max(_minHitPoints, _hitPoints - damage);
            OnTakeDamage?.Invoke(damage);
            
            if (_hitPoints <= _minHitPoints)
                OnDeath?.Invoke();
        }
    }
}