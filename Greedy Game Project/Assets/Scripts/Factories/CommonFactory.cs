using UnityEngine;

/// <summary> A class for creating Game Objects. </summary>
public static class CommonFactory
{
    /// <summary> 
    /// Creates a new Game Object from an existing one, to reuse the common data (FlyWeight Pattern).
    /// </summary>
    /// <param name="flyWeightObject"> The object to create this object from. </param>
    /// <param name="objectName"> The name of the Game Object. </param>
    /// <param name="position"> The position to create the Game Object in. </param>
    /// <returns>  </returns>
    public static GameObject CreateCommonObject(GameObject flyWeightObject, string objectName, Vector2 position)
    {
        GameObject gameObject = new GameObject()
        {
            name = objectName,
            tag = flyWeightObject.tag
        };

        SpriteRenderer flyWeightSprite = flyWeightObject.GetComponent<SpriteRenderer>();
        PolygonCollider2D flyWeightCollider = flyWeightObject.GetComponent<PolygonCollider2D>();

        SpriteRenderer sprite = gameObject.AddComponent<SpriteRenderer>();
        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        gameObject.transform.localScale = flyWeightObject.transform.localScale;

        sprite.sprite = flyWeightSprite.sprite;
        sprite.sortingLayerName = flyWeightSprite.sortingLayerName;
        collider.points = flyWeightCollider.points;
        collider.isTrigger = flyWeightCollider.isTrigger;

        gameObject.transform.position = position;

        return gameObject;
    }
}