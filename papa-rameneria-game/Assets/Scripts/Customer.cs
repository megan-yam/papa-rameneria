using UnityEngine;
using System.Collections.Generic;
 
public class Customer : MonoBehaviour
{
    // FIX: waitTimer now only starts after the customer arrives,
    // not immediately on spawn (was ticking during isMoving=false default state).
    protected float waitTimer = 0f;
    private bool hasArrived = false;
 
    private Animator animator;
    public Animator Animator => animator;
    public float speed = 2f;
    private Vector3 target;
    private bool isMoving = false;
    private CustomerOrder order;
 
    // FIX: Added public SetBowl() so ServeButtonController (or bowl itself)
    // can assign this before grading. Was never assigned before — caused
    // NullReferenceException on every Calculate* call.
    private Bowl actualIngredients;
    public void SetBowl(Bowl bowl) { actualIngredients = bowl; }
    public bool HasBowl => actualIngredients != null;
 
    private CustomerDialogue dialogue;
 
    void Awake()
    {
        animator = GetComponent<Animator>();
        order = GetComponent<CustomerOrder>();
        dialogue = GetComponent<CustomerDialogue>();
    }
 
    void Update()
    {
        if (animator != null)
            animator.SetBool("isWalking", isMoving);
 
        if (isMoving)
        {
            Move();
        }
        else if (hasArrived) // FIX: only tick timer after arrival
        {
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
            hasArrived = true; // FIX: mark arrival so timer can begin
            if (dialogue != null)
                dialogue.StartDialogue();
        }
    }
 
    public float CalculateAccuracy()
{
    // Safety check if no bowl was handed over
    if (actualIngredients == null || order == null)
        return 0f;

    // Get the list of physical GameObjects from the bowl
    List<GameObject> bowlItems = actualIngredients.getIngredients();

    // 1. Convert the physical bowl GameObjects into a list of IngredientTypes
    List<IngredientType> cookedIngredients = new List<IngredientType>();
    foreach (GameObject item in bowlItems)
    {
        if (item != null)
        {
            Ingredient ing = item.GetComponent<Ingredient>();
            if (ing != null)
            {
                cookedIngredients.Add(ing.ingredientType);
            }
        }
    }

    // 2. Create a checklist out of the customer's exact order requirements
    List<IngredientType> customerExpectations = new List<IngredientType>(order.order);

    float correctCount = 0f;
    float penalty = 0f;

    // 3. Score every item the player prepared
    foreach (IngredientType type in cookedIngredients)
    {
        // If the customer wanted this, check it off their list
        if (customerExpectations.Contains(type))
        {
            customerExpectations.Remove(type);
            correctCount++;
        }
        else
        {
            // Player added something that wasn't ordered! 10 point deduction
            penalty += 10f;
        }
    }

    // 4. Calculate the base percentage based on how many ordered items were successfully provided
    if (order.order.Count == 0) return 0f; // Prevent division by zero
    
    float baseAccuracy = (correctCount / order.order.Count) * 100f;
    float finalScore = baseAccuracy - penalty;

    // Keep the score strictly bounded between 0% and 100%
    return Mathf.Clamp(finalScore, 0f, 100f);
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
        if (actualIngredients == null)
            return 0f;
 
        FoodState.CookState status = actualIngredients.GetNoodleCookState();
        switch (status)
        {
            case FoodState.CookState.Raw:    return 0f;
            case FoodState.CookState.Cooked: return 100f;
            case FoodState.CookState.Burnt:  return 50f;
            default:                         return 0f;
        }
    }
 
    public float CalculateTotalRating()
    {
        float accuracy   = CalculateAccuracy();
        float timeliness = CalculateTimeliness();
        float cooking    = CalculateCooking();
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
            text += ingredient + "\n";
        return text;
    }
}