using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public bool CanPlace = true;
    public bool isPlaced = false;
    
    [SerializeField] private GameObject[] buildings;
    [SerializeField] private LayerMask layerMask;

    private GameObject selectedBuild;
    private Vector3 spawnPlace;
    void Update()
    {
        if (selectedBuild is not null)
        {
            selectedBuild.transform.position = spawnPlace;

            if (Input.GetMouseButton(0) && CanPlace && !EventSystem.current.IsPointerOverGameObject())
            {
                selectedBuild = null;
                isPlaced = true;
            }

            if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject())
            {
                GameObject gameObject = selectedBuild;
                Destroy(gameObject);

                selectedBuild = null;
            }
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray,out hit,1000,layerMask))
        {
            spawnPlace = hit.point;
        }
    }

    public void SelectBuildings(int index)
    {
        if (selectedBuild is null)
        {
            selectedBuild = Instantiate(buildings[index]);
        }
    }
}
