using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public List<GameObject> buildingsSelected = new List<GameObject>();
    
    public static UnitSelections unitSelections;

    public ObjectType selectedObjectType;
    [SerializeField] private GameplayUIManager _gameplayUIManager;
    private void Awake() => unitSelections = (UnitSelections) FindObjectOfType(typeof(UnitSelections));
    
    public static EcsEntity Entity(GameObject unit)
    {
        return unit.GetComponent<EntityReference>().entity;
    }

    private void Select(GameObject objectToAdd, List<GameObject> typeList)
    {
        //Debug.Log("Select!");
        typeList.Add(objectToAdd);

        SelectionRing.EnableSelectionRing(objectToAdd);

        if (DetermineObjectType(objectToAdd) == ObjectType.Building)
        {
            _gameplayUIManager.OnOpenBuildingUI(typeList[0]);
            selectedObjectType = ObjectType.Building;
            return;
        }
        if (DetermineObjectType(objectToAdd) == ObjectType.Unit)
        {
            _gameplayUIManager.OnOpenFormationUI();
            selectedObjectType = ObjectType.Unit;
        }
        
        Debug.Log(objectToAdd.name); 
        Entity(objectToAdd).Get<OnSelected>();
    }

    private ObjectType DetermineObjectType (GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Unit unit)) 
            return ObjectType.Unit;
        if(gameObject.TryGetComponent(out Building building))
            return ObjectType.Building;
        
        return ObjectType.Empty;
    }

    private bool SelectedOtherType(List<GameObject> typeList)
    {
        if (typeList.Count == 0)
            return false;
        
        return true;
    }

    public void ClickSelect(GameObject objectToAdd)
    {
        DeselectAll();
        
        if (DetermineObjectType(objectToAdd) == ObjectType.Unit)
            Select(objectToAdd, unitsSelected);
        else
            Select(objectToAdd, buildingsSelected);
    }

    public void ShiftClickSelect(GameObject objectToAdd)
    {
        if (DetermineObjectType(objectToAdd) == ObjectType.Building)
        {
            if (buildingsSelected.Contains(objectToAdd))
                Deselect(objectToAdd, buildingsSelected);
            
            return;
        }

        if (SelectedOtherType(buildingsSelected)) 
            return;

        if (!unitsSelected.Contains(objectToAdd))
            Select(objectToAdd, unitsSelected);

        else
            Deselect(objectToAdd, unitsSelected);
    }
    
    public void DragSelect(GameObject unitToAdd)
    {
        if (SelectedOtherType(buildingsSelected))
            return;

        if (!unitsSelected.Contains(unitToAdd)) 
            Select(unitToAdd, unitsSelected);
    }
    
    public void DeselectAll()
    {
        foreach (GameObject unit in unitsSelected)
        {
            SelectionRing.DisableSelectionRing(unit);
            Entity(unit).Del<OnSelected>();
        }
        
        unitsSelected.Clear();

        if (buildingsSelected.Count != 0)
        {
            SelectionRing.DisableSelectionRing(buildingsSelected[0]);
            _gameplayUIManager.OnDisableBuildingUI();
            
            buildingsSelected.Clear();
            return;
        }
        
        _gameplayUIManager.OnDisableFormationUI();
            
    }
    
    private void Deselect(GameObject objectToDeselect, List<GameObject> typeList)
    {
        SelectionRing.DisableSelectionRing(objectToDeselect);

        typeList.Remove(objectToDeselect);
        selectedObjectType = ObjectType.Empty;

        Debug.Log(DetermineObjectType(objectToDeselect));
        
        if (DetermineObjectType(objectToDeselect) == ObjectType.Building)
            return;

        //_gameplayUIManager.OnDisableFormationUI();
        
        Entity(objectToDeselect).Del<OnSelected>();
    }

    //созданы для работы с UI
    public void SelectUnit(GameObject objectToAdd, List<GameObject> typeList) => Select(objectToAdd, typeList);
    public void DeselectUnit(GameObject objectToAdd, List<GameObject> typeList) => Deselect(objectToAdd, typeList);
}

public enum ObjectType : byte
{
    Unit,
    Building,
    Empty
}

