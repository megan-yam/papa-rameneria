using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject speechBubble;
    public TextMeshProUGUI dialogueText;

    void Awake()
    {
        Instance = this;

        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
    }

    public void ShowDialogue(string line)
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
        }

        if (dialogueText != null)
        {
            dialogueText.text = line;
        }
    }

    public void HideDialogue()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
    }
}