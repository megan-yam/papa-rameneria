using UnityEngine;
using TMPro;

public class CustomerDialogue : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject interactBubble;
    public TMP_Text dialogueText;

    private Customer customer;

    [TextArea]
    public string greetingDialogue;

    [TextArea]
    public string orderDialogue;

    public float dialogueDuration = 10f;

    private int dialogueStage = 0;

    void Start()
    {
        customer = GetComponent<Customer>();

        speechBubble.SetActive(false);
        interactBubble.SetActive(true);
    }

    public void StartDialogue()
    {
        CancelInvoke(nameof(EndDialogue));

        dialogueStage = 0;

        speechBubble.SetActive(true);
        interactBubble.SetActive(false);

        dialogueText.text = greetingDialogue;
        customer.Animator.SetBool("talking", true);
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
}