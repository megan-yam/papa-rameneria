using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    public HashSet<IngredientType> order;

    void Awake()
    {
        GenerateOrder();
    }

    void GenerateOrder()
    {
        order = new HashSet<IngredientType>();
        IngredientType[] noodles =
        {
            IngredientType.ThickNoodles,
            IngredientType.ThinNoodles,
            IngredientType.NormalNoodles
        };

        IngredientType[] soups =
        {
            IngredientType.Tonkatsu,
            IngredientType.Miso,
            IngredientType.Shoyu
        };

        IngredientType[] proteins =
        {
            IngredientType.Egg,
            IngredientType.Pork,
            IngredientType.Tofu
        };

        IngredientType[] veggies =
        {
            IngredientType.GreenOnion,
            IngredientType.FishCake
        };

        // Required ingredients
        order.Add(noodles[Random.Range(0, noodles.Length)]);
        order.Add(soups[Random.Range(0, soups.Length)]);
        order.Add(proteins[Random.Range(0, proteins.Length)]);

        // Random veggie count (0-2)
        int veggieCount = Random.Range(0, veggies.Length + 1);

        List<IngredientType> availableVeggies =
            new List<IngredientType>(veggies);

        for (int i = 0; i < veggieCount; i++)
        {
            int index = Random.Range(0, availableVeggies.Count);
            order.Add(availableVeggies[index]);
            availableVeggies.RemoveAt(index);
        }
    }

    public bool ContainsIngredient(IngredientType ingredient)
    {
        return order.Contains(ingredient);
    }
}
