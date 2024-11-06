using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform startDestination;
    public List<GameObject> unitToSpawnList; // types that we can produce
    [HideInInspector] public List<GameObject> unitUIList; 
    
    public Dictionary<UnitType, int> unitsToDo;
    
    private BarrackUIController _barrackUIController;

    private bool _isBusy = false;
    
    private void Awake()
    {
        InitUnitsQueue();
        _barrackUIController = GetBarrackUIController();
        SetSpawnController();
        _barrackUIController.UpdateUnitUIList();
    }
    public void SetSpawnController() => _barrackUIController.CurrentSpawnController = this;

    private void InitUnitsQueue()
    {
        unitsToDo = new();

        foreach (var unit in unitToSpawnList)
            unitsToDo.Add(GetUnitType(unit), 0);
        
    }
    
    private void OnEnable()
    {
        SetSpawnController();
    }

    private void OnDisable()
    {
        _barrackUIController.CurrentSpawnController = null;
    }
    
    private UnitType _currentUnitType = UnitType.Bash;
    private void Update()
    {
        if(!isUnitQueueContainElements())
                return;
        
        Debug.Log("UnitQueueContainElements");
        if (unitsToDo[_currentUnitType] != 0)
        {
            Debug.Log("UnitQueueContainElements: " + _currentUnitType);
            if (_isBusy == false)
            {
                unitsToDo[_currentUnitType]--;

                int indexOfunitToSpawn = GetIndexOfUnitInList(_currentUnitType);

                GameObject unitToSpawn = unitToSpawnList[indexOfunitToSpawn];
                StartCoroutine(CreateUnit(unitToSpawn));
            }
            else
                return;
        }
        else
            _currentUnitType = MoveNext(_currentUnitType);
    }

    private UnitType MoveNext(UnitType currentUnitType) // getting next unitType in enum
    {
        int index = GetIndexOfUnitInList(currentUnitType);
        
        if (index != unitToSpawnList.Count - 1)
            return GetUnitType(unitToSpawnList[++index]);
        else
            return GetUnitType(unitToSpawnList[0]);
    }

    private int GetIndexOfUnitInList(UnitType unitType)
    {
        int indexOfunitToSpawn = 0;
        
        foreach (GameObject unitPrefab in unitToSpawnList)
            if (GetUnitType(unitPrefab) == unitType)
                indexOfunitToSpawn = unitToSpawnList.IndexOf(unitPrefab);

        return indexOfunitToSpawn;
    }
    
    
    private bool isUnitQueueContainElements()
    {
        foreach (var unitsQueue in unitsToDo)
        {
            if (unitsQueue.Value != 0)
                return true;
        }

        return false;
    }

    IEnumerator Counter(float secs, int index)
    {
        float temp = 0;
        while (temp <= secs)
        {
            unitUIList[index].transform.GetChild(3).GetComponent<Image>().fillAmount = temp / secs;
            Debug.Log(temp / secs);
            
            yield return new WaitForSeconds(0.1f);
            temp += 0.1f;
        }
    }
    IEnumerator CreateUnit(GameObject unitToSpawn)
    {
        _isBusy = true;

        int index = 0;

        for (int i = 0; i < unitUIList.Count; i++)
            if (GetUnitType(unitToSpawnList[i]) == GetUnitType(unitToSpawn))
                index = GetIndexOfUnitInList(GetUnitType(unitToSpawnList[i]));

        StartCoroutine(Counter(GetUnitCreationTime(unitToSpawn), index));
        unitUIList[index].transform.GetChild(2).gameObject.SetActive(true);
        unitUIList[index].transform.GetChild(3).gameObject.SetActive(true);
        
        yield return new WaitForSeconds(GetUnitCreationTime(unitToSpawn));
        
        unitUIList[index].transform.GetChild(2).gameObject.SetActive(false);
        unitUIList[index].transform.GetChild(3).gameObject.SetActive(false);
        
        UnitSpawner.SpawnUnit(unitToSpawn, spawnPoint.position, startDestination.position);
        _isBusy = false;
        
        unitUIList[index].transform.GetChild(1).GetComponent<TMP_Text>().text =
            (int.Parse(unitUIList[index].transform.GetChild(1).GetComponent<TMP_Text>().text) - 1).ToString();
    }
    
    private float GetUnitCreationTime(GameObject unit)
    {
        if (unit.TryGetComponent(out Unit unitComponent))
        {
            return unitComponent.timeToCreate;
        }
        return 0;
    }
    
    private UnitType GetUnitType(GameObject unit)
    {
        if (unit.TryGetComponent(out Unit unitComponent))
        {
            return unitComponent.unitType;
        }
        return UnitType.None;
    }

    private BarrackUIController GetBarrackUIController() =>
        GameObject.Find("Managers").GetComponentInChildren<BarrackUIController>();
}
