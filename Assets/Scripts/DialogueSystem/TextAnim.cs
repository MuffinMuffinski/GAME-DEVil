using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private readonly Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
    
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
    
    private void AddToQueue(IEnumerator coroutine)
    {
        coroutineQueue.Enqueue(coroutine);
        if (coroutineQueue.Count == 1)
            StartCoroutine(CoroutineCoordinator());
    }
 
    private IEnumerator CoroutineCoordinator()
    {
        while (true)
        {
            while (coroutineQueue.Count > 0)
            {
                yield return StartCoroutine(coroutineQueue.Peek());
                coroutineQueue.Dequeue();
            }
            if (coroutineQueue.Count == 0)
                yield break;
        }
    }
    
    private IEnumerator TypeWriterEffect(string newText)
    {
        textMeshPro.text = newText;
        textMeshPro.ForceMeshUpdate();
        
        var totalCharCount = textMeshPro.textInfo.characterCount;
        var counter = 0;

        while (true)
        {
            var visibleCount = counter % (totalCharCount + 1);
            textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalCharCount)
            {
                yield return new WaitForSeconds(1f);
                break;
            }
            
            counter += 1;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void AddToTypeWriter(string newText)
    {
        AddToQueue(TypeWriterEffect(newText));
    }

    public void ClearTyper()
    {
        StopAllCoroutines();
        coroutineQueue.Clear();
        textMeshPro.text = string.Empty;
    }
}
