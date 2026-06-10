using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Active Order Ticket")]
    public GameObject orderTicketPanel;
    public TextMeshProUGUI ticketText;

    [Header("Score Card Overlay")]
    public GameObject scoreCardPanel;
    public TextMeshProUGUI scoreText;
    public Button continueButton;

    [Header("Game Over Screen")]
    public GameObject gameOverPanel;
    public Button resetButton;

    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;

        if (orderTicketPanel != null) orderTicketPanel.SetActive(false);
        if (scoreCardPanel != null) scoreCardPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (continueButton != null)
            continueButton.onClick.AddListener(OnContinuePressed);

        if (resetButton != null)
            resetButton.onClick.AddListener(ResetGame);
    }

    public void DisplayOrderTicket(System.Collections.Generic.List<IngredientType> ingredients)
    {
        if (orderTicketPanel == null || ticketText == null) return;
        orderTicketPanel.SetActive(true);

        string orderDetails = "<color=#FFA500><b>ACTIVE RAMEN ORDER</b></color>\n";
        foreach (IngredientType item in ingredients)
            orderDetails += $"\n• {item}";
        ticketText.text = orderDetails;
    }

    public void HideOrderTicket()
    {
        if (orderTicketPanel != null) orderTicketPanel.SetActive(false);
    }

    public void ShowScoreCard(string scoreReportData, bool lastCustomer = false)
    {
        isGameOver = lastCustomer;
        if (scoreCardPanel != null) scoreCardPanel.SetActive(true);
        if (scoreText != null) scoreText.text = scoreReportData;
    }

    private void OnContinuePressed()
    {
        if (scoreCardPanel != null) scoreCardPanel.SetActive(false);

        if (isGameOver)
        {
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HideScoreCard()
    {
        if (scoreCardPanel != null) scoreCardPanel.SetActive(false);
    }
}