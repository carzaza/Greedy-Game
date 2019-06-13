using UnityEngine;

/// <summary> The controller associated with the Bomb Game Objects. </summary>
public class BombController : MonoBehaviour
{
    public AudioSource explosionSound;
    public Animator bombAnimator;

    /// <summary> 
    /// Detects if another <see cref="GameObject"/> enters in the collider of this Game Object.
    /// </summary>
    /// <param name="collision"> The collision. </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bombAnimator.SetBool("explotando", true);
            explosionSound.Play();
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}