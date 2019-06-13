using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed, walkTime, walkCounter, waitTime, waitCounter;

    private Rigidbody2D myRigidbody;

    public bool isWalking;
    private int walkDirection;

    public Animator loboAnimator;
    public AudioSource attackSound;

    /// <summary> Start is called before the first frame update </summary>
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    /// <summary> Update is called once per frame. </summary>
    private void Update()
    {
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector2(0, moveSpeed);
                    loboAnimator.SetInteger("Direccion", 0);
                    break;

                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed, 0);
                    loboAnimator.SetInteger("Direccion", 1);
                    break;

                case 2:
                    myRigidbody.velocity = new Vector2(0, -moveSpeed);
                    loboAnimator.SetInteger("Direccion", 2);
                    break;

                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    loboAnimator.SetInteger("Direccion", 3);
                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector2.zero;
            loboAnimator.SetInteger("Direccion", 4);

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    /// <summary> Chooses a random direction to turn. </summary>
    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    /// <summary> 
    /// Detects if another <see cref="GameObject"/> enters in the collider of this Game Object.
    /// </summary>
    /// <param name="collision"> The collision. </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Food"))
        {
            isWalking = false;
            myRigidbody.velocity = Vector2.zero;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            attackSound.Play();
        }
    }
}