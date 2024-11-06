using UnityEngine;

public struct UnitElement
{
    public GameObject UiElement;
    public UnitType unitType;

    public UnitElement(GameObject UiElement, UnitType unitType)
    {
        this.UiElement = UiElement;
        this.unitType = unitType;
    }
}