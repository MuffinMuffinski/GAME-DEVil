using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerLabel;
    [SerializeField] private int ReputationPoints = 0;

    [SerializeField] private List<GameObject> endPanelsEmails;

    private float targetTime = 31f;
    public bool isPaused;
    public bool secretEndingUnlocked { get; set; }
    
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
        
        if (ReputationPoints < 5)
        {
            //Bad ending
            endPanelsEmails[0].gameObject.SetActive(true);
        }

        if (ReputationPoints >= 5)
        {
            //Good ending
            endPanelsEmails[1].gameObject.SetActive(true);
        }

        if (ReputationPoints >= 5 && secretEndingUnlocked)
        {
            //Secret ending
            endPanelsEmails[2].gameObject.SetActive(true);
        }
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
        
        //TODO stop input
    }
    
    public void ContinueTimer()
    {
        //Pause timer
        isPaused = false;
    }

    public void RestartGame()
    {
        //TODO 
    }

    public void ExitGame()
    {
        
    }
}
