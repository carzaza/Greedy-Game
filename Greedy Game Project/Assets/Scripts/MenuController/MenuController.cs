using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> The controller associated with the menus. </summary>
public class MenuController : MonoBehaviour
{
    private Mediator Mediator;

    public Image BrightnessImage;

    private Text finalScore;

    private GameObject configurationMenu, pauseMenu, endGameMenu;
    private GameObject gameOverImage, victoryImage;
    private GameObject onImage, offImage, musicOnImage, musicOffImage;

    public AudioSource winSound, gameOverSound;

    private Slider brightnessSlider;

    private bool soundActivated, musicActivated;

    private float brightness = 0.0f;

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        brightnessSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    /// <summary> Awake is called when the script instance is being loaded. </summary>
    private void Awake()
    {
        Mediator = GameObject.Find("Mediator").GetComponent<Mediator>();
        brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();

        finalScore = GameObject.Find("ScoreFinal").GetComponent<Text>();

        pauseMenu = GameObject.Find("PauseMenu");
        configurationMenu = GameObject.Find("ConfigurationMenu");
        endGameMenu = GameObject.Find("EndGameMenu");

        onImage = GameObject.Find("OnImage");
        offImage = GameObject.Find("OffImage");
        onImage.SetActive(true);
        offImage.SetActive(false);

        musicOnImage = GameObject.Find("MusicOnImage");
        musicOffImage = GameObject.Find("MusicOffImage");
        musicOnImage.SetActive(true);
        musicOffImage.SetActive(false);

        gameOverImage = GameObject.Find("GameOver");
        victoryImage = GameObject.Find("Victory");

        configurationMenu.SetActive(false);
        pauseMenu.SetActive(false);
        endGameMenu.SetActive(false);

        Color c = BrightnessImage.color;

        try
        {
            c.a = 1 - GlobalControl.Instance.brightness;
        }
        catch (Exception)
        {
            c.a = 0.0f;
        }

        BrightnessImage.color = c;
    }

    /// <summary> Update is called once per frame. </summary>
    private void Update()
    {
        if (GlobalControl.Instance.musicActivated)
        {
            musicOnImage.SetActive(true);
            musicOffImage.SetActive(false);
        }
        else
        {
            musicOnImage.SetActive(false);
            musicOffImage.SetActive(true);
        }

        if (GlobalControl.Instance.soundActivated)
        {
            onImage.SetActive(true);
            offImage.SetActive(false);
        }
        else
        {
            onImage.SetActive(false);
            offImage.SetActive(true);
        }
    }

    /// <summary> Shows the menu associated with the end of the game. </summary>
    /// <param name="victory"> 
    /// Indicates if the victory screen must be shown or the game over screen.
    /// </param>
    /// <returns> An empty IEnumerator that enables this method to be called in a coroutine. </returns>
    public IEnumerator ShowEndGameMenu(bool victory)
    {
        if (victory)
        {
            gameOverImage.SetActive(false);
            victoryImage.SetActive(true);
        }
        else
        {
            gameOverImage.SetActive(true);
            victoryImage.SetActive(false);
        }

        endGameMenu.SetActive(true);
        finalScore.text = $"Puntos: {GlobalControl.Instance.score}";

        Button newGameButton = GameObject.Find("NuevaPartida").GetComponent<Button>();
        Button exitButton = GameObject.Find("Salir").GetComponent<Button>();

        newGameButton.onClick.AddListener(NewGameButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);

        yield return new WaitUntil(() => !gameOverSound.isPlaying && !winSound.isPlaying);

        AudioListener.pause = true;
    }

