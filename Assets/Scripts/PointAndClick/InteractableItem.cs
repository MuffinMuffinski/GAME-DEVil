using System;
using UnityEngine;
using TMPro;

public class InteractableItem : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextAsset inkDialogue;
    [SerializeField] private TextMeshProUGUI textFieldItem;
    [SerializeField] private Material outlineMaterial;
    public string itemName;

    private bool interacted;

    private Material defaultMaterial;

    private void Awake()
    {
        defaultMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void OnMouseOver()
    {
        if(GameManager.Instance.isPaused || interacted) return;
        
        textFieldItem.text = itemName;
        
        //highlight
        GetComponent<SpriteRenderer>().material = outlineMaterial;
    }

    public void OnMouseExit()
    {
        textFieldItem.text = "";
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    public void OnMouseDown()
    {
        if(GameManager.Instance.isPaused || interacted) return;

        interacted = true;
        audioSource.Play();
        
        GameManager.Instance.OnItemClick();
        BasicInkExample.Instance.inkJSONAsset = inkDialogue;
        BasicInkExample.Instance.StartStory();
    }
}
