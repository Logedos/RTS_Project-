using UnityEngine;

namespace Fsm
{
    public abstract class State
    {
        private readonly Fsm _fsm;

        protected State CurrentState => _fsm.CurrentState;
        protected State PreviousState => _fsm.PreviousState;
        
        protected State(Fsm fsm)
        {
            _fsm = fsm;
        }

        public void SetState<T>() where T : State => _fsm.SetState<T>();
        
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
        
        public virtual void OnCollisionEnter(Collision other) { }
        public virtual void OnCollisionStay(Collision other) { }
        public virtual void OnCollisionExit(Collision other) { }
        
        public virtual void OnTriggerEnter(Collider other) { }
        public virtual void OnTriggerStay(Collider other) { }
        public virtual void OnTriggerExit(Collider other) { }
    }
}