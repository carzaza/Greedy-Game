using UnityEngine;

/// <summary> The controller associated with a Rock. </summary>
public class RockController : MonoBehaviour
{
    public AudioSource CollisionSound;

    /// <summary> 
    /// Detects if another <see cref="GameObject"/> enters in the collider of this Game Object.
    /// </summary>
    /// <param name="collision"> The collision. </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollisionSound.Play();
        }
    }
}