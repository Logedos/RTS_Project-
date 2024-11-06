using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeResValueManager : MonoBehaviour
{
    [SerializeField] private GameObject changeResValueStone;
    [SerializeField] private GameObject changeResValueWood;
    [SerializeField] private GameObject changeResValueFood;
    [SerializeField] private GameObject changeResValueGold;

    private static GameObject _changeResValueStone;
    private static GameObject _changeResValueWood;
    private static GameObject _changeResValueFood;
    private static GameObject _changeResValueGold;

    private static string changeValueAnimName = "ToChangeValue"; 
    private static string DefaultAnimName = "ToDefault"; 
    private void Awake()
    {
        _changeResValueStone = changeResValueStone;
        _changeResValueFood = changeResValueFood;
        _changeResValueWood = changeResValueWood;
        _changeResValueGold = changeResValueGold;
    }

    public static void VisualizeResChanging(ResType resType, int amount, OperationType operationType)
    {
        string sign = String.Empty;

        switch (operationType)
        {
            case OperationType.Add:
                sign = "+ ";
                break;
            case OperationType.Subtract: 
                sign = "- ";
                break;
        }

        switch (resType)
        {
            case ResType.Stone:
                _changeResValueStone.GetComponent<TMP_Text>().text = sign + Mathf.Abs(amount).ToString();
                PlayAnimation(_changeResValueStone);
                break;
            case ResType.Food:
                _changeResValueFood.GetComponent<TMP_Text>().text = sign + Mathf.Abs(amount).ToString();
                PlayAnimation(_changeResValueFood);
                break;
            case ResType.Wood:
                _changeResValueWood.GetComponent<TMP_Text>().text = sign + Mathf.Abs(amount).ToString();
                PlayAnimation(_changeResValueWood); 
                break;
            case ResType.Gold:
                _changeResValueGold.GetComponent<TMP_Text>().text = sign + Mathf.Abs(amount).ToString();
                PlayAnimation(_changeResValueGold);
                break;
        }
    }

    public enum OperationType : byte
    {
        Add,
        Subtract
    }

    private static void PlayAnimation(GameObject changeResValue)
    {
        changeResValue.GetComponent<Animator>().SetTrigger(DefaultAnimName); 
        changeResValue.GetComponent<Animator>().SetTrigger(changeValueAnimName);
    }

}
