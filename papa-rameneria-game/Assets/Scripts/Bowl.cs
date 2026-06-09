using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    private List<GameObject> ingredients = new();

    public void AddIngredient(GameObject ingredient)
    {
        ingredients.Add(ingredient);
    }

    public List<GameObject> getIngredients()
    {
        return ingredients;
    }

    public IngredientType GetIngredientType(GameObject obj)
    {
        return obj.GetComponent<Ingredient>().ingredientType;
    }


    public FoodState.CookState GetNoodleCookState()
    {
        foreach (GameObject ingredient in ingredients)
        {
            FoodState foodState = ingredient.GetComponent<FoodState>();

            if (foodState != null)
            {
                return foodState.state;
            }
        }

        return FoodState.CookState.Raw; // fallback
    }
}