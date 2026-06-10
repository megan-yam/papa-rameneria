using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDialogue : MonoBehaviour
{
    // Keeping this variable so Unity doesn't throw editor compilation errors, 
    // but we will no longer use it for text printing.
    [HideInInspector] public System.Collections.Generic.List<string> orderLines;

    public void StartDialogue()
    {
        // 1. Immediately look for the structural order recipe on this customer
        CustomerOrder customerOrder = GetComponent<CustomerOrder>();
        
        if (customerOrder != null)
        {
            // 2. Instantly post the data straight to the screen overlay ticket
            UIManager.Instance?.DisplayOrderTicket(customerOrder.order);
        }
        else
        {
            Debug.LogError($"CustomerDialogue on {gameObject.name} could not find its CustomerOrder sibling component!");
        }
    }
}