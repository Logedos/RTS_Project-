using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    enum UIState : byte
    {
        Active,
        NonActive
    }

    [SerializeField] private GameObject unitInfoUI;
    [SerializeField] private GameObject constructionButton;
    [SerializeField] private GameObject activeConstructionUI;
    [SerializeField] private GameObject nonActiveConstructionUI;
    
    [SerializeField] private GameObject marketButton;
    [SerializeField] private GameObject marketUI;

    [SerializeField] private GameObject miningBuildingUI;
    [SerializeField] private Button addWorkersButton;
    [SerializeField] private Button removeWorkerButton;

    [SerializeField] private GameObject ambarUI;
    
    [SerializeField] private GameObject formationUI;
    [SerializeField] private List<GameObject> selectedUnitShower;
    
    [SerializeField] private Sprite activeCell;
    [SerializeField] private Sprite nonActiveCell;

    [SerializeField] private UnitSelections selectedObjects;
    [SerializeField] private Transform buildingPool;

    [SerializeField] private GameObject barrackUI;
    
    private void Start()
    {
        constructionButton.GetComponent<Button>().onClick.AddListener(InteractWithConstructionUI);
        marketButton.GetComponent<Button>().onClick.AddListener(InteractWithMarketUI);
    }

    private void InteractWithConstructionUI()
    {
        if(constructionButton.GetComponent<Button>().GetComponent<Image>().sprite == activeCell)
            OnDisableConstructionUI();
        else
            OnOpenConstructionUI();
    }

    private void InteractWithMarketUI()
    {
        if(marketButton.GetComponent<Button>().GetComponent<Image>().sprite == activeCell)
            OnDisableMarketUI();
        else
            OnOpenMarketUI();
    }
    
    private void OnOpenMarketUI()
    {
        SetStateForFormationUI(UIState.NonActive);
        CloseBuildingUI();
        SetStateForMarketUI(UIState.Active);
        CloseBarrackUI();
        SetNonActiveConstructionUI();
        
    }

    private void OnDisableMarketUI()
    {
        SetStateForMarketUI(UIState.Active);
        CLoseUnitInfoUI();
    }

    public void OnOpenFormationUI()
    {
        SetStateForMarketUI(UIState.NonActive);
        CloseBuildingUI();
        DisableConstructionUiAllForms();
        SetStateForFormationUI(UIState.Active);
        CloseBarrackUI();
        InitUnitsInfoUI();
    }
    public void OnDisableFormationUI()
    {
        SetStateForFormationUI(UIState.NonActive);
        SetNonActiveConstructionUI();
        CLoseUnitInfoUI();
    }
    private void OnOpenConstructionUI()
    {
        SetStateForFormationUI(UIState.NonActive);
        CloseBuildingUI();
        SetStateForMarketUI(UIState.NonActive);
        CloseBarrackUI();
        SetActiveConstructionUI();
    }
    private void OnDisableConstructionUI()
    {
        SetNonActiveConstructionUI();
    }
    public void OnOpenBuildingUI(GameObject gameObject)
    {
        SetStateForMarketUI(UIState.NonActive);
        SetNonActiveConstructionUI();
        SetStateForFormationUI(UIState.NonActive);
        CloseBarrackUI();
        OpenBuildingUI(gameObject);
    }

    public void OnDisableBuildingUI()
    {
        CloseBuildingUI();
        CLoseUnitInfoUI();
    }

    public void OnOpenBarrackUI()
    {
        DisableConstructionUiAllForms();
        CloseBuildingUI();
        SetStateForMarketUI(UIState.NonActive);
        SetStateForFormationUI(UIState.NonActive);
        OpenBarrackUI();
    }
    public void OnDisableBarrackUI()
    {
        CloseBarrackUI();
        SetNonActiveConstructionUI();
    }
    
    private void SetActiveConstructionUI()
    {
        activeConstructionUI.SetActive(true);
        nonActiveConstructionUI.SetActive(false);
        constructionButton.GetComponent<Image>().sprite = activeCell;
    }
    private void SetNonActiveConstructionUI()
    {
        activeConstructionUI.SetActive(false);
        nonActiveConstructionUI.SetActive(true);
        constructionButton.GetComponent<Image>().sprite = nonActiveCell;
    }
    private void DisableConstructionUiAllForms()
    {
        activeConstructionUI.SetActive(false);
        nonActiveConstructionUI.SetActive(false);
        constructionButton.GetComponent<Image>().sprite = nonActiveCell;
    }
    private void SetStateForMarketUI(UIState state)
    {
        if (!IsPoolContainObjectByName(buildingPool, "Market"))
            return;
        
        marketUI.SetActive(GetState(state));
        
        if (GetState(state))
            marketButton.GetComponent<Image>().sprite = activeCell;
        else
            marketButton.GetComponent<Image>().sprite = nonActiveCell;
        
        OpenUnitInfoUI();
    }

    private void OpenBuildingUI(GameObject building)
    {
        Transform econimicElement = building.transform.Find("EconomicElement");
         if (econimicElement.TryGetComponent(out MiningBuilding miningBuilding))
         { 
             miningBuildingUI.SetActive(true);
             InitMiningBuildingUI(miningBuilding);
         }
         else if (econimicElement.TryGetComponent(out EconomicBuilding economicBuilding))
         {
             if (economicBuilding is AmbarBehaviour)
                 InitAmbarUI(economicBuilding);
             else if (economicBuilding is BarrackBehaviour)
             {
                 /*Debug.Log("economicBuilding stats:" + 
                           economicBuilding.Name + " " +  
                           economicBuilding.CurrentBuildingHP + " " + 
                           economicBuilding.MaxBuildingHP);*/
                 InitBarrackUI(economicBuilding);
             }
         }
    }
    private void CloseBuildingUI()
    {
        miningBuildingUI.SetActive(false);
        
        ambarUI.SetActive(false);
        foreach (GameObject uiElement in selectedUnitShower)
        {
            uiElement.SetActive(false);
        }
    }

    private void OpenUnitInfoUI()
    {
        unitInfoUI.SetActive(true);
    }
    private void CLoseUnitInfoUI()
    {
        unitInfoUI.SetActive(false);
    }
    private void SetStateForFormationUI(UIState state)
    {
        formationUI.SetActive(GetState(state));
    }

    private void InitMiningBuildingUI(MiningBuilding miningBuilding)
    {
        miningBuildingUI.transform.Find("WorkersAmountShower").Find("WorkersAmountCounterShower").GetComponent<TMP_Text>().text =
            miningBuilding.currentPersonToWork.ToString();
        
        miningBuildingUI.transform.Find("UpgradeButton").Find("UpgradeLevelShower").GetComponent<TMP_Text>().text =
            miningBuilding.Level.ToString();

        addWorkersButton.onClick.RemoveAllListeners();
        addWorkersButton.onClick.AddListener(miningBuilding.TryAddNewWorker);
        addWorkersButton.onClick.AddListener(() =>
            miningBuildingUI.transform.Find("WorkersAmountShower").Find("WorkersAmountCounterShower")
                    .GetComponent<TMP_Text>().text =
                miningBuilding.currentPersonToWork.ToString());
        
        removeWorkerButton.onClick.RemoveAllListeners();
        removeWorkerButton.onClick.AddListener(miningBuilding.TryRemoveWorker);
        removeWorkerButton.onClick.AddListener(() =>
            miningBuildingUI.transform.Find("WorkersAmountShower").Find("WorkersAmountCounterShower")
                    .GetComponent<TMP_Text>().text =
                miningBuilding.currentPersonToWork.ToString());
        
        InitBuildingUI(miningBuilding);
    }

    private void InitAmbarUI(EconomicBuilding economicBuilding)
    { 
        AmbarBehaviour ambar = (AmbarBehaviour)economicBuilding;
        ambarUI.transform.Find("MaxResAmount").GetComponent<TMP_Text>().text = (ambar.Level * 1000).ToString();
        InitBuildingUI(ambar);
        
        ambarUI.SetActive(true);
    }

    private void InitBarrackUI(EconomicBuilding economicBuilding)
    {
        BarrackBehaviour barrack = (BarrackBehaviour)economicBuilding;
        
        InitBuildingUI(barrack);
        OpenBarrackUI();
    }
    
    private void InitBuildingUI(IBuilding building)
    {
        Debug.Log("Name: " + building.Name);
        Debug.Log("CurrentBuildingHP: " + building.CurrentBuildingHP);
        Debug.Log("MaxBuildingHP: " + building.MaxBuildingHP);
        
        unitInfoUI.transform.Find("UnitStats").gameObject.SetActive(false);
        
        unitInfoUI.transform.Find("UnitName").GetComponent<TMP_Text>().text =
            building.Name;
        
       /* unitInfoUI.transform.Find("UnitHpRateShower").GetComponent<Image>().fillAmount =
            building.CurrentBuildingHP/building.MaxBuildingHP;*/

        unitInfoUI.transform.Find("UnitHpRateText").GetComponent<TMP_Text>().text =
            building.CurrentBuildingHP + " hp";

        /* this switch is for init unit preview
        switch (building.Name)
        {
            
        }*/
        
        OpenUnitInfoUI();
    }

    private void InitUnitsInfoUI()
    {
        if (selectedObjects.unitList[0].TryGetComponent(out Unit unit))
        {
            unitInfoUI.transform.Find("UnitName").GetComponent<TMP_Text>().text =
                unit.unitName;
            
            unitInfoUI.transform.Find("UnitHpRateShower").GetComponent<Image>().fillAmount =
                unit.currentHP / unit.maxHp;
            
            unitInfoUI.transform.Find("UnitHpRateText").GetComponent<TMP_Text>().text =
                unit.currentHP + " hp";
            
            Transform unitStats = unitInfoUI.transform.Find("UnitStats");
            unitStats.gameObject.SetActive(true);
            unitStats.Find("UnitAttackRateText").GetComponent<TMP_Text>().text = ((int)unit.attackRate).ToString();
            unitStats.Find("UnitDeffenceRateText").GetComponent<TMP_Text>().text = ((int)unit.deffenceRate).ToString();

            int swordsManCounter = 0;
            int archerCounter = 0;
            int mageCounter = 0;
            
            foreach (GameObject gameUnit in selectedObjects.unitsSelected)
            {
                if (gameUnit.TryGetComponent(out Unit unitComponent))
                {
                    switch (unitComponent.unitType)
                    {
                        case UnitType.Bash:
                            selectedUnitShower[0].SetActive(true);
                            swordsManCounter++;
                            break;
                        case UnitType.Archer:
                            selectedUnitShower[1].SetActive(true);
                            archerCounter++;
                            break;
                        case UnitType.Mage:
                            selectedUnitShower[2].SetActive(true);
                            mageCounter++;
                            break;
                    }
                }
            }
            
            selectedUnitShower[0].transform.Find("SwordsManCounter").GetComponent<TMP_Text>().text = swordsManCounter.ToString();
            selectedUnitShower[1].transform.Find("ArcherCounter").GetComponent<TMP_Text>().text = archerCounter.ToString();
            selectedUnitShower[2].transform.Find("MageCounter").GetComponent<TMP_Text>().text = mageCounter.ToString();
            
            /* this switch is for init unit preview
            switch (building.Name)
            {
                
            }*/
            
           
        }
        
        OpenUnitInfoUI();
    }

    private void OpenBarrackUI()
    {
        barrackUI.SetActive(true);
    }
    private void CloseBarrackUI()
    {
        barrackUI.SetActive(false);
    }

    private bool IsPoolContainObjectByName(Transform pool, string name)
    {
        foreach (Transform building in pool)
        {
            if (building.Find(name) is not null)
                return true;
        }

        return false;
    }
    private bool GetState(UIState state) => state == UIState.Active ? true : false;
}
