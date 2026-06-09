using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    private List<IngredientType> ingredients = new();

    public void AddIngredient(IngredientType ingredient)
    {
        ingredients.Add(ingredient);
    }

    public List<IngredientType> GetIngredients()
    {
        return ingredients;
    }
}