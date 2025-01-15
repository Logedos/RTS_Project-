using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Unity.AI.Navigation;
using System.Threading;
public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Grid grid;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    
    
    [SerializeField] private ObjectsData database;
    private int selectedObjectIndex;

    [SerializeField] private GameObject gridVisualization;

    private GridData buildings;
    private List<GameObject> placedGamaObject = new List<GameObject>();

    [SerializeField] private PreviewSystem previewSystem;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;
    private UnitSelections UnitSelections => UnitSelections.unitSelections;

    [SerializeField] private Transform buildingsPool;
    [SerializeField] private TimeManager timeManager;
    
    private GameObject currentBuilding;
    private void Start()
    {
        StopPlacement();
        buildings = new GridData();
    }

    public void StartPlacement(int id)
    { 
        StopPlacement();
        UnitSelections.DeselectAll();
        selectedObjectIndex = database.objectDatas.
            FindIndex(data => data.ID == id);
        if (selectedObjectIndex < 0)
        {
            Debug.Log($"No ID found {id}");
            return;
        }
        gridVisualization.SetActive(true);
        
       previewSystem.StartShowingPlacementPreview((database.objectDatas[selectedObjectIndex].Prefab),
           database.objectDatas[selectedObjectIndex].Size);
       
        inputManager.OnClicked += PlaceBuild;
        inputManager.OnExit += StopPlacement;
    }
    private void PlaceBuild()
    {
        if (inputManager.IsPointerOverUI)
        {
            return;
        }
        Vector3 mouserPos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mouserPos);

        bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
        if (!placementValidity)
            return;
        
        GameObject gameBuilding = Instantiate(database.objectDatas[selectedObjectIndex].Prefab, buildingsPool);
        currentBuilding = gameBuilding;
        gameBuilding.transform.position = grid.CellToWorld(gridPos);
        placedGamaObject.Add(gameBuilding);
        
        timeManager.buildingsToBuild.Add(gameBuilding);

        buildings.AddOblectAt(gridPos, database.objectDatas[selectedObjectIndex].Size,
            database.objectDatas[selectedObjectIndex].ID, placedGamaObject.Count - 1);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPos), database.objectDatas[selectedObjectIndex].Size, false);

        gameBuilding.layer = 6; // set Clickable layer

        _navMeshSurface.BuildNavMesh(); // updating navMesh
        
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            StopPlacement();
        }
       // gameBuilding.GetComponent<MeshRenderer>().enabled = true;
        
        Storage storage = JsonSaver.GetResources();

        storage.resourcesList[0].currentAmount -= 50;
        storage.resourcesList[1].currentAmount -= 50;
        storage.resourcesList[3].currentAmount -= 20;
        
        JsonSaver.SaveResources(storage);
        
        ResourceShowerManager.UpdateResInfo();
    }

    private bool CheckPlacementValidity(Vector3Int gridPos, int i)
    {
        return buildings.CanPlaceObjectAt(gridPos, database.objectDatas [i].Size);
    }

    private void StopPlacement()
    {
        if (selectedObjectIndex != -1)
            previewSystem.StopShowingPreview(currentBuilding);
        
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        inputManager.OnClicked -= PlaceBuild;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }
    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        
        Vector3 mouserPos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mouserPos);

        if (lastDetectedPosition != gridPos)
        { 
            bool placementValidity = CheckPlacementValidity(gridPos, selectedObjectIndex);
            
            mouseIndicator.transform.position = mouserPos;
            previewSystem.UpdatePosition(grid.CellToWorld(gridPos), database.objectDatas[selectedObjectIndex].Size, placementValidity);
            lastDetectedPosition = gridPos;
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopPlacement();
        }
    }
}
