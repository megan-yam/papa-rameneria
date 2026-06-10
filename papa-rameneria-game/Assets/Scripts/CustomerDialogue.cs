using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// public class CustomerDialogue : MonoBehaviour
// {
//     [Header("UI")]
//     private GameObject speechBubble;
//     private TextMeshProUGUI[] dialogueText;

//     [Header("Dialogue Lines")]
//     public List<string> orderLines = new List<string>();

//     private int currentLine = 0;
//     public int customerIdx = 0;

//     void Start()
//     {
//         speechBubble = GameObject.Find("SpeechBubble");
//         for(int i = 0; i < 5; i++)
//         {
//             dialogueText[i] = GameObject.Find("DialogueText"+i).GetComponent<TextMeshProUGUI>();
            
//         }
//         speechBubble.SetActive(false);

//     }

//     public void StartDialogue()
//     {
//         currentLine = 0;
//         speechBubble.SetActive(true);
//         StartCoroutine(ShowDialogue());
//     }

//     IEnumerator ShowDialogue()
//     {
//         while(currentLine < orderLines.Count)
//         {
//             dialogueText[customerIdx].text = orderLines[currentLine];
//             yield return new WaitForSeconds(2f);
//             currentLine++;
//         }
//         speechBubble.SetActive(false);
//     }
    
// }

public class CustomerDialogue : MonoBehaviour
{
    public List<string> orderLines;

    public void StartDialogue()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        foreach (string line in orderLines)
        {
            UIManager.Instance.ShowDialogue(line);
            yield return new WaitForSeconds(2f);
        }

        UIManager.Instance.HideDialogue();
    }

    public void AdvanceDialogue()
    {
        dialogueStage++;

        if (dialogueStage == 1)
        {
            dialogueText.text = orderDialogue;

            // Automatically close after 10 seconds
            Invoke(nameof(EndDialogue), dialogueDuration);
        }
    }

    public void EndDialogue()
    {
        speechBubble.SetActive(false);
        interactBubble.SetActive(true);

        customer.Animator.SetBool("talking", false);

        dialogueStage = 0;
    }

    void OnMouseDown()
    {
        if (!speechBubble.activeSelf)
        {
            StartDialogue();
        }
        else if (dialogueStage == 0)
        {
            AdvanceDialogue();
        }
    }

    public void EnableInteraction()
    {
        interactBubble.SetActive(true);
    }
}