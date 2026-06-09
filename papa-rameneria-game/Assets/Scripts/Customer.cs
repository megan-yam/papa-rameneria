using UnityEngine;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    protected float waitTimer;
    private Animator animator;
    public float speed = 2f;
    private Vector3 target;
    private bool isMoving = false;
    // private bool hasArrived;
    HashSet<Material> order;
    // protein: pork, fishcakes, tofu, egg
    // noodle types: thick and thin
    // veggies: green onions, bok choy, mushrooms, bean sprouts
    HashSet<Material> actualIngredients;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
{
    Debug.Log(gameObject.name + " | controller: " +
        animator.runtimeAnimatorController);
        Debug.Log(gameObject.name + " animator object = " + animator.gameObject.name);
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
        // if (hasArrived) return;
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) <= 0.01f)
        {
            transform.position = target;
            isMoving = false;
            // hasArrived = true;
            // animator.SetBool("isWalking", isMoving);
        }
    }

    public float CalculateAccuracy()
    {
        float correct = 0;
        foreach (Material ingredient in actualIngredients)
        {
            if (order.Contains(ingredient))
            {
                correct += 1;
            }
        }
        return (float)correct / order.Count * 100f;
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
        // TODO: Check if material is cooked
        float score = 100;
        return score;
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
        // hasArrived = false;
        // animator.SetBool("isWalking", isMoving);
    }
}
