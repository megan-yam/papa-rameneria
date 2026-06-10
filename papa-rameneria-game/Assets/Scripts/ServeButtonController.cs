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

        RectTransform rect = buttonObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1f, 1f);
        rect.anchorMax = new Vector2(1f, 1f);
        rect.pivot = new Vector2(1f, 0.5f);
        rect.anchoredPosition = new Vector2(-20f, -125f);
        rect.sizeDelta = new Vector2(125f, 30f);

        Image image = buttonObject.GetComponent<Image>();
        ApplyImageStyle(image, stationButton);

        Outline outline = buttonObject.AddComponent<Outline>();
        outline.effectColor = new Color(0.74f, 0.55f, 0.32f, 1f);
        outline.effectDistance = new Vector2(2f, -2f);

        serveButton = buttonObject.GetComponent<Button>();
        ApplyButtonStyle(serveButton, stationButton);
        serveButton.onClick.AddListener(Serve);

        GameObject textObject = new GameObject("Text (TMP)", typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        textObject.transform.SetParent(buttonObject.transform, false);

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        TextMeshProUGUI label = textObject.GetComponent<TextMeshProUGUI>();
        label.text = "serve";
        label.alignment = TextAlignmentOptions.Center;
        label.fontSize = 14f;
        label.color = Color.black;
        label.raycastTarget = false;
        ApplyTextStyle(label, stationButton);
    }

    private Button FindStationButtonTemplate()
    {
        GameObject stationButtonObject = GameObject.Find("topping station");
        return stationButtonObject != null ? stationButtonObject.GetComponent<Button>() : null;
    }

    private void ApplyImageStyle(Image image, Button stationButton)
    {
        Image stationImage = stationButton != null ? stationButton.GetComponent<Image>() : null;

        if (stationImage != null)
        {
            image.sprite = stationImage.sprite;
            image.type = stationImage.type;
            image.color = stationImage.color;
            image.material = stationImage.material;
            image.pixelsPerUnitMultiplier = stationImage.pixelsPerUnitMultiplier;
            return;
        }

        image.sprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
        image.type = Image.Type.Sliced;
        image.color = Color.white;
    }

    private void ApplyButtonStyle(Button button, Button stationButton)
    {
        if (stationButton == null)
            return;

        button.transition = stationButton.transition;
        button.colors = stationButton.colors;
        button.spriteState = stationButton.spriteState;
        button.animationTriggers = stationButton.animationTriggers;
    }

    private void ApplyTextStyle(TextMeshProUGUI label, Button stationButton)
    {
        TextMeshProUGUI stationLabel = stationButton != null ? stationButton.GetComponentInChildren<TextMeshProUGUI>() : null;
        if (stationLabel == null)
            return;

        label.font = stationLabel.font;
        label.fontSharedMaterial = stationLabel.fontSharedMaterial;
        label.fontSize = stationLabel.fontSize;
        label.fontStyle = stationLabel.fontStyle;
        label.color = stationLabel.color;
        label.enableAutoSizing = stationLabel.enableAutoSizing;
        label.fontSizeMin = stationLabel.fontSizeMin;
        label.fontSizeMax = stationLabel.fontSizeMax;
    }

    private void Serve()
    {
        if (player == null)
            return;

        player.FaceOrderStation();
        UpdateButtonVisibility();
    }

    private void UpdateButtonVisibility()
    {
        if (serveButton == null || player == null)
            return;

        bool shouldShow = player.currentView == PlayerViewController.Station.ToppingStation;
        serveButton.gameObject.SetActive(shouldShow);
    }
}
