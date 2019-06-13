using UnityEngine;

/// <summary> The factory for Rocks. </summary>
public class RockFactory : MonoBehaviour
{
    // The Game Object to create the new Rocks from.
    public GameObject flyWeightRock;

    /// <summary> Creates a new Rock Game Object from an existing one (FlyWeight Pattern). </summary>
    /// <param name="position"> The position to create the Game Object in. </param>
    /// <returns> The new created Rock. </returns>
    public GameObject CreateRock(Vector2 position)
    {
        return CommonFactory.CreateCommonObject(flyWeightRock, "Rock", position);
    }
}