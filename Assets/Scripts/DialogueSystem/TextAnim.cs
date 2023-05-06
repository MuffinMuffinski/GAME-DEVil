using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public List<string> dialogText = new List<string>();

    private int i;
    
    public static TextAnim Instance { get; private set; }

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
    
    private IEnumerator TypeWriterEffect()
    {
        textMeshPro.ForceMeshUpdate();
        
        var totalCharCount = textMeshPro.textInfo.characterCount;
        var counter = 0;

        while (true)
        {
            var visibleCount = counter % (totalCharCount + 1);
            textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalCharCount)
            {
                i += 1;
                //Invoke("EndTypingCheck", 0.3f);
                break;
            }
            
            counter += 1;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void EndTypingCheck()
    {
        if (i <= dialogText.Count - 1)
        {
            textMeshPro.text = dialogText[i];
            StartCoroutine(TypeWriterEffect());
        }
    }
}
