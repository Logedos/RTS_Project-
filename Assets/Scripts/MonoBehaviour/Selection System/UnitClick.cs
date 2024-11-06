using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitClick : MonoBehaviour
{
    private Camera camera;

    public LayerMask clickable;

    private UnitSelections UnitSelections => UnitSelections.unitSelections;
    
    private void Start() => camera = Camera.main;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    UnitSelections.ShiftClickSelect(hitInfo.transform.gameObject);
                else 
                    UnitSelections.ClickSelect(hitInfo.transform.gameObject);
            }
            else if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                    UnitSelections.DeselectAll();
            }
        }
    }
}