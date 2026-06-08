using UnityEngine;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    protected float waitTimer;
    private Animator animator;
    public float speed = 2f;
    public Vector3 target;
    private bool isMoving;
    HashSet<Material> order;
    HashSet<Material> actualIngredients;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            isMoving = false;
            animator.Play("idle");
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
}
