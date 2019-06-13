using Assets.Scripts.Observer;
using UnityEngine;

/// <summary> The food present in the scenery and eaten by the main player. </summary>
public class Food : Subject
{
    // The Mediator.
    private Mediator Mediator;

    // The calories associated to this food.
    public int Calories { get; set; } = 0;

    /// <summary> This method is called when the script instance is being loaded. </summary>
    protected void Awake()
    {
        Mediator = GameObject.Find("Mediator").GetComponent<Mediator>();
    }

    /// <summary> The action of consume the food. </summary>
    public void Consume()
    {
        Mediator.IncreaseCalories(Calories);
        Notify(Calories);

        gameObject.SetActive(false);
    }
}