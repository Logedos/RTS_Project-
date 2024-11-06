using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BarrackUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> unitUIListAll; 
    [SerializeField] private List<GameObject> unitListAll; 

    public SpawnController CurrentSpawnController;

    [SerializeField] private GameplayUIManager _gameplayUIManager;
    
    private void Awake()
    {
        
    }

    public void UpdateUnitUIList()
    {
        CurrentSpawnController.unitUIList = new List<GameObject>();

        for (int i = 0; i < unitListAll.Count; i++)
        {
            for (int j = 0; j < CurrentSpawnController.unitToSpawnList.Count; j++)
            {
                if (unitListAll[i].GetComponent<Unit>().unitType ==
                    CurrentSpawnController.unitToSpawnList[j].GetComponent<Unit>().unitType)
                {
                    Debug.Log("unitUIList.Add");
                    CurrentSpawnController.unitUIList.Add(unitUIListAll[i]);
                }
            }
        }
    }

   /* public void EnableBarrackUI()
    {
        _gameplayUIManager.OnOpenBarrackUI();
    }

    public void DisableBarrackUI()
    {
        _gameplayUIManager.OnDisableBarrackUI();
    }*/
}