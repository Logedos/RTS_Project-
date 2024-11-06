using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProccesBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject buildingProccesShowerPrefab;
    [SerializeField] private GameObject buildingPrefab;

    public void Build()
    {
        buildingProccesShowerPrefab.SetActive(false);
        buildingPrefab.SetActive(true);
        
        CheckBuildingType();
    }
    
    /// <summary>
    /// This metod check building type and manage it in game
    /// </summary>
    private void CheckBuildingType()
    {
        try
        {
            Transform ecomonicComponent = this.transform.Find("EconomicElement");
            if (ecomonicComponent.TryGetComponent<MiningBuilding>(out MiningBuilding miningBuildingComponent))
            {
                GameHandler.AddRes += miningBuildingComponent.AddRes;
            }
            else if(ecomonicComponent.TryGetComponent<EconomicBuilding>(out EconomicBuilding economicBuildingComponent))
            {
                economicBuildingComponent.PerformAction();
                
                if(economicBuildingComponent is BarrackBehaviour)
                    if (ecomonicComponent.parent.GetChild(0).TryGetComponent(out SpawnController controller))
                    {
                        Debug.Log("Current spawn controller was setted");
                        controller.SetSpawnController();
                    }

            }

            if (this.TryGetComponent(out BoxCollider collider))
                collider.enabled = true;
        }
        catch (Exception exception)
        {
            Debug.Log("EconomicElement was't found!");
        }
    }
}
