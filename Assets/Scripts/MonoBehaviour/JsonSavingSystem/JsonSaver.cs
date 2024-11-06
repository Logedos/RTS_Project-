using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using File = System.IO.File;
using Input = UnityEngine.Input;

public class JsonSaver : MonoBehaviour
{
    private const string fileName = "/SavedData/resourceInfo.json";
    
    public static Storage GetResources()
    {
        string fullPath = Application.dataPath + fileName;
        string json = File.ReadAllText(fullPath);

        return ConvertToClass(JsonUtility.FromJson<StorageStruct>(json));
    }
/// <summary>
/// Обновляет ресурсы в джейсоне, берет старые и перезаписывает их(проблем с отсутcвием файлов не будет, так так файл создан вручную)
/// </summary>
/// <param name="resType">Тип ресурса</param>
/// <param name="currentAmountToAdd">Количество ресурсов которые нужно добавить</param>
    public static void SaveResources(ResType resType, int currentAmountToAdd)
    {
        Storage storage = GetResources();

        int totalSum = 0;

        foreach (var resource in storage.resourcesList)
        {
            if(resource is not Population)
                totalSum += resource.currentAmount;
        }

        foreach (var resource in storage.resourcesList)
        {
            if (resource.resType == resType && resType != ResType.Population)
            {
                if (currentAmountToAdd + totalSum <= storage.maxAmount)
                {
                    resource.currentAmount += currentAmountToAdd;
                }
                else if(currentAmountToAdd + totalSum > storage.maxAmount)
                {
                    resource.currentAmount += storage.maxAmount - totalSum;
                }
            }
            else if(resType == ResType.Population)
            {
                if (currentAmountToAdd + resource.currentAmount <= storage.maxAmount)
                {
                    resource.currentAmount += currentAmountToAdd;
                }
                else if(currentAmountToAdd + resource.currentAmount > storage.maxAmount)
                {
                    resource.currentAmount += storage.maxAmount - resource.currentAmount;
                }
            }
        }
        Debug.Log("[SaveResources - JsonSaver]");

        SaveResources(storage);
    }
/// <summary>
/// Увеличивает вместимость хранения ресурсов
/// </summary>
/// <param name="resType">Тип ресурса</param>
/// <param name="maxAmountToAdd">Число, на сколько мы хотим увеличить обьем хранилища</param>
    public static void ExpandMaxAmout(int maxAmountToAdd)
    {
        Storage storage = GetResources();
        storage.maxAmount += maxAmountToAdd;
        
        SaveResources(storage);
    }
    /// <summary>
    /// Увеличивает максимальное количество людей в поселении
    /// </summary>
    /// <param name="maxPopulationToAdd">Число, на сколько мы хотим увеличить максимальное количество</param>
    public static void ExpandMavPopulation(int maxPopulationToAdd)
    {
        Storage storage = GetResources();
        storage.maxPopulation += maxPopulationToAdd;
        
        SaveResources(storage);
    }
    
    public static void SaveResources(Storage storage)
    {
        string fullPath = Application.dataPath + fileName;
       
        string json = JsonUtility.ToJson(ConvertToStruct(storage),true);
        
        File.WriteAllText(fullPath,json);
    }
    
    private static StorageStruct ConvertToStruct(Storage storage)
    {
        StorageStruct storageStruct = new StorageStruct
        {
            resourcesList = storage.resourcesList,
            maxAmount = storage.maxAmount,
            maxPopulation = storage.maxPopulation
        };

        return storageStruct;
    }
    private static Storage ConvertToClass(StorageStruct storageStruct)
    {
        Storage storage = new Storage()
        {
            resourcesList = storageStruct.resourcesList,
            maxAmount = storageStruct.maxAmount,
            maxPopulation = storageStruct.maxPopulation
        };
        return storage;
    }

    private void InitStorage()
    {
        Storage storage = new Storage();

        foreach (var resourcese in storage.resourcesList)
        {
            resourcese.currentAmount = 0;
        }

        storage.maxAmount = 0;
        storage.maxPopulation = 0;
        
        SaveResources(storage);
    }
}
