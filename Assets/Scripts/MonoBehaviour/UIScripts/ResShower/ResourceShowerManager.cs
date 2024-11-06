using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceShowerManager : MonoBehaviour
{
   
   
   [SerializeField] private TMP_Text stoneUI;
   [SerializeField] private TMP_Text woodUI;
   [SerializeField] private TMP_Text foodUI;
   [SerializeField] private TMP_Text goldUI;
   [SerializeField] private TMP_Text populationUI;
   [SerializeField] private TMP_Text ambarUI;

   private static TMP_Text _stoneUI;
   private static TMP_Text _woodUI;
   private static TMP_Text _foodUI;
   private static TMP_Text _goldUI; 
   private static TMP_Text _populationUI; 
   private static TMP_Text _ambarUI; 

   private void Start()
   {
      InitResShowers();
      
      UpdateResInfo(); // обновление состояния ремурсов в начале игры
      GameHandler.TimeToAddRes += UpdateResInfo;
   }

   public static void UpdateResInfo()
   {
      Storage storage = JsonSaver.GetResources();
      int currentResourcesAmount = 0;
      
      foreach (var resource in storage.resourcesList)
      {
         int oldValue = 0;
         if (resource.resType == ResType.Stone)
         {
            oldValue = int.Parse(_stoneUI.text);
            _stoneUI.text = resource.currentAmount.ToString();
            ShowResChange(ResType.Stone, resource.currentAmount - oldValue);

            currentResourcesAmount += resource.currentAmount;
         }
         else if (resource.resType == ResType.Wood)
         {
            oldValue = int.Parse(_woodUI.text);
            _woodUI.text = resource.currentAmount.ToString();
            ShowResChange(ResType.Wood, resource.currentAmount - oldValue);
            
            currentResourcesAmount += resource.currentAmount;
         }
         else if (resource.resType == ResType.Food)
         {
            oldValue = int.Parse(_foodUI.text);
            _foodUI.text = resource.currentAmount.ToString();
            ShowResChange(ResType.Food, resource.currentAmount - oldValue);
            
            currentResourcesAmount += resource.currentAmount;
         }
         else if (resource.resType == ResType.Gold)
         {
            oldValue = int.Parse(_goldUI.text);
            _goldUI.text = resource.currentAmount.ToString();
            ShowResChange(ResType.Gold, resource.currentAmount - oldValue);
            
            currentResourcesAmount += resource.currentAmount;
         }
         else if(resource.resType == ResType.Population)
         {
            oldValue = int.Parse(_populationUI.text.Split(" ")[0]);
            _populationUI.text = $"{resource.currentAmount.ToString()} / {storage.maxPopulation}";
            ShowResChange(ResType.Population, resource.currentAmount - oldValue);
         }
      }

      _ambarUI.text = $"{currentResourcesAmount} / {storage.maxAmount}";
   }

   private void InitResShowers()
   {
      _stoneUI = stoneUI;
      _foodUI = foodUI;
      _goldUI = goldUI;
      _woodUI = woodUI;
      _ambarUI = ambarUI;
      _populationUI = populationUI;
   }

   private static void ShowResChange(ResType resType, int value)
   {
      if (value > 0)
      {
         ChangeResValueManager.VisualizeResChanging(resType,value,
            ChangeResValueManager.OperationType.Add);
      }
      else if(value < 0)
      {
         ChangeResValueManager.VisualizeResChanging(resType,value,
            ChangeResValueManager.OperationType.Subtract);
      }
   }
}
