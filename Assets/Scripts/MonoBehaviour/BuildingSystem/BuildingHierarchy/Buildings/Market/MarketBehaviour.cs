using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketBehaviour : MonoBehaviour
{
    public ResType resTypeToSell = ResType.Food;
    public ResType resTypeToBuy = ResType.Wood;

    [SerializeField] private TMP_Text resTypeToSellAmount;
    [SerializeField] private TMP_Text resTypeToBuyAmount;

    [SerializeField] private GameObject stoneIcoToSell;
    [SerializeField] private GameObject foodIcoToSell;
    [SerializeField] private GameObject woodIcoToSell;
    [SerializeField] private GameObject goldIcoToSell;
    
    [SerializeField] private GameObject stoneIcoToBuy;
    [SerializeField] private GameObject foodIcoToBuy;
    [SerializeField] private GameObject woodIcoToBuy;
    [SerializeField] private GameObject goldIcoToBuy;

    [SerializeField] private ExchangeData exchangeData;

    [SerializeField] private Sprite cellImage;
    [SerializeField] private Sprite selectedCellImage;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void Trade_OnClick()
    {
        Storage storage = JsonSaver.GetResources();
        foreach (IResources resorce in storage.resourcesList)
        {
            if (resorce.resType == resTypeToSell)
            {
                float changeCof = GetExchangeCof(resTypeToSell, resTypeToBuy);
                if (int.Parse(resTypeToSellAmount.text) >= resorce.currentAmount)
                {
                    storage.resourcesList.Find(x => x.resType == resTypeToBuy).currentAmount +=
                        (int)(resorce.currentAmount * changeCof);
                    resorce.currentAmount = 0;
                }
                else if(int.Parse(resTypeToSellAmount.text) < resorce.currentAmount)
                {
                    storage.resourcesList.Find(x => x.resType == resTypeToBuy).currentAmount +=
                        (int)(int.Parse(resTypeToSellAmount.text) * changeCof);
                    resorce.currentAmount -= int.Parse(resTypeToSellAmount.text);
                }
            }
        }
        JsonSaver.SaveResources(storage);
        
    }*/
    
    public void Trade_OnClick() 
    {
    Storage storage = JsonSaver.GetResources();
    float totalResourcesToAdd = 0f;
    float totalCurrentRes = 0f;
    float changeCof = GetExchangeCof(resTypeToSell, resTypeToBuy);
    bool tradeIsDone = false;
    
    foreach (IResources resorce in storage.resourcesList)
    {
        if (resorce.resType == resTypeToSell)
        { 
            changeCof = GetExchangeCof(resTypeToSell, resTypeToBuy);
            totalResourcesToAdd += int.Parse(resTypeToSellAmount.text) * changeCof;
        }

        totalCurrentRes += resorce.currentAmount;
    }
    
    // Проверяем, превышает ли общее количество максимальное количество
    if (totalCurrentRes + totalResourcesToAdd - int.Parse(resTypeToSellAmount.text) <= storage.maxAmount)
    {
        foreach (IResources resorce in storage.resourcesList)
        {
            if (resorce.resType == resTypeToSell)
            {
                
                if (int.Parse(resTypeToSellAmount.text) >= resorce.currentAmount)
                {
                    storage.resourcesList.Find(x => x.resType == resTypeToBuy).currentAmount +=
                        (int)(resorce.currentAmount * changeCof);
                    resorce.currentAmount = 0;
                    
                    tradeIsDone = true;
                }
                else if (int.Parse(resTypeToSellAmount.text) < resorce.currentAmount)
                {
                    storage.resourcesList.Find(x => x.resType == resTypeToBuy).currentAmount +=
                        (int)(int.Parse(resTypeToSellAmount.text) * changeCof);
                    resorce.currentAmount -= int.Parse(resTypeToSellAmount.text);
                    
                    tradeIsDone = true;
                }
            }
        }

        
    }
    else if(totalCurrentRes + totalResourcesToAdd - int.Parse(resTypeToSellAmount.text) > storage.maxAmount)
    {
        foreach (IResources resorce in storage.resourcesList)
        {
            if (resorce.resType == resTypeToSell)
            {
                int freeSpace = storage.maxAmount - (int)totalCurrentRes;
                float x = freeSpace / (changeCof - 1);

                storage.resourcesList.Find(x => x.resType == resTypeToBuy).currentAmount += (int)(changeCof * x);
                totalCurrentRes += (int)(changeCof * x);
                resorce.currentAmount = storage.maxAmount - ((int)totalCurrentRes - resorce.currentAmount);
                
                tradeIsDone = true;
            }
        }

        tradeIsDone = true;
    }

    if (tradeIsDone)
    {
        JsonSaver.SaveResources(storage);
        ResourceShowerManager.UpdateResInfo();
        UpdateBuyResAmount();
        DecreaseCof(resTypeToSell, resTypeToBuy);
    }
    
    }
    public void ChangeSellRes_OnClick(int index)
    {
        
        ResType resType = GetResTypeByIndex(index);
        resTypeToSell = resType;
        SetActiveResImageToSell(resType);
        UpdateBuyResAmount();
    }

    private void SetActiveResImageToSell(ResType resType)
    {
        stoneIcoToSell.GetComponent<Image>().sprite = cellImage;
        woodIcoToSell.GetComponent<Image>().sprite = cellImage;
        foodIcoToSell.GetComponent<Image>().sprite = cellImage;
        goldIcoToSell.GetComponent<Image>().sprite = cellImage;
        
        if (resType == ResType.Stone)
        {
            stoneIcoToSell.GetComponent<Image>().sprite = selectedCellImage;
        }
        else if (resType == ResType.Wood)
        {
            woodIcoToSell.GetComponent<Image>().sprite = selectedCellImage;
        }
        else if (resType == ResType.Food)
        {
            foodIcoToSell.GetComponent<Image>().sprite = selectedCellImage;
        }
        else if (resType == ResType.Gold)
        {
            goldIcoToSell.GetComponent<Image>().sprite = selectedCellImage;
        }
    }
    
    public void ChangeBuyRes_OnClick(int index)
    {
        ResType resType = GetResTypeByIndex(index);
        resTypeToBuy = resType;
        SetActiveResImageToBuy(resType);
        UpdateBuyResAmount();
    }
    private void SetActiveResImageToBuy(ResType resType)
    {
        stoneIcoToBuy.GetComponent<Image>().sprite = cellImage;
        woodIcoToBuy.GetComponent<Image>().sprite = cellImage;
        foodIcoToBuy.GetComponent<Image>().sprite = cellImage;
        goldIcoToBuy.GetComponent<Image>().sprite = cellImage;
        
        if (resType == ResType.Stone)
        {
            stoneIcoToBuy.GetComponent<Image>().sprite = selectedCellImage;
        }
        else if (resType == ResType.Wood)
        {
            woodIcoToBuy.GetComponent<Image>().sprite = selectedCellImage;
        }
        else if (resType == ResType.Food)
        {
            foodIcoToBuy.GetComponent<Image>().sprite = selectedCellImage;
        }
        else if (resType == ResType.Gold)
        {
            goldIcoToBuy.GetComponent<Image>().sprite = selectedCellImage;
        }
    }

    private float GetExchangeCof(ResType sellResType, ResType buyResType)
    {
        if (sellResType == ResType.Stone && buyResType == ResType.Wood) //*
        {
            return exchangeData.stoneToWoodCurrent;
        }
        else if(sellResType == ResType.Stone && buyResType == ResType.Food)//*
        {
            return exchangeData.stoneToFoodCurrent;
        }
        else if (sellResType == ResType.Stone && buyResType == ResType.Gold)//*
        {
            return exchangeData.stoneToGoldCurrent;
        }
        else if (sellResType == ResType.Wood && buyResType == ResType.Stone) //*
        {
            return exchangeData.woodToStoneCurrent;
        }
        else if (sellResType == ResType.Wood && buyResType == ResType.Food) //*
        {
            return exchangeData.woodToFoodCurrent;
        }
        else if (sellResType == ResType.Wood && buyResType == ResType.Gold) //*
        {
            return exchangeData.woodToGoldCurrent;
        }
        else if (sellResType == ResType.Food && buyResType == ResType.Gold) //*
        {    
            return exchangeData.foodToGoldCurrent;
        }
        else if (sellResType == ResType.Food && buyResType == ResType.Stone) //*
        {
            return exchangeData.foodToStoneCurrent;
        }
        else if (sellResType == ResType.Food && buyResType == ResType.Wood) //*
        {
            return exchangeData.foodToWoodCurrent;
        }
        else if (sellResType == ResType.Gold && buyResType == ResType.Wood) //*
        {
            return exchangeData.goldToWoodCurrent;
        }
        else if (sellResType == ResType.Gold && buyResType == ResType.Stone) //*
        {
            return exchangeData.goldToStoneCurrent;
        }
        else if (sellResType == ResType.Gold && buyResType == ResType.Food) //*
        {
            return exchangeData.goldToFoodCurrent;
        }

        return 1;
    }
    private void DecreaseCof(ResType sellResType, ResType buyResType)
    {
        if (sellResType == ResType.Stone && buyResType == ResType.Wood) //*
        {
            if(exchangeData.stoneToWoodCurrent - exchangeData.stoneToWoodDecrease > 0)
                exchangeData.stoneToWoodCurrent -= exchangeData.stoneToWoodDecrease;
        }
        else if(sellResType == ResType.Stone && buyResType == ResType.Food)//*
        {
            if(exchangeData.stoneToFoodCurrent - exchangeData.stoneToFoodDecrease > 0)
                exchangeData.stoneToFoodCurrent -= exchangeData.stoneToFoodDecrease;
        }
        else if (sellResType == ResType.Stone && buyResType == ResType.Gold)//*
        {
            if(exchangeData.stoneToGoldCurrent - exchangeData.stoneToGoldDecrease > 0)
                exchangeData.stoneToGoldCurrent -= exchangeData.stoneToGoldDecrease;
        }
        else if (sellResType == ResType.Wood && buyResType == ResType.Stone) //*
        {
            if(exchangeData.woodToStoneCurrent - exchangeData.woodToStoneDecrease > 0)
                exchangeData.woodToStoneCurrent -= exchangeData.woodToStoneDecrease;
        }
        else if (sellResType == ResType.Wood && buyResType == ResType.Food) //*
        {
            if(exchangeData.woodToFoodCurrent - exchangeData.woodToFoodDecrease > 0)
                exchangeData.woodToFoodCurrent -= exchangeData.woodToFoodDecrease;
        }
        else if (sellResType == ResType.Wood && buyResType == ResType.Gold) //*
        {
            if(exchangeData.woodToGoldCurrent - exchangeData.woodToGoldDecrease > 0)
                exchangeData.woodToGoldCurrent -= exchangeData.woodToGoldDecrease;
        }
        else if (sellResType == ResType.Food && buyResType == ResType.Gold)//*
        {            
            if(exchangeData.foodToGoldCurrent - exchangeData.foodToGoldDecrease > 0)
                exchangeData.foodToGoldCurrent -= exchangeData.foodToGoldDecrease;
        }
        else if (sellResType == ResType.Food && buyResType == ResType.Stone) //*
        {
            if(exchangeData.foodToStoneCurrent - exchangeData.foodToStoneDecrease > 0)
                exchangeData.foodToStoneCurrent -= exchangeData.foodToStoneDecrease;
        }
        else if (sellResType == ResType.Food && buyResType == ResType.Wood) //*
        {
            if(exchangeData.foodToWoodCurrent - exchangeData.foodToWoodDecrease > 0)
                exchangeData.foodToWoodCurrent -= exchangeData.foodToWoodDecrease;
        }
        else if (sellResType == ResType.Gold && buyResType == ResType.Wood) //*
        {
            if(exchangeData.goldToWoodCurrent - exchangeData.goldToWoodDecrease > 0)
                exchangeData.goldToWoodCurrent -= exchangeData.goldToWoodDecrease;
        }
        else if (sellResType == ResType.Gold && buyResType == ResType.Stone) //*
        {
            if(exchangeData.goldToStoneCurrent - exchangeData.goldToWoodDecrease > 0)
                exchangeData.goldToStoneCurrent -= exchangeData.goldToWoodDecrease;
        }
        else if (sellResType == ResType.Gold && buyResType == ResType.Food) //*
        {
            if(exchangeData.goldToFoodCurrent - exchangeData.goldToFoodDecrease > 0)
                exchangeData.goldToFoodCurrent -= exchangeData.goldToFoodDecrease;
        }
    }
    public void ChangeValueForResToSell_OnClick(int resCount)
    {
        if (int.Parse(resTypeToSellAmount.text) + resCount <= 0)
        {
            resTypeToSellAmount.text = 0.ToString();
            Debug.Log("<=0");
        }
        else
        {
            resTypeToSellAmount.text = (int.Parse(resTypeToSellAmount.text) + resCount).ToString();
        }
        UpdateBuyResAmount();
    }

    private ResType GetResTypeByIndex(int index)
    {
        if (index == 0)
        {
            return ResType.Stone;
        }
        else if(index == 1)
        {
            return ResType.Wood;
        }
        else if(index == 2)
        {
            return ResType.Food;
        }
        else
        {
            return ResType.Gold;
        }
    }

    private void UpdateBuyResAmount()
    {
        resTypeToBuyAmount.text = ((int)(GetExchangeCof(resTypeToSell, resTypeToBuy) * int.Parse(resTypeToSellAmount.text))).ToString();
    }
    
}
