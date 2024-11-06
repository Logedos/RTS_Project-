using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour
{
    [SerializeField] private Button gamePlayButton;
    [SerializeField] private Button gameSpeedUpButton;
    [SerializeField] private Button gameSpeedDowmButton;
    [SerializeField] private TMP_Text gameSpeedShower;
    
    [SerializeField] private Sprite gameContinueSprite;
    [SerializeField] private Sprite gamePauseSprite;

    private float currentTimeScale = 1f;
    private bool timerIsPlay = true;
    private void Start()
    {
        gamePlayButton.onClick.AddListener(InteractWithPlayButton);
        gameSpeedUpButton.onClick.AddListener(GameSpeedUpButton_Click);
        gameSpeedDowmButton.onClick.AddListener(GameSpeedDowmButton_Click);
    }

    public void InteractWithPlayButton()
    {
        if (gamePlayButton.GetComponent<Image>().sprite == gameContinueSprite)
        {
            gamePlayButton.GetComponent<Image>().sprite = gamePauseSprite;
            timerIsPlay = false;
            Time.timeScale = 0f;
        }
        else if (gamePlayButton.GetComponent<Image>().sprite == gamePauseSprite)
        {
            gamePlayButton.GetComponent<Image>().sprite = gameContinueSprite;
            timerIsPlay = true;
            Time.timeScale = currentTimeScale;
        }
    }

    public void GameSpeedDowmButton_Click()
    {
        if (currentTimeScale <= 0.5f)
            return;
        
        currentTimeScale-=0.5f;
        gameSpeedShower.text = currentTimeScale.ToString() + "x";
        
        if(timerIsPlay)
            Time.timeScale = currentTimeScale;
    }
    
    public void GameSpeedUpButton_Click()
    {
        if (currentTimeScale >= 2f)
            return;
        
        currentTimeScale+=0.5f;
        gameSpeedShower.text = currentTimeScale.ToString() + "x";
        
        if(timerIsPlay)
            Time.timeScale = currentTimeScale;
    }
}
