using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    private Camera camera;

    [SerializeField] private RectTransform boxVisual;
    private Rect selectionBox;

    private Vector2 startPosition;
    private Vector2 endPosition;

    private UnitSelections UnitSelections => UnitSelections.unitSelections;

    [SerializeField] private GameplayUIManager _gameplayUIManager;

    private void Start()
    {
        camera = Camera.main;

        startPosition = Vector2.zero;
        endPosition = Vector2.zero;

        DrawVisual();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;

            selectionBox = new Rect();
        }

        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;

            DrawVisual();
            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;

            DrawVisual();
            SelectUnits();
            
            //_gameplayUIManager.ActivateFormationUI();
        }
    }

    private void DrawVisual()
    {
        Vector2 boxCenterPos = (startPosition + endPosition) / 2;

        boxVisual.position = boxCenterPos;

        Vector2 boxSize = new Vector2(Mathf.Abs(endPosition.x - startPosition.x), Mathf.Abs(endPosition.y - startPosition.y));
        boxVisual.sizeDelta = boxSize;
    }

    private void DrawSelection()
    {
        if (Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }

        else
        {
            selectionBox.xMax = Input.mousePosition.x;
            selectionBox.xMin = startPosition.x;
        }

        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }

        else
        {
            selectionBox.yMax = Input.mousePosition.y;
            selectionBox.yMin = startPosition.y;
        }
    }

    private void SelectUnits()
    {
        foreach (GameObject unit in UnitSelections.unitList)
        {
            if (selectionBox.Contains(camera.WorldToScreenPoint(unit.transform.position)))
                UnitSelections.DragSelect(unit);
        }
    }
}

