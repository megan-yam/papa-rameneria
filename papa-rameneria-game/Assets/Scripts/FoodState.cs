using UnityEngine;

public class FoodState : MonoBehaviour
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

    private Renderer[] renderers;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Start()
    {
        UpdateAppearance();
    }

    public void SetState(CookState newState)
    {
        state = newState;
        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        if (renderers == null)
            return;

        switch (state)
        {
            case CookState.Raw:
                foreach (Renderer r in renderers)
                {
                    r.material = rawMat;
                }
                break;

            case CookState.Cooked:
                foreach (Renderer r in renderers)
                {
                    r.material = cookedMat;
                }
                break;

            case CookState.Burnt:
                foreach (Renderer r in renderers)
                {
                    r.material = burntMat;
                }
                break;
        }
    }
}