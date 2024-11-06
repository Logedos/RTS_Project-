using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fsm
{
    public sealed class Fsm
    {
        private readonly Dictionary<Type, State> _states = new();
        
        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }
        
        public void AddState<T>(T state) where T : State
        {
            Type stateType = state.GetType();
            
            if (!_states.TryAdd(stateType, state))
                throw new NullReferenceException("The state already exists");
        }

        public void RemoveState<T>()
        {
            Type stateType = typeof(T);
            
            if (!_states.Remove(stateType))
                throw new NullReferenceException("The state to remove does not exist");
        }
        
        public void SetInitialState<T>() where T : State
        {
            if (CurrentState != null) return;
            
            Type stateType = typeof(T);
            
            CurrentState = _states[stateType];
            CurrentState.Enter();
        }
        
        public void SetState<T>() where T : State
        {
            Type currentStateType = CurrentState.GetType();
            Type nextStateType = typeof(T);
            
            if (currentStateType == nextStateType) return;

            CurrentState.Exit();
            PreviousState = CurrentState;
            
            CurrentState = _states[nextStateType];
            CurrentState.Enter();
        }
        
        public void SetState(Type nextStateType)
        {
            Type currentStateType = CurrentState.GetType();
            
            if (currentStateType == nextStateType) return;

            CurrentState.Exit();
            PreviousState = CurrentState;
            
            CurrentState = _states[nextStateType];
            CurrentState.Enter();
        }

        public void Update() => CurrentState.Update();
        
        public void OnCollisionEnter(Collision other) => CurrentState.OnCollisionEnter(other);
        public void OnCollisionStay(Collision other) => CurrentState.OnCollisionStay(other);
        public void OnCollisionExit(Collision other) => CurrentState.OnCollisionExit(other);
        
        public void OnTriggerEnter(Collider other) => CurrentState.OnTriggerEnter(other);
        public void OnTriggerStay(Collider other) => CurrentState.OnTriggerStay(other);
        public void OnTriggerExit(Collider other) => CurrentState.OnTriggerExit(other);
    }
}