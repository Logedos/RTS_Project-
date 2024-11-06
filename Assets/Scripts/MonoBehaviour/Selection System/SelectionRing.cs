using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionRing : MonoBehaviour
{
    private void Start() => gameObject.SetActive(false);

    public static GameObject GetSelectionRing(GameObject parent)
    {
        return parent.GetComponentInChildren<SelectionRing>(true).gameObject;
    }

    public static void EnableSelectionRing(GameObject parent) => GetSelectionRing(parent).SetActive(true);

    public static void DisableSelectionRing(GameObject parent) => GetSelectionRing(parent).SetActive(false);
}
