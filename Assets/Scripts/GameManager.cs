using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerLabel;
	[SerializeField] private TMP_Text pointsLabel;
    [SerializeField] private List<GameObject> endPanelsEmails;
    [SerializeField] private SpriteRenderer reputationBar;
    [SerializeField] private List<Sprite> reputationFillers;
    [SerializeField] private InteractableItem secretItem;

    private float targetTime = 31f;
    public float points;
    public bool isPaused;
    public bool secretEndingUnlocked;
    
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
        
        if (points < 5)
        {
            //Bad ending
            endPanelsEmails[0].gameObject.SetActive(true);
        }

        if (points >= 5)
        {
            //Good ending
            endPanelsEmails[1].gameObject.SetActive(true);
        }

        if (points >= 5 && secretEndingUnlocked)
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

    public void AddPoints(int point)
    {
        if (point >= 3)
        {
            points++;
        }
        else if (point <= 1)
        {
            points--;
        }

        points = Mathf.Clamp(points, 0, 5);
        pointsLabel.text = points.ToString();

        reputationBar.sprite = reputationFillers[(int)points];

        if (points == 5)
        {
            secretItem.UnlockSecretItem();
        }
    }

    public void RestartGame()
    {
        //TODO 
    }

    public void ExitGame()
    {
        
    }
}
