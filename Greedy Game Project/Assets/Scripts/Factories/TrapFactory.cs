using UnityEngine;

/// <summary> The factory for Trap Game Objects. </summary>
public class TrapFactory : MonoBehaviour
{
    // The Cactus Game Object to create the new ones from.
    public GameObject flyWeightCactus;

    /// <summary> Creates a Cactus Game Object from an existing one (FlyWeight Pattern). </summary>
    /// <param name="position"> The position to create object in. </param>
    /// <returns> The new Cactus Game Object. </returns>
    public GameObject CreateCactus(Vector2 position)
    {
        return CommonFactory.CreateCommonObject(flyWeightCactus, "Cactus", position);
    }
}