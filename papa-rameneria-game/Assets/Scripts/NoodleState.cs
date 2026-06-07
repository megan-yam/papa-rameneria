using UnityEngine;

public class NoodleState : MonoBehaviour
{
    public enum CookState
    {
        Raw,
        Cooked,
        Burnt
    }

    public CookState state = CookState.Raw;

    public Material rawMat;
    public Material cookedMat;
    public Material burntMat;

    private Renderer noodleRenderer;

    void Start()
    {
        noodleRenderer = GetComponentInChildren<Renderer>();
        UpdateAppearance();
    }

    public void SetState(CookState newState)
    {
        state = newState;
        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        if (noodleRenderer == null)
            return;

        switch (state)
        {
            case CookState.Raw:
                noodleRenderer.material = rawMat;
                break;

            case CookState.Cooked:
                noodleRenderer.material = cookedMat;
                break;

            case CookState.Burnt:
                noodleRenderer.material = burntMat;
                break;
        }
    }
}