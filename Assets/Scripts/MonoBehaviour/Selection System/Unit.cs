using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    public int currentHP;
    
    [SerializeField] private int _maxHP;
    public int maxHp => _maxHP;
    
    [SerializeField] private string _unitName;
    public string unitName => _unitName;

    [SerializeField] private UnitType _unitType;
    public UnitType unitType => _unitType;

    public float attackRate;
    public float deffenceRate;

    public float timeToCreate;
    
    private UnitSelections UnitSelections => UnitSelections.unitSelections;

    private void Start() => UnitSelections.unitList.Add(gameObject);

    private void OnDestroy() => UnitSelections.unitList.Remove(gameObject);
}