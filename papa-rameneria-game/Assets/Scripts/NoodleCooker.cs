using UnityEngine;
using TMPro;



public class NoodleCooker : MonoBehaviour
{
    public Material rawMat;
    public Material cookedMat;
    public Material burntMat;
    private NoodleState noodleState;
    public float cookedTime = 5f;
    public float burntTime = 10f;
    public TMP_Text timerText;
    public TMP_Text statusText;
    private bool cooking = false;
    private float timer = 0f;

    void Start()
    {
        timerText.gameObject.SetActive(false);
        statusText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!cooking)
            return;

        timer += Time.deltaTime;

        timerText.text = timer.ToString("F1") + "s";

        //while cooking
        if (timer < cookedTime)
        {
            statusText.text = "Cooking..";
        }
        //raw to cooked
        if (timer >= cookedTime && noodleState.state == NoodleState.CookState.Raw)
        {

            if (noodleState != null)
                noodleState.SetState(NoodleState.CookState.Cooked);

            statusText.text = "Cooked!";
        }
        //cooked to burnt
        if (timer >= burntTime && noodleState.state == NoodleState.CookState.Cooked)
        {
            if (noodleState != null)
                noodleState.SetState(NoodleState.CookState.Burnt);

            statusText.text = "Burnt!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Noodles"))
        {
            timer = 0f;
            cooking = true;

            timerText.gameObject.SetActive(true);
            statusText.gameObject.SetActive(true);

            noodleState = other.GetComponent<NoodleState>();

            Debug.Log("Cooking started!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Noodles"))
        {
            cooking = false;

            timerText.gameObject.SetActive(false);
            statusText.gameObject.SetActive(false);
        }
    }
}