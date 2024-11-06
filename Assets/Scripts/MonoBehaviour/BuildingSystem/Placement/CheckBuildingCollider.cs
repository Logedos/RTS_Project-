using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuildingCollider : MonoBehaviour
{
    [SerializeField] private BuildingManager buildingManager;
    [SerializeField] private string buildingTag;

    [SerializeField] private GameObject canPlaceShower;
    [SerializeField] private Material canPlaceMaterial;
    [SerializeField] private Material canNotPlaceMaterial;

    private void Start()
    {
        buildingManager = GameObject.Find("BuildingSystemManager").GetComponent<BuildingManager>();
    }

    private void Update()
    {
        if (buildingManager.CanPlace && canPlaceShower is not null)
        {
            canPlaceShower.GetComponent<MeshRenderer>().material = canPlaceMaterial;
        }
        else if(!buildingManager.CanPlace && canPlaceShower is not null)
        {
            canPlaceShower.GetComponent<MeshRenderer>().material = canNotPlaceMaterial;
        }

        if (buildingManager.isPlaced)
        {
            canPlaceShower = null;

            buildingManager.isPlaced = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag(buildingTag))
            buildingManager.CanPlace = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(buildingTag)) 
            buildingManager.CanPlace = true;
    }
}
