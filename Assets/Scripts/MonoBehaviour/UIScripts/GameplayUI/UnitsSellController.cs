using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsSellController : MonoBehaviour
{
    [SerializeField] private UnitSelections _unitSelections;
    [SerializeField] private List<GameObject> selectedUnitShower;
    [SerializeField] private GameplayUIManager _gameplayUIManager;

    public void OnBashButtonClick()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            DeselectByUnitType(UnitType.Bash);
            selectedUnitShower[0].SetActive(false);
        }
        else
        {
            SelectByUnitType(UnitType.Bash);
            selectedUnitShower[1].SetActive(false);
            selectedUnitShower[2].SetActive(false);
        }
    }

    public void OnArcherButtonClick()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            DeselectByUnitType(UnitType.Archer);
            selectedUnitShower[1].SetActive(false);
        }
        else
        {
            SelectByUnitType(UnitType.Archer);
            selectedUnitShower[0].SetActive(false);
            selectedUnitShower[2].SetActive(false);
        }
    }
    
    public void OnMageButtonClick()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            DeselectByUnitType(UnitType.Mage);
            selectedUnitShower[2].SetActive(false);
        }
        else
        {
            SelectByUnitType(UnitType.Mage);
            selectedUnitShower[0].SetActive(false);
            selectedUnitShower[1].SetActive(false);
        }
    }

    private void SelectByUnitType(UnitType unitType)
    {
        List<GameObject> unitsSelectedClone = new List<GameObject>();
        
        foreach (GameObject unit in _unitSelections.unitsSelected)
            unitsSelectedClone.Add(unit);
        
        _unitSelections.DeselectAll();
        _gameplayUIManager.OnOpenFormationUI();
        
        foreach (GameObject unit in unitsSelectedClone)
        {
            if (unit.TryGetComponent(out Unit unitComponent))
                if (unitComponent.unitType == unitType)
                    _unitSelections.SelectUnit(unit, _unitSelections.unitsSelected);
        }
    }
    private void DeselectByUnitType(UnitType unitType)
    {
        List<GameObject> unitsSelectedClone = new List<GameObject>();
        
        foreach (GameObject unit in _unitSelections.unitsSelected)
            unitsSelectedClone.Add(unit);
        
        foreach (GameObject unit in unitsSelectedClone)
        {
            if(unit.TryGetComponent(out Unit unitComponent))
                if (unitComponent.unitType == unitType)
                    _unitSelections.DeselectUnit(unit, _unitSelections.unitsSelected);
        }
    }
}
