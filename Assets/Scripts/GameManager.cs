using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text timerLabel;
    [SerializeField] private List<InteractableItem> itemList;

    private float targetTime = 20f;
    private bool isPaused;
    
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (!isPaused)
        {
            targetTime -= Time.deltaTime;
            
            var timeString = targetTime < 10 ? $"0{(int)targetTime}" : ((int)targetTime).ToString();           
            timerLabel.text = $"00:{timeString}";
        }
 
        if (targetTime <= 0.0f)
        {
            TimerEnded();
        }
    }

    void TimerEnded()
    {
        isPaused = true;
        
        //TODO end game
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }


    public void OnItemClick()
    {
        //Pause timer
        isPaused = true;

        //Play corresponding dialogue > safe item as already clicked?
        //DialoguePanel.gameObject.SetActive(true);
    }
    
    public void ContinueTimer()
    {
        //Pause timer
        isPaused = false;
    }
}
