using System.Collections;
using UnityEngine;

/// <summary> The controller for the Life System. </summary>
public class LifeSystemController : MonoBehaviour
{
    // The Mediator.
    private Mediator Mediator;

    // The number of lives.
    public int lives;

    private GameObject player;

    private Vector2 playerSpawnInitPosition = new Vector2(0.0f, 0.0f);

    private GameObject FirstLife;
    private GameObject SecondLife;
    private GameObject ThirdLife;

    public AudioSource dieSound, wolfAttackSound, bombSound;

    /// <summary> This method is called when the script instance is being loaded. </summary>
    private void Awake()
    {
        Mediator = GameObject.Find("Mediator").GetComponent<Mediator>();
    }

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        lives = GlobalControl.Instance.lives;

        FirstLife = GameObject.Find("FirstLife");
        SecondLife = GameObject.Find("SecondLife");
        ThirdLife = GameObject.Find("ThirdLife");

        ShowLives();

        player = GameObject.Find("Player");

    }

    /// <summary> Provocates the death of the main player and the end of the game. </summary>
    public void Die()
    {
        DecreaseLife();
        ShowLives();

        if (lives > 0)
        {
            dieSound.Play();
            StartCoroutine(Respawn());
        }
        else
        {
            Mediator.EndGame(false);
        }
    }

    /// <summary> Provocates the reappearance of the main player. </summary>
    public IEnumerator Respawn()
    {
        GameObject[] bombObjects = GameObject.FindGameObjectsWithTag("Bomb");

        Time.timeScale = 0;

        yield return new WaitUntil(() => !wolfAttackSound.isPlaying && !bombSound.isPlaying);

        player.transform.position = playerSpawnInitPosition;

        Mediator.EnemyRespawn();

        Time.timeScale = 1;
    }

    /// <summary> Increases lives in an unit. </summary>
    public void IncreaseLife()
    {
        if (lives < 3)
        {
            lives++;
            ShowLives();
        }
    }

    /// <summary> Decrease lives in an unit. </summary>
    /// <remarks> Registers the death of the player if the number of lives is equals to zero. </remarks>
    public void DecreaseLife()
    {
        if (lives > 0)
        {
            Mediator.ResetDamage();
            lives--;
        }
    }

    /// <summary> Shows the available lives in the UI to the player. </summary>
    private void ShowLives()
    {
        if (lives == 3)
        {
            FirstLife.SetActive(true);
            SecondLife.SetActive(true);
            ThirdLife.SetActive(true);
        }
        else if (lives == 2)
        {
            FirstLife.SetActive(true);
            SecondLife.SetActive(true);
            ThirdLife.SetActive(false);
        }
        else if (lives == 1)
        {
            FirstLife.SetActive(true);
            SecondLife.SetActive(false);
            ThirdLife.SetActive(false);
        }
        else
        {
            FirstLife.SetActive(false);
            SecondLife.SetActive(false);
            ThirdLife.SetActive(false);
        }
    }

    /// <summary> Saves the number of lives in the <see cref="GlobalControl"/>. </summary>
    public void SaveLives()
    {
        GlobalControl.Instance.lives = lives;
    }
}