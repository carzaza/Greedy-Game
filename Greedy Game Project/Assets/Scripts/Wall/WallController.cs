using UnityEngine;

/// <summary> The controller for the Wall Game Objects. </summary>
public class WallController : MonoBehaviour
{
    public AudioSource collisionSound;

    /// <summary> 
    /// Detects if another <see cref="GameObject"/> enters in the collider of this Game Object.
    /// </summary>
    /// <param name="collision"> The collision. </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionSound.Play();
        }
    }
}