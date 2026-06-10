using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class ServeButtonController : MonoBehaviour
{
    private PlayerViewController player;
    private Button serveButton;
 
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        if (FindObjectOfType<ServeButtonController>() != null)
            return;
 
        PlayerViewController playerView = FindObjectOfType<PlayerViewController>();
        if (playerView == null)
            return;
 
        GameObject controllerObject = new GameObject("Serve Button Controller");
        ServeButtonController controller = controllerObject.AddComponent<ServeButtonController>();
        controller.player = playerView;
    }
 
    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerViewController>();
 
        CreateButton();
        UpdateButtonVisibility();
    }
 
    private void Update()
    {
        UpdateButtonVisibility();
    }
 
    private void CreateButton()
    {
        if (serveButton != null)
            return;
 
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
            return;
 
        Button stationButton = FindStationButtonTemplate();
        GameObject buttonObject = new GameObject("Serve Button", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        buttonObject.transform.SetParent(canvas.transform, false);
 
        RectTransform rt = buttonObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(160, 50);
        rt.anchoredPosition = new Vector2(0, -200);
 
        Image img = buttonObject.GetComponent<Image>();
        img.color = Color.green;
 
        serveButton = buttonObject.GetComponent<Button>();
        serveButton.onClick.AddListener(OnServePressed);
 
        GameObject textObject = new GameObject("Text", typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        textObject.transform.SetParent(buttonObject.transform, false);
 
        TextMeshProUGUI buttonText = textObject.GetComponent<TextMeshProUGUI>();
        buttonText.text = "SERVE";
        buttonText.fontSize = 24;
        buttonText.color = Color.white;
        buttonText.alignment = TextAlignmentOptions.Center;
 
        RectTransform textRt = textObject.GetComponent<RectTransform>();
        textRt.anchoredPosition = Vector3.zero;
        textRt.sizeDelta = rt.sizeDelta;
    }
 
    private Button FindStationButtonTemplate()
    {
        foreach (Button b in FindObjectsOfType<Button>(true))
        {
            if (b.name.Contains("Station") || b.name.Contains("View"))
                return b;
        }
        return null;
    }
 
    private void OnServePressed()
{
    Customer waitingCustomer = FindWaitingCustomer();
    Bowl bowl = FindBowlAtOrderStation();

    if (waitingCustomer == null || bowl == null)
        return;

    waitingCustomer.SetBowl(bowl);

    float accuracy   = waitingCustomer.CalculateAccuracy();
    float timeliness = waitingCustomer.CalculateTimeliness();
    float cooking    = waitingCustomer.CalculateCooking();
    float totalScore = waitingCustomer.CalculateTotalRating();

    string resultText = $"Recipe Accuracy:  <color=black>{accuracy:F0}%</color>\n" +
                        $"Timeliness Bonus: <color=black>{timeliness:F0}%</color>\n" +
                        $"Cooking Precision: <color=black>{cooking:F0}%</color>\n" +
                        $"<size=1.4em><b>FINAL RATING: <color=green>{totalScore:F0}%</color></b></size>";

    CustomerSpawner spawner = FindObjectOfType<CustomerSpawner>();
    bool isLastCustomer = spawner != null && spawner.IsLastCustomer;

    UIManager.Instance?.ShowScoreCard(resultText, isLastCustomer);
    UIManager.Instance?.HideOrderTicket();

    Destroy(bowl.gameObject);
    Destroy(waitingCustomer.gameObject);

    if (spawner != null && !isLastCustomer)
        spawner.NotifyCustomerServed();

    player.FaceOrderStation();
    UpdateButtonVisibility();
}

    private Customer FindWaitingCustomer()
    {
        foreach (Customer c in FindObjectsOfType<Customer>())
        {
            if (!c.HasBowl)
                return c;
        }
        return null;
    }
 
    private Bowl FindBowlAtOrderStation()
    {
        Bowl[] bowls = FindObjectsOfType<Bowl>();
        if (bowls.Length > 0)
            return bowls[0];
        return null;
    }
 
    private void UpdateButtonVisibility()
    {
        if (serveButton == null || player == null)
            return;
 
        bool shouldShow = (player.currentView == PlayerViewController.Station.OrderStation) && (FindWaitingCustomer() != null);
        serveButton.gameObject.SetActive(shouldShow);
    }
}