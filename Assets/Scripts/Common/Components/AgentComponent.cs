using System;
using UnityEngine;
using UnityEngine.AI;

namespace Common.Components
{
    public sealed class AgentComponent : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _agent;
        
        private float _sqrStoppingDistance;
        public Transform Target;

        public float RemainingDistance => _agent.remainingDistance;

        private void Awake()
        {
            float stoppingDistance = _agent.stoppingDistance;
            _sqrStoppingDistance = stoppingDistance * stoppingDistance;
        }

        public void SetDestination()
        {
            _agent.SetDestination(Target.position);
        }

        public bool IsDestinationReached()
        {
            Vector3 targetPosition = _agent.destination;
            float remainingDistance = _agent.remainingDistance;
            float sqrRemainingDistance = remainingDistance * remainingDistance;
            
            if (sqrRemainingDistance > _sqrStoppingDistance) return false;

            return true;
        }
        
        public void FreezeAgent()
        {
            _agent.isStopped = true;
        }

        public void UnfreezeAgent()
        {
            _agent.isStopped = false;
        }
    }
}