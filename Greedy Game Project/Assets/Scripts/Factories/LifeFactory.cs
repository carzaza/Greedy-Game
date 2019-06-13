using UnityEngine;

/// <summary> The factory for Lives. </summary>
public class LifeFactory : MonoBehaviour
{
    // The object to create the new objects from.
    public GameObject flyWeightLife;

    /// <summary> Creates a new Life Game Object from an existing one (FlyWeight Pattern). </summary>
    /// <param name="position"> The position to create the object in. </param>
    /// <returns> The new Life Game Object. </returns>
    public GameObject CreateLife(Vector2 position)
    {
        return CommonFactory.CreateCommonObject(flyWeightLife, "Life", position);
    }
}