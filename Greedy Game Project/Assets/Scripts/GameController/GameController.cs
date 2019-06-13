using Assets.Scripts.Observer;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> The controller for the Game. </summary>
public class GameController : MonoBehaviour, IObserver
{
    // The Mediator.
    private Mediator Mediator;

    // The current level.
    private int level;

    // The sounds for victory and game over.
    private AudioSource winSound, gameOverSound;

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        level = GlobalControl.Instance.level;

        InitializeLevel();

        Mediator.AddFoodObserver(this);
    }

    /// <summary> This method is called when the script instance is being loaded. </summary>
    private void Awake()
    {
        Mediator = GameObject.Find("Mediator").GetComponent<Mediator>();

        winSound = GameObject.Find("WinSoundAudioSource").GetComponent<AudioSource>();
        gameOverSound = GameObject.Find("GameOverAudioSource").GetComponent<AudioSource>();
    }

    /// <summary> Creates the Game Objects of the current level. </summary>
    private void InitializeLevel()
    {
        switch (level)
        {
            case 1:
                Mediator.CreateRandomObject(4, ObjectType.StandardApple);
                Mediator.CreateRandomObject(4, ObjectType.StandardCarrot);
                Mediator.CreateRandomObject(1, ObjectType.GoldenApple);
                Mediator.CreateRandomObject(1, ObjectType.GoldenCarrot);
                Mediator.CreateRandomObject(1, ObjectType.Life);
                Mediator.CreateRandomObject(2, ObjectType.Rock);
                Mediator.CreateRandomObject(3, ObjectType.Cactus);

                break;
            case 2:
                Mediator.CreateRandomObject(5, ObjectType.StandardApple);
                Mediator.CreateRandomObject(5, ObjectType.StandardCarrot);
                Mediator.CreateRandomObject(1, ObjectType.GoldenApple);
                Mediator.CreateRandomObject(1, ObjectType.GoldenCarrot);
                Mediator.CreateRandomObject(1, ObjectType.Life);
                Mediator.CreateRandomObject(3, ObjectType.Rock);
                Mediator.CreateRandomObject(3, ObjectType.Cactus);

                break;
            case 3:
                Mediator.CreateRandomObject(6, ObjectType.StandardApple);
                Mediator.CreateRandomObject(6, ObjectType.StandardCarrot);
                Mediator.CreateRandomObject(1, ObjectType.GoldenApple);
                Mediator.CreateRandomObject(1, ObjectType.GoldenCarrot);
                Mediator.CreateRandomObject(1, ObjectType.Life);
                Mediator.CreateRandomObject(5, ObjectType.Rock);
                Mediator.CreateRandomObject(4, ObjectType.Cactus);

                break;
            case 4:
                Mediator.CreateRandomObject(7, ObjectType.StandardApple);
                Mediator.CreateRandomObject(7, ObjectType.StandardCarrot);
                Mediator.CreateRandomObject(1, ObjectType.GoldenApple);
                Mediator.CreateRandomObject(1, ObjectType.GoldenCarrot);
                Mediator.CreateRandomObject(1, ObjectType.Life);
                Mediator.CreateRandomObject(6, ObjectType.Rock);
                Mediator.CreateRandomObject(4, ObjectType.Cactus);

                break;
            case 5:
                Mediator.CreateRandomObject(8, ObjectType.StandardApple);
                Mediator.CreateRandomObject(8, ObjectType.StandardCarrot);
                Mediator.CreateRandomObject(1, ObjectType.GoldenApple);
                Mediator.CreateRandomObject(1, ObjectType.GoldenCarrot);
                Mediator.CreateRandomObject(1, ObjectType.Life);
                Mediator.CreateRandomObject(6, ObjectType.Rock);
                Mediator.CreateRandomObject(3, ObjectType.Cactus);

                break;
        }
    }

    /// <summary> Ends the game. </summary>
    /// <param name="victory"> Indicates if the user has won or not. </param>
    public void EndGame(bool victory)
    {
        Time.timeScale = 0;

        Mediator.MuteMusic(true);

        if (GlobalControl.Instance.soundActivated)
        {
            if (victory)
            {
                winSound.Play();
            }
            else
            {
                gameOverSound.Play();
            }
        }
    }

    /// <summary> Starts the next level or ends the game if the user has won the last level. </summary>
    public void NextLevel()
    {
        level++;

        if (level == 6)
        {
            Mediator.EndGame(true);
        }
        else
        {
            SaveLevel();
            SceneManager.LoadScene(level);
        }
    }

    /// <summary> Saves the level in the <see cref="GlobalControl"/>. </summary>
    public void SaveLevel()
    {
        GlobalControl.Instance.level = level;
    }

    /// <summary> The action of being notified by the Food Controller of a food consumed. </summary>
    /// <param name="calories"> The calories of the consumed food. </param>
    public void Actualiza(int calories)
    {
        if (GameObject.FindGameObjectsWithTag("Food").Length == 1)
        {
            Mediator.NextLevel();
        }
    }
}