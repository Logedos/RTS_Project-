using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static Action TimeToAddRes;
    public static Action AddRes;

    void Awake()
    {
        TimeToAddRes += AddResInvoker;
    }

    private void AddResInvoker()
    {
        AddRes += AddResInit;
        AddRes.Invoke();
    }

    private void AddResInit()
    {
        return;
    }
}
