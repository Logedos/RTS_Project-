using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ExchangeData : ScriptableObject
{
    [Header("Stone Exchange")]
    public const float stoneToWoodBase = 0.7f;
    public float stoneToWoodCurrent = 0.7f;
    public float stoneToWoodDecrease = 0.01f;
    
    public const float stoneToFoodBase = 1.2f;
    public float stoneToFoodCurrent = 1.2f;
    public float stoneToFoodDecrease = 0.012f;
    
    public const float stoneToGoldBase = 0.1f;
    public float stoneToGoldCurrent = 0.1f;
    public float stoneToGoldDecrease = 0.008f;
    
    [Header("Wood Exchange")]
    public const float woodToStoneBase = 0.8f;
    public float woodToStoneCurrent = 0.8f;
    public float woodToStoneDecrease = 0.009f;
    
    public const float woodToFoodBase = 1.1f;
    public float woodToFoodCurrent = 1.1f;
    public float woodToFoodDecrease = 0.008f;
    
    public const float woodToGoldBase = 0.08f;
    public float woodToGoldCurrent = 0.08f;
    public float woodToGoldDecrease = 0.006f;
    
    [Header("Food Exchange")]
    public const float foodToStoneBase = 0.45f;
    public float foodToStoneCurrent = 0.45f;
    public float foodToStoneDecrease = 0.009f;
    
    public const float foodToWoodBase = 0.5f;
    public float foodToWoodCurrent = 0.5f;
    public float foodToWoodDecrease = 0.001f;
    
    public const float foodToGoldBase = 0.05f;
    public float foodToGoldCurrent = 0.05f;
    public float foodToGoldDecrease = 0.0005f;
    
    [Header("Gold Exchange")]
    public const float goldToStoneBase = 8f;
    public float goldToStoneCurrent = 8f;
    public float goldToStoneDecrease = 0.09f;
    
    public const float goldToWoodBase = 9f;
    public float goldToWoodCurrent = 9f;
    public float goldToWoodDecrease = 0.08f;
    
    public const float goldToFoodBase = 15f;
    public float goldToFoodCurrent = 15f;
    public float goldToFoodDecrease = 0.02f;
    
}
