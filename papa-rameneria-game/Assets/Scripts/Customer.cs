using UnityEngine;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    protected float waitTimer;
    private Animator animator;
    public Animator Animator => animator;
    public float speed = 2f;
    private Vector3 target;
    private bool isMoving = false;
    private CustomerOrder order;
    // Noodles: thick, thin, normal
    // Soup: Tonkotsu, Miso, Shoyu
    // Protein: eggs, pork, tofu
    // Veggies: green onion, fishcake
    // HashSet<IngredientType> actualIngredients;
    private Bowl actualIngredients;
    private CustomerDialogue dialogue;


    void Awake()
    {
        animator = GetComponent<Animator>();
        order = GetComponent<CustomerOrder>();
        dialogue = GetComponent<CustomerDialogue>();
    }

    void Update()
    {
        animator.SetBool("isWalking", isMoving);
        if (isMoving)
        {
            Move();
        }
        else
        {
            // timer starts once customer gets to front
            waitTimer += Time.deltaTime;
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) <= 0.01f)
        {
            transform.position = target;
            isMoving = false;
            if (dialogue != null)
            {
                dialogue.EnableInteraction();
            }
        }
    }

    public float CalculateAccuracy()
    {
        float correct = 0;
        foreach (GameObject ingredient in actualIngredients.getIngredients())
        {
            IngredientType type = actualIngredients.GetIngredientType(ingredient);
            if (order.order.Contains(type))
            {
                correct += 1;
            }
        }
        return (float)correct / order.order.Count * 100f;
    }

    public float CalculateTimeliness()
    {
        float maxGoodTime = 150f;
        float decayRate = 0.2f;

        if (waitTimer <= maxGoodTime)
            return 100f;

        float score = 100f - (waitTimer - maxGoodTime) * decayRate;

        return Mathf.Clamp(score, 0f, 100f);
    }

    public float CalculateCooking()
    {
        FoodState.CookState status = actualIngredients.GetNoodleCookState();
        switch (status)
        {
            case FoodState.CookState.Raw:
                return 0;
            case FoodState.CookState.Cooked:
                return 100;
            case FoodState.CookState.Burnt:
                return 50;
            default:
                return 0;
        }
    }

    public float CalculateTotalRating()
    {
        float accuracy = CalculateAccuracy();
        float timeliness = CalculateTimeliness();
        float cooking = CalculateCooking();

        return (accuracy + timeliness + cooking) / 3f;
    }

    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
        isMoving = true;
    }

    public string GetOrderText()
    {
        string text = "";

        foreach (IngredientType ingredient in order.order)
        {
            text += ingredient + "\n";
        }

        return text;
    }
}
