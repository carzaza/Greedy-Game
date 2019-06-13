using UnityEngine;

/// <summary> 
/// The Object to transfer data between scenes. This object has an unique instance (Singleton Pattern).
/// </summary>
public class GlobalControl : MonoBehaviour
{
    // The instance of the Global Control.
    public static GlobalControl Instance;

    // The accumulated score in the game.
    public int score;

    // The lives of the player.
    public int lives;

    // The damage received by the player.
    public double damage;

    // The calories captured by the player.
    public int calories;

    // The level.
    public int level;

    // The brightness.
    public float brightness;

    // Indicates if the music is activated.
    public bool musicActivated;

    // Indicates if the sound is activated.
    public bool soundActivated;

    /// <summary> This method is called when the script instance is being loaded. </summary>
    private void Awake()
    {
        if (Instance is null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
}