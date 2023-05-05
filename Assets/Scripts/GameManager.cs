using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private List<InteractableItem> itemList;
    
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

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        //Timer start and animation
    }

    public void OnItemClick(string itemName)
    {
        //Pause timer
        //Play corresponding dialogue > safe item as already clicked?
        DialoguePanel.gameObject.SetActive(true);
    }
}
