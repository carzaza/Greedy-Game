using System;
using UnityEngine;

/// <summary> The factory for Foods. </summary>
public class FoodFactory : MonoBehaviour
{
    // The Game Object to creates the new ones from.
    public GameObject flyWeightStandardApple, flyWeightGoldenApple, flyWeightStandardCarrot, flyWeightGoldenCarrot;

    // The calories of a standard apple.
    private static readonly int StandardAppleCalories = 20;

    // The calories of a golden apple.
    private static readonly int GoldenAppleCalories = 30;

    // The calories of a standard carrot.
    private static readonly int StandardCarrotCalories = 40;

    // The calories of a golden carrot.
    private static readonly int GoldenCarrotCalories = 50;

    /// <summary> Creates a new Food from an existing one (FlyWeight Pattern). </summary>
    /// <param name="objectType"> The type of the Food to create. </param>
    /// <param name="position"> The position to create the Food in. </param>
    /// <returns> The new Food Game Object. </returns>
    public GameObject CreateFood(ObjectType objectType, Vector2 position)
    {
        GameObject flyWeightObject;
        int calories;

        switch (objectType)
        {
            case ObjectType.StandardApple:
                flyWeightObject = flyWeightStandardApple;
                calories = StandardAppleCalories;

                break;
            case ObjectType.GoldenApple:
                flyWeightObject = flyWeightGoldenApple;
                calories = GoldenAppleCalories;

                break;
            case ObjectType.StandardCarrot:
                flyWeightObject = flyWeightStandardCarrot;
                calories = StandardCarrotCalories;

                break;
            case ObjectType.GoldenCarrot:
                flyWeightObject = flyWeightGoldenCarrot;
                calories = GoldenCarrotCalories;

                break;
            default:
                throw new Exception("Operation not supported.");
        }

        GameObject foodGameObject = CommonFactory.CreateCommonObject(flyWeightObject, objectType.ToString(), position);
        foodGameObject.AddComponent<Food>().Calories = calories;

        return foodGameObject;
    }
}