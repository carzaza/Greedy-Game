using Assets.Scripts.Observer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary> The class implementing the Mediator Pattern. </summary>
public class Mediator : MonoBehaviour
{
    private FoodController FoodController;
    private DamageSystemController DamageSystemController;
    private LifeSystemController LifeSystemController;
    private SoundController SoundController;
    private MenuController MenuController;
    private GameController GameController;
    private EnemyController EnemyController;

    private FoodFactory foodFactory;
    private LifeFactory lifeFactory;
    private TrapFactory trapFactory;
    private EnergyCapsuleFactory energyCapsuleFactory;
    private RockFactory rockFactory;

    private List<Vector2> possiblePositions = new List<Vector2>();
    private int size = 0;

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        InitializePositions();
    }

    private void Awake()
    {
        FoodController = GameObject.Find("FoodController").GetComponent<FoodController>();
        DamageSystemController = GameObject.Find("DamageSystemController").GetComponent<DamageSystemController>();
        LifeSystemController = GameObject.Find("LifeSystemController").GetComponent<LifeSystemController>();
        SoundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        MenuController = GameObject.Find("MenuController").GetComponent<MenuController>();
        EnemyController = GameObject.Find("EnemyController").GetComponent<EnemyController>();

        foodFactory = GameObject.Find("FoodFactory").GetComponent<FoodFactory>();
        lifeFactory = GameObject.Find("LifeFactory").GetComponent<LifeFactory>();
        trapFactory = GameObject.Find("TrapFactory").GetComponent<TrapFactory>();
        energyCapsuleFactory = GameObject.Find("EnergyCapsuleFactory").GetComponent<EnergyCapsuleFactory>();
        rockFactory = GameObject.Find("RockFactory").GetComponent<RockFactory>();
    }

    /// <summary> Add an observer to each Food in the game. </summary>
    /// <param name="observer"> The observer to add. </param>
    public void AddFoodObserver(IObserver observer)
    {
        FoodController.AddFoodObserver(observer);
    }

    /// <summary> Ends the game. </summary>
    /// <param name="victory"> Indicates if the user has won or not. </param>
    public void EndGame(bool victory)
    {
        GameController.EndGame(victory);
        StartCoroutine(MenuController.ShowEndGameMenu(victory));
    }

    /// <summary> Pauses the game. </summary>
    public void PauseGame()
    {
        MenuController.ShowPauseMenu();
    }

    /// <summary> Mutes or unmutes the sound depending on the parameter. </summary>
    /// <param name="mute"> Indicates if the sound must be muted or not. </param>
    public void MuteSound(bool mute)
    {
        SoundController.MuteSound(mute);
    }

    /// <summary> Mutes or unmutes the music depending on the paramenter. </summary>
    /// <param name="mute"> Indicates if the music must be muted or not. </param>
    public void MuteMusic(bool mute)
    {
        SoundController.MuteMusic(mute);
    }

    /// <summary> Makes the player die. </summary>
    public void Die()
    {
        LifeSystemController.Die();
    }

    /// <summary> Increases the life of the player in one unit. </summary>
    public void IncreaseLife()
    {
        LifeSystemController.IncreaseLife();
    }

    /// <summary> Increases the damage of the player in the given quantity. </summary>
    /// <param name="damage"> The damage to increase to the user. </param>
    public void IncreaseDamage(double damage)
    {
        DamageSystemController.IncreaseDamage(damage);
    }

    /// <summary> Decreases the damage of the user. </summary>
    public void DecreaseDamage()
    {
        DamageSystemController.DecreaseDamage();
    }

    /// <summary> Resets to zero the damage of the user. </summary>
    public void ResetDamage()
    {
        DamageSystemController.ResetDamage();
    }

    /// <summary> Increases the consumed calories in the given quantity. </summary>
    /// <param name="calories"> The calories to increase. </param>
    public void IncreaseCalories(int calories)
    {
        FoodController.IncreaseCalories(calories);
    }

    /// <summary> Passes to the next level. </summary>
    public void NextLevel()
    {
        LifeSystemController.SaveLives();
        DamageSystemController.SaveDamage();
        FoodController.SaveCalories();
        GameController.NextLevel();
    }

    /// <summary> Creates a given number of the given Game Object type. </summary>
    /// <param name="objectsNumber"> The number of Game Objects to create. </param>
    /// <param name="objectType"> The type of the objects to create. </param>
    /// <returns> The created Game Objects. </returns>
    public IEnumerable<GameObject> CreateRandomObject(int objectsNumber, ObjectType objectType)
    {
        List<GameObject> createdGameObjects = new List<GameObject>();

        for (int i = 0; i < objectsNumber; i++)
        {
            createdGameObjects.Add(CreateRandomObject(objectType));
        }

        return createdGameObjects;
    }

    /// <summary> Creates a random object of the given type in a random position. </summary>
    /// <param name="objectType"> The type of the object to create. </param>
    /// <returns> The created Game Object. </returns>
    private GameObject CreateRandomObject(ObjectType objectType)
    {
        Vector2 position = GetRandomPosition();

        switch (objectType)
        {
            case ObjectType.StandardApple:
            case ObjectType.GoldenApple:
            case ObjectType.StandardCarrot:
            case ObjectType.GoldenCarrot:
                return foodFactory.CreateFood(objectType, position);
            case ObjectType.Life:
                return lifeFactory.CreateLife(position);
            case ObjectType.Cactus:
                return trapFactory.CreateCactus(position);
            case ObjectType.EnergyCapsule:
                return energyCapsuleFactory.CrateEnergyCapsule(position);
            case ObjectType.Rock:
                return rockFactory.CreateRock(position);
            default:
                return null;
        }
    }

    /// <summary> Gets a random position from a collection of possible positions. </summary>
    /// <returns> A random position. </returns>
    private Vector2 GetRandomPosition()
    {
        int random = Random.Range(0, size - 1);

        Vector2 position = possiblePositions.ElementAt(random);

        possiblePositions.RemoveAt(random);
        size--;

        return position;
    }

    /// <summary> Initializes the possible positions to create a Game Object in. </summary>
    private void InitializePositions()
    {
        possiblePositions.Add(new Vector2(-1.5f, -6.5f));
        possiblePositions.Add(new Vector2(6.5f, 2.0f));
        possiblePositions.Add(new Vector2(6.5f, 5.5f));
        possiblePositions.Add(new Vector2(-1.5f, 2.0f));
        possiblePositions.Add(new Vector2(-4.0f, -2.0f));
        possiblePositions.Add(new Vector2(-6.0f, -6.0f));
        possiblePositions.Add(new Vector2(2.0f, -3.0f));
        possiblePositions.Add(new Vector2(6.0f, -4.5f));
        possiblePositions.Add(new Vector2(3.5f, 4.5f));
        possiblePositions.Add(new Vector2(-0.5f, -4.5f));
        possiblePositions.Add(new Vector2(-4.5f, 2.0f));
        possiblePositions.Add(new Vector2(-2.0f, -2.5f));
        possiblePositions.Add(new Vector2(2.0f, 0.0f));
        possiblePositions.Add(new Vector2(4.5f, -3.0f));
        possiblePositions.Add(new Vector2(-3.0f, 5.5f));
        possiblePositions.Add(new Vector2(-0.5f, 3.5f));
        possiblePositions.Add(new Vector2(-6.5f, -3.0f));
        possiblePositions.Add(new Vector2(1.5f, 5.0f));
        possiblePositions.Add(new Vector2(4.5f, 1.5f));
        possiblePositions.Add(new Vector2(-4.5f, 5.0f));
        possiblePositions.Add(new Vector2(-0.5f, -2.0f));
        possiblePositions.Add(new Vector2(-3.0f, -4.5f));
        possiblePositions.Add(new Vector2(-4.0f, 0.0f));
        possiblePositions.Add(new Vector2(-3.0f, 3.5f));
        possiblePositions.Add(new Vector2(-3.5f, -6.5f));
        possiblePositions.Add(new Vector2(6.5f, -1.0f));
        possiblePositions.Add(new Vector2(-6.0f, -1.0f));
        possiblePositions.Add(new Vector2(-4.5f, -6.5f));
        possiblePositions.Add(new Vector2(1.0f, -1.5f));
        possiblePositions.Add(new Vector2(3.0f, 3.0f));
        possiblePositions.Add(new Vector2(-6.0f, 3.5f));
        possiblePositions.Add(new Vector2(1.0f, -6.0f));
        possiblePositions.Add(new Vector2(6.0f, -6.5f));
        possiblePositions.Add(new Vector2(-4.5f, -5.5f));

        size = possiblePositions.ToArray().Length;
    }

    public void EnemyRespawn()
    {
        EnemyController.EnemyRespawn();
    }
}