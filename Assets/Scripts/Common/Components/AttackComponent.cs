using UnityEngine;

namespace Common.Components
{
    public sealed class AttackAgentComponent : MonoBehaviour
    {
        [SerializeField]
        private int _damage;

        public void Attack(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);
        }
    }
}