    /// <summary> Shows the menu associated with the pause of the game. </summary>
    /// <remarks> Closes the pause menu in case it is already opened. </remarks>
    public void ShowPauseMenu()
    {
        if (GameIsPaused())
        {
            pauseMenu.SetActive(false);
            configurationMenu.SetActive(false);

            ResumeButtonClicked();

            return;
        }

        AudioListener.pause = true;
        pauseMenu.SetActive(true);

        Button resumeButton = GameObject.Find("Reanudar").GetComponent<Button>();
        Button saveGameButton = GameObject.Find("GuardarPartida").GetComponent<Button>();
        Button configurationButton = GameObject.Find("Configuracion").GetComponent<Button>();
        Button exitButton = GameObject.Find("Salir").GetComponent<Button>();

        Time.timeScale = 0;

        resumeButton.onClick.RemoveAllListeners();
        saveGameButton.onClick.RemoveAllListeners();
        configurationButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

        resumeButton.onClick.AddListener(ResumeButtonClicked);
        saveGameButton.onClick.AddListener(SaveButtonClicked);
        configurationButton.onClick.AddListener(ConfigurationButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
    }

    /// <summary> Checks if the game is paused. </summary>
    /// <returns> True if the game is paused, false in the other case. </returns>
    private bool GameIsPaused()
    {
        return pauseMenu.activeSelf || configurationMenu.activeSelf;
    }

    /// <summary> Checks the new value of the slider. </summary>
    /// <remarks> This method is called each time the slider value changes. </remarks>
    public void ValueChangeCheck()
    {
        Color c = BrightnessImage.color;
        c.a = 1 - brightnessSlider.value;

        BrightnessImage.color = c;

        GlobalControl.Instance.brightness = brightnessSlider.value;
    }

    /// <summary> Saves the current game. </summary>
    private void SaveButtonClicked()
    {
        SaveLoad.Save(GlobalControl.Instance);
    }

    /// <summary> Closes the pause menu and calls the configuration menu to be opened. </summary>
    private void ConfigurationButtonClicked()
    {
        pauseMenu.SetActive(false);

        ConfigureGame();
    }

    /// <summary> Opens the configuration menu. </summary>
    public void ConfigureGame()
    {
        SaveInitialState();

        configurationMenu.SetActive(true);
        brightnessSlider.value = GlobalControl.Instance.brightness;

        Button soundButton = GameObject.Find("SoundButton").GetComponent<Button>();
        Button ambientMusicButton = GameObject.Find("AmbientMusicButton").GetComponent<Button>();
        Button okButton = GameObject.Find("OkButton").GetComponent<Button>();
        Button cancelButton = GameObject.Find("CancelButton").GetComponent<Button>();

        soundButton.onClick.RemoveAllListeners();
        ambientMusicButton.onClick.RemoveAllListeners();
        okButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        soundButton.onClick.AddListener(SoundButtonClicked);
        ambientMusicButton.onClick.AddListener(AmbientMusicButtonClicked);
        okButton.onClick.AddListener(OkButtonClicked);
        cancelButton.onClick.AddListener(CancelButtonClicked);
    }

    /// <summary> Activates or deactivates the game sound depending on the initial state. </summary>
    private void SoundButtonClicked()
    {
        if (onImage.activeSelf)
        {
            onImage.SetActive(false);
            offImage.SetActive(true);

            GlobalControl.Instance.soundActivated = false;
        }
        else
        {
            onImage.SetActive(true);
            offImage.SetActive(false);

            GlobalControl.Instance.soundActivated = true;
        }
    }

    /// <summary> Activates or deactivates the ambient music depending on the initial state. </summary>
    private void AmbientMusicButtonClicked()
    {
        if (musicOnImage.activeSelf)
        {
            musicOnImage.SetActive(false);
            musicOffImage.SetActive(true);

            // TODO: Refactor this value assigning the state of the ON image to the value.
            GlobalControl.Instance.musicActivated = false;
        }
        else
        {
            musicOnImage.SetActive(true);
            musicOffImage.SetActive(false);

            GlobalControl.Instance.musicActivated = true;
        }
    }

    /// <summary> Applies the new chosen configuration. </summary>
    private void OkButtonClicked()
    {
        CloseConfigurationMenu();
    }

    /// <summary> Resets the configuration. </summary>
    private void CancelButtonClicked()
    {
        ResetInitialState();

        CloseConfigurationMenu();
    }

    /// <summary> Closes the configuration menu. </summary>
    private void CloseConfigurationMenu()
    {
        configurationMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    /// <summary> Saves the configuration when the menu is called. </summary>
    private void SaveInitialState()
    {
        soundActivated = GlobalControl.Instance.soundActivated;
        musicActivated = GlobalControl.Instance.musicActivated;
        brightness = GlobalControl.Instance.brightness;
    }

    /// <summary> Resets the configuration as it was when the menu was called. </summary>
    private void ResetInitialState()
    {
        GlobalControl.Instance.soundActivated = soundActivated;
        GlobalControl.Instance.musicActivated = musicActivated;

        if (GlobalControl.Instance.soundActivated)
        {
            onImage.SetActive(true);
            offImage.SetActive(false);
        }
        else
        {
            onImage.SetActive(false);
            offImage.SetActive(true);
        }

        if (GlobalControl.Instance.musicActivated)
        {
            musicOnImage.SetActive(true);
            musicOffImage.SetActive(false);
        }
        else
        {
            musicOnImage.SetActive(false);
            musicOffImage.SetActive(true);
        }

        GlobalControl.Instance.brightness = brightness;
        brightnessSlider.value = brightness;
    }

    /// <summary> Closes the pause menu and continues the game. </summary>
    private void ResumeButtonClicked()
    {
        pauseMenu.SetActive(false);

        Mediator.MuteSound(!GlobalControl.Instance.soundActivated);

        Mediator.MuteMusic(!GlobalControl.Instance.musicActivated);

        AudioListener.pause = false;
        Time.timeScale = 1;
    }

    /// <summary> Starts a new game starting from level one. </summary>
    private void NewGameButtonClicked()
    {
        GlobalControl.Instance.calories = 0;
        GlobalControl.Instance.damage = 0;
        GlobalControl.Instance.level = 1;
        GlobalControl.Instance.score = 0;
        GlobalControl.Instance.lives = 3;

        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    /// <summary> Closes the game and goes to the main menu. </summary>
    private void ExitButtonClicked()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}