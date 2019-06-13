using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Image BrightnessImage;

    private Button resumeButton, NewGameButton, SettingsButton, exitButton;
    private Slider brightnessSlider;

    private GameObject onImage, offImage, musicOnImage, musicOffImage;

    private GameObject configurationMenu, mainMenu;

    private Button soundButton, ambientMusicButton, okButton, cancelButton;

    private bool soundActivated, musicActivated;

    private float brightness = 0.0f;

    private AudioSource mainMenuMusic;

    /// <summary> Start is called before the first frame update. </summary>
    private void Start()
    {
        brightnessSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
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

    /// <summary> Update is called once per frame. </summary>
    private void Update()
    {
        CheckBrightness();

        if (GlobalControl.Instance.musicActivated)
        {
            mainMenuMusic.mute = false;
            musicOnImage.SetActive(true);
            musicOffImage.SetActive(false);
        }
        else
        {
            mainMenuMusic.mute = true;
            musicOnImage.SetActive(false);
            musicOffImage.SetActive(true);
        }
    }

    /// <summary> This method is called when the script instance is being loaded. </summary>
    public void Awake()
    {
        mainMenuMusic = GameObject.Find("MainMenuMusic").GetComponent<AudioSource>();
        mainMenuMusic.Play();

        mainMenuMusic.mute = !GlobalControl.Instance?.musicActivated ?? false;

        mainMenu = GameObject.Find("MainMenu");
        resumeButton = GameObject.Find("Reanude").GetComponent<Button>();
        NewGameButton = GameObject.Find("NewGame").GetComponent<Button>();
        SettingsButton = GameObject.Find("Settings").GetComponent<Button>();
        exitButton = GameObject.Find("ExitGame").GetComponent<Button>();

        configurationMenu = GameObject.Find("ConfigurationMenu");

        onImage = GameObject.Find("OnImage");
        offImage = GameObject.Find("OffImage");
        onImage.SetActive(true);
        offImage.SetActive(false);

        musicOnImage = GameObject.Find("MusicOnImage");
        musicOffImage = GameObject.Find("MusicOffImage");
        musicOnImage.SetActive(true);
        musicOffImage.SetActive(false);

        soundButton = GameObject.Find("SoundButton").GetComponent<Button>();
        ambientMusicButton = GameObject.Find("AmbientMusicButton").GetComponent<Button>();
        okButton = GameObject.Find("OkButton").GetComponent<Button>();
        cancelButton = GameObject.Find("CancelButton").GetComponent<Button>();

        brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();

        configurationMenu.SetActive(false);

        resumeButton.onClick.AddListener(ResumeButtonClick);
        NewGameButton.onClick.AddListener(NewGameButtonClick);
        SettingsButton.onClick.AddListener(SettingsButtonClick);
        exitButton.onClick.AddListener(ExitButtonClick);
    }

    /// <summary> Adjust the brightness of the game depending on the configuration. </summary>
    private void CheckBrightness()
    {
        Color c = BrightnessImage.color;
        c.a = 1 - GlobalControl.Instance.brightness;

        BrightnessImage.color = c;
    }

    /// <summary> Resumes the game. </summary>
    private void ResumeButtonClick()
    {
        Game data = SaveLoad.Load();

        GlobalControl.Instance.score = data.score;
        GlobalControl.Instance.calories = data.calories;
        GlobalControl.Instance.damage = data.damage;
        GlobalControl.Instance.lives = data.lives;
        GlobalControl.Instance.level = data.level;
        GlobalControl.Instance.brightness = data.brightness;
        GlobalControl.Instance.musicActivated = data.musicActivated;
        GlobalControl.Instance.soundActivated = data.soundActivated;

        SceneManager.LoadScene(data.level);
    }

    /// <summary> Starts a new game starting from level one. </summary>
    private void NewGameButtonClick()
    {
        GlobalControl.Instance.score = 0;
        GlobalControl.Instance.calories = 0;
        GlobalControl.Instance.damage = 0;
        GlobalControl.Instance.lives = 3;
        GlobalControl.Instance.level = 1;

        SaveLoad.Save(GlobalControl.Instance);

        SceneManager.LoadScene(1);
    }

    /// <summary> Closes the main menu and calls the configuration menu to be opened. </summary>
    private void SettingsButtonClick()
    {
        mainMenu.SetActive(false);

        OpenConfigurationMenu();
    }

    /// <summary> Opens the configuration menu. </summary>
    private void OpenConfigurationMenu()
    {
        SaveInitialState();

        configurationMenu.SetActive(true);
        brightnessSlider.value = GlobalControl.Instance.brightness;

        soundButton.onClick.RemoveAllListeners();
        ambientMusicButton.onClick.RemoveAllListeners();
        okButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        soundButton.onClick.AddListener(SoundButtonClicked);
        ambientMusicButton.onClick.AddListener(AmbientMusicButtonClicked);
        okButton.onClick.AddListener(OkButtonClicked);
        cancelButton.onClick.AddListener(CancelButtonClicked);
    }

    /// <summary> Activates or deactivates the sound of the game. </summary>
    private void SoundButtonClicked()
    {
        if (onImage.activeSelf)
        {
            onImage.SetActive(false);
            offImage.SetActive(true);
        }
        else
        {
            onImage.SetActive(true);
            offImage.SetActive(false);
        }

        GlobalControl.Instance.soundActivated = onImage.activeSelf;
    }

    /// <summary> Activates or deactivates the ambient music. </summary>
    private void AmbientMusicButtonClicked()
    {
        if (musicOnImage.activeSelf)
        {
            musicOnImage.SetActive(false);
            musicOffImage.SetActive(true);
        }
        else
        {
            musicOnImage.SetActive(true);
            musicOffImage.SetActive(false);
        }

        GlobalControl.Instance.musicActivated = musicOnImage.activeSelf;
        mainMenuMusic.mute = !GlobalControl.Instance.musicActivated;
    }

    /// <summary> Applies the new chosen configuration. </summary>
    private void OkButtonClicked()
    {
        soundButton.onClick.RemoveAllListeners();

        CloseConfigurationMenu();
    }

    /// <summary> Closes the configuration menu. </summary>
    private void CloseConfigurationMenu()
    {
        configurationMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    /// <summary> Resets the configuration. </summary>
    private void CancelButtonClicked()
    {
        soundButton.onClick.RemoveAllListeners();

        ResetInitialState();

        soundButton.CancelInvoke();

        CloseConfigurationMenu();
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

        mainMenuMusic.mute = !GlobalControl.Instance.musicActivated;
        brightnessSlider.value = brightness;
    }

    /// <summary> Quits the application. </summary>
    private void ExitButtonClick()
    {
        Application.Quit();
    }
}