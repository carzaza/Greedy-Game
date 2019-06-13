using UnityEngine;

/// <summary> The factory for Energy Capsules. </summary>
public class EnergyCapsuleFactory : MonoBehaviour
{
    // The Game Object to create the new ones from.
    public GameObject flyWeightEnergyCapsule;

    /// <summary> 
    /// Creates a new Energy Capsule from an existing one, to reuse the common data (FlyWeight Pattern).
    /// </summary>
    /// <param name="position"> The position to create the Energy Capsule in. </param>
    /// <returns> The created Energy Capsule. </returns>
    public GameObject CrateEnergyCapsule(Vector2 position)
    {
        return CommonFactory.CreateCommonObject(flyWeightEnergyCapsule, "EnergyCapsule", position);
    }
}