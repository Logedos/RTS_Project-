using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddUnitsToSpawnButton : MonoBehaviour
{
   [SerializeField] private UnitType UnitTypeToAdd;
   [SerializeField] private BarrackUIController _barrackUIController;

   public void AddUnitToDoList()
   {
       _barrackUIController.CurrentSpawnController.unitsToDo[UnitTypeToAdd] += 1;
       
       gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text =
           (int.Parse(gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text) + 1).ToString();
   }
}
