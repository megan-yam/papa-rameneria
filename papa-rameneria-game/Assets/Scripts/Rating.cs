using System.Collections.Generic;
using UnityEngine;

public class Rating : MonoBehaviour
{
    public float accuracy; // right ingredients?
    public float timeliness; // on time?
    public float cook; // not undercooked or overcooked?
    HashSet<Material> correctIngredients;
    HashSet<Material> actualIngredients;

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateAccuracy()
    {
        float correct = correctIngredients.Count;
        foreach (Material ingredient in correctIngredients)
        {
            if (!actualIngredients.Contains(ingredient))
            {
                correct -= 1;
            }
        }
        return correct / correctIngredients.Count;
    }

    

    public float CalculateRating()
    {
        return (accuracy + timeliness + cook) / 3f;
    }
}
