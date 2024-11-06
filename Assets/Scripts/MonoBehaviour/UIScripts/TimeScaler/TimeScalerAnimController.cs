using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeScalerAnimController : MonoBehaviour
{
    enum TimeScalerState
    {
        Active,
        NonActive
    }
    
    [SerializeField] private string showTriggerName = "Show";
    [SerializeField] private string hideTriggerName = "Hide";
    
    private TimeScalerState _state = TimeScalerState.NonActive;
    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }
    
    public void ChangeTimeScalerState()
    {
        if (_state == TimeScalerState.Active)
        {
            _animator.SetTrigger(hideTriggerName);
            _state = TimeScalerState.NonActive;
        }
        else if(_state == TimeScalerState.NonActive)
        {
            _animator.SetTrigger(showTriggerName);
            _state = TimeScalerState.Active;
        }
    }
}
