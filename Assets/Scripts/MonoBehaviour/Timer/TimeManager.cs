using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TimerData dayChangerData;
    [SerializeField] private TMP_Text dayCountShower;
    [SerializeField] private TMP_Text clockText;
    [SerializeField] private float timeStepToAddRes = 30f;
    public float currentTime;
    private const float timeConst = 1f;

    public List<GameObject> buildingsToBuild;
    
    private void Start()
    {
        currentTime = 0;
        
        StartCoroutine(WaitForAddRes());
        
        dayCountShower.text = dayChangerData.dayNumber.ToString();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        VisualiseTime(currentTime);
    }

    private void VisualiseTime(float time)
    {
        if (dayChangerData.secondPerDay > time)
        {
            float gameSeconds = time * dayChangerData.secondPerDay;

            int hours = Mathf.FloorToInt(gameSeconds / 3600);
            int minutes = Mathf.FloorToInt(gameSeconds % 3600 / 60);
            
            clockText.text = $"{hours % 25}:{minutes % 60}";
        }
        else
        {
            Debug.Log("Here");
            NextDayUpdate();
        }
    }

    private void NextDayUpdate()
    {
        currentTime = 0;
        dayChangerData.dayNumber++;
        dayCountShower.text = dayChangerData.dayNumber.ToString();

        FinishAllBuildings();
        ResourceShowerManager.UpdateResInfo();
    }
    
    private IEnumerator WaitForAddRes()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeStepToAddRes);
            GameHandler.TimeToAddRes.Invoke();
        }
    }

    private void FinishAllBuildings()
    {
        foreach (GameObject building in buildingsToBuild)
        {
            if (building.transform.TryGetComponent(out BuildingProccesBehaviour buildingProccesBehaviour))
            {
                Debug.Log("BuildingProccesBehaviour was found");
                buildingProccesBehaviour.Build();
            }
        }
        buildingsToBuild.Clear();
    }
}
