using System.Linq;
using UnityEngine;

/// <summary> The controller associated with the Damage System. </summary>
public class DamageSystemController : MonoBehaviour
{
    // The Mediator.
    private Mediator Mediator;

    // The Energy Capsule that is randomly shown one time each level.
    private GameObject energyCapsule;

    // The damage that the user has received.
    public double Damage { get; set; }

    // The Bar to show the damage and the calories in the Canvas.
    private SimpleHealthBar healthBar;

    // Indicates if the Energy Capsule has been created and destroyed.
    private bool energyCapsuleCreated, energyCapsuleDeleted = false;

    private readonly float MinTime = 2;
    private readonly float MaxTime = 10;
    private float currentTime, randomCreateTime, randomDeleteTime;

    // The maximum damage amount.
    private static readonly float MAX_VALUE = 100;

    /// <summary> This method is called when the script instance is being loaded. </summary>
    protected void Awake()
    {
        Mediator = GameObject.Find("Mediator").GetComponent<Mediator>();
        healthBar = GameObject.Find("DamageBar").GetComponent<SimpleHealthBar>();
    }

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        Damage = GlobalControl.Instance.damage;

        UpdateBar();

        randomCreateTime = Random.Range(MinTime, MaxTime);
        randomDeleteTime = randomCreateTime + Random.Range(5, 10);
    }

    /// <summary> 
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        //Check if its the right time to create the object
        if (currentTime >= randomCreateTime && !energyCapsuleCreated)
        {
            energyCapsule = Mediator.CreateRandomObject(1, ObjectType.EnergyCapsule).FirstOrDefault();

            energyCapsuleCreated = true;
        }
        else if (currentTime >= randomDeleteTime && !energyCapsuleDeleted)
        {
            energyCapsule.SetActive(false);

            energyCapsuleDeleted = true;
        }
    }

    /// <summary> Decrease the damage in a ten percent. </summary>
    public void DecreaseDamage()
    {
        if (Damage > 10)
        {
            Damage -= 10;
        }
        else
        {
            Damage = 0;
        }

        UpdateBar();
    }

    /// <summary> Sets the damage to zero. </summary>
    public void ResetDamage()
    {
        Damage = 0;

        UpdateBar();
    }

    /// <summary> Increases the damage in the given amount. </summary>
    /// <param name="damage"> The amount to increase the damage in. </param>
    public void IncreaseDamage(double damage)
    {
        Damage += damage;

        if (Damage >= 100)
        {
            Mediator.Die();
            Damage = 0;
        }

        UpdateBar();
    }

    /// <summary> Updates the Damage Bar in the Canvas to show the current damage. </summary>
    private void UpdateBar()
    {
        healthBar.UpdateBar((float)Damage, MAX_VALUE);
    }

    /// <summary> Saver the damage in the <see cref="GlobalControl"/>. </summary>
    public void SaveDamage()
    {
        GlobalControl.Instance.damage = Damage;
    }
}