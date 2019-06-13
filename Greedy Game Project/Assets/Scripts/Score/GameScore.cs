using Assets.Scripts.Observer;
using UnityEngine;
using UnityEngine.UI;

/// <summary> The controller for the score in the game. </summary>
public class GameScore : MonoBehaviour, IObserver
{
    public int score;

    private Mediator Mediator;

    // The text shown in the Canvas.
    public Text Text { get; set; }

    /// <summary> 
    /// The action of being notified by a change in a subject this controller is observing.
    /// </summary>
    /// <param name="calories"> The consumed calories. </param>
    public void Actualiza(int calories)
    {
        score += calories;
        Text.text = "Puntos: " + score;
    }

    /// <summary> Awake is called when the script instance is being loaded. </summary>
    private void Awake()
    {
        Mediator = GameObject.Find("Mediator").GetComponent<Mediator>();
        Text = GameObject.Find("ScoreWindow").GetComponent<Text>();
    }

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        score = GlobalControl.Instance.score;
        Text.text = "Puntos: " + score;

        Mediator.AddFoodObserver(this);
    }

    /// <summary> Saves the current score in the <see cref="GlobalControl"/>. </summary>
    public void SaveScore()
    {
        GlobalControl.Instance.score = score;
    }
}