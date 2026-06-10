using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
}