using UnityEngine;

public class CustomerDialogue : MonoBehaviour
{
    public GameObject speechBubble;
    private Customer customer;
    void Start()
    {
        customer = GetComponent<Customer>();
        speechBubble.SetActive(false);
    }

    void OnMouseDown()
    {
        speechBubble.SetActive(true);
        customer.Animator.SetBool("talking", true);
    }
}
