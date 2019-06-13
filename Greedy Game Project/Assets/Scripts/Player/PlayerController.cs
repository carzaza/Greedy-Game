using System.Collections;
using UnityEngine;

/// <summary> The controller for the main player. </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;

    private Mediator Mediator;

    private Renderer playerRenderer;

    private bool inFood = false;
    public Animator playerAnimator;

    public AudioSource walkSound, eatSound, lifeUpSound, dieSound, explosionSound, energyCapsuleSound;

    private bool isMoving = false;

    private float horizontalMovement, verticalMovement;

    private Collider2D foodCollider;

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        Mediator = GameObject.Find("Mediator").GetComponent<Mediator>();
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    /// <summary> Update is called once per frame. </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Mediator.PauseGame();
        }

        Eat();

        WalkingSound();
    }

    /// <summary> 
    /// Pauses or unpauses the walking sound depending on whether the player is moving or not.
    /// </summary>
    private void WalkingSound()
    {
        if (isMoving)
        {
            walkSound.UnPause();
        }
        else
        {
            walkSound.Pause();
        }
    }

    /// <summary> This method is called each time a frame updates. </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary> 
    /// This method is called when another object enters a trigger collider attached to this object.
    /// </summary>
    /// <param name="collision"> The other collider associated with this collision. </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnergyCapsule"))
        {
            energyCapsuleSound.Play();
            Mediator.ResetDamage();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Food"))
        {
            foodCollider = collision;
            inFood = true;
        }
        if (collision.gameObject.CompareTag("Life"))
        {
            Mediator.IncreaseLife();
            lifeUpSound.Play();
            Destroy(collision.gameObject);
        }
    }

    /// <summary> 
    /// This method is called when another object exits a trigger collider attached to this object.
    /// </summary>
    /// <param name="collision"> The other collider associated with this collision. </param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            foodCollider = null;
            inFood = false;
        }
    }

    /// <summary> 
    /// Detects if another <see cref="GameObject"/> enters in the collider of this Game Object.
    /// </summary>
    /// <param name="collision"> The collision. </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Mediator.Die();
            StartCoroutine("Flasher");
        }
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Mediator.IncreaseDamage(30);
            StartCoroutine("Flasher");
        }
        if (collision.gameObject.CompareTag("Cactus"))
        {
            Mediator.IncreaseDamage(30);
            StartCoroutine("Flasher");
        }
    }

    /// <summary> The player movement. </summary>
    private void Move()
    {
        if (verticalMovement <= 0.5f && verticalMovement >= -0.5f)
            horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (horizontalMovement <= 0.5f && horizontalMovement >= -0.5f)
            verticalMovement = Input.GetAxisRaw("Vertical");

        if (horizontalMovement >= 0.5f)
        {
            transform.Translate(new Vector3(walkSpeed * Time.deltaTime * horizontalMovement, 0, 0));
            playerAnimator.SetInteger("Direction", 2);
            isMoving = true;
        }
        if (horizontalMovement <= -0.5f)
        {
            transform.Translate(new Vector3(walkSpeed * Time.deltaTime * horizontalMovement, 0, 0));
            playerAnimator.SetInteger("Direction", 4);
            isMoving = true;
        }
        if (verticalMovement <= -0.5f)
        {
            transform.Translate(new Vector3(0, walkSpeed * Time.deltaTime * verticalMovement, 0));
            playerAnimator.SetInteger("Direction", 3);
            isMoving = true;
        }
        if (verticalMovement >= 0.5f)
        {
            transform.Translate(new Vector3(0, walkSpeed * Time.deltaTime * verticalMovement, 0));
            playerAnimator.SetInteger("Direction", 1);
            isMoving = true;
        }
        if (verticalMovement == 0 && horizontalMovement == 0)
        {
            playerAnimator.SetInteger("Direction", 0);
            isMoving = false;
        }
    }

    /// <summary> The action of eat the food. </summary>
    private void Eat()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inFood)
        {
            foodCollider.gameObject.GetComponent<Food>()?.Consume();
            eatSound.Play();
            inFood = false;
        }
    }

    /// <summary> Provocates the Greedy to flashes. </summary>
    /// <returns> An empty IEnumerator that enables this method to be called in a coroutine. </returns>
    private IEnumerator Flasher()
    {
        playerAnimator.SetInteger("Direction", 0);
        for (int i = 0; i < 5; i++)
        {
            playerRenderer.material.color = Color.red;
            yield return new WaitForSeconds(.1f);
            playerRenderer.material.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    }
}