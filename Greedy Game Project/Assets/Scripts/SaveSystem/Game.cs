using System;

[Serializable]
public class Game
{
    public static Game current;

    public int score, lives, calories, level;

    public bool musicActivated, soundActivated;

    public double damage;

    public float brightness;

    /// <summary> Saves the game. </summary>
    /// <param name="globalControl"> The Object containing the global configuration of the game. </param>
    public Game(GlobalControl globalControl)
    {
        score = globalControl.score;
        lives = globalControl.lives;
        damage = globalControl.damage;
        calories = globalControl.calories;
        level = globalControl.level;
        brightness = globalControl.brightness;
        musicActivated = globalControl.musicActivated;
        soundActivated = globalControl.soundActivated;
    }
}