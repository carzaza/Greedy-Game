using Assets.Scripts.Observer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary> The Controller for Food Game Objects. </summary>
public class FoodController : MonoBehaviour
{
    // The Mediator.
    private Mediator Mediator;

    // The calories captured by the player.
    public int Calories { get; private set; }

    // The maximun calories account.
    private static readonly float MAX_VALUE = 100;

    // The Bar that shows the captured calories in the Canvas.
    private SimpleHealthBar caloriesBar;

    /// <summary> This method is called when the script instance is being loaded. </summary>
    private void Awake()
    {
        Mediator = GameObject.Find("Mediator").GetComponent<Mediator>();
        caloriesBar = GameObject.Find("CaloriesBar").GetComponent<SimpleHealthBar>();
    }

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        Calories = GlobalControl.Instance.calories;

        UpdateBar();
    }

    /// <summary> Increase the quantity of the captured calories in the given quantity. </summary>
    /// <param name="calories"> 
    /// The amount to increase the quantity of the captured calories in.
    /// </param>
    public void IncreaseCalories(int calories)
    {
        GlobalControl.Instance.score += calories;

        Calories += calories;
        UpdateBar();

        if (Calories >= MAX_VALUE)
        {
            Mediator.DecreaseDamage();
            Calories -= 100;
        }

        UpdateBar();
    }

    /// <summary> Updates the bar that shows the calories collected in the Canvas. </summary>
    private void UpdateBar()
    {
        caloriesBar.UpdateBar(Calories, MAX_VALUE);
    }

    /// <summary> Saves the calories in the <see cref="GlobalControl"/>. </summary>
    public void SaveCalories()
    {
        GlobalControl.Instance.calories = Calories;
    }

    /// <summary> Add the given observer to each food object. </summary>
    /// <param name="observer"> The observer to add. </param>
    public void AddFoodObserver(IObserver observer)
    {
        IEnumerable<Food> foods = GameObject.FindGameObjectsWithTag("Food").Select(go => go.GetComponent<Food>());

        foreach (Food food in foods)
        {
            food.AddObserver(observer);
        }
    }
}