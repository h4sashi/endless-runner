using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Hanzo;
using Hanzo.AudioSystem;

public class GameController : MonoBehaviour
{
    public CameraSwitch cameraSwitch;

    public static bool isPaused = false;
    public GameObject pausePanel, pauseButton, powerUpCanvas;

    public Slider musicSlider, sfxSlider;
    public GameObject musicDisabledICO;
    public GameObject sfxDisabledICO;

    // private const string TOPDISTANCE = "TOP_DISTANCE";

    [SerializeField] TextMeshProUGUI currentDistance;
    [SerializeField] TextMeshProUGUI topDistance;

    private void Start()
    {
        Resume();
        MusicSliderState();
        SFXSliderState();

        //Music State Method()
        musicDisabledICO.SetActive(false);

        string musicString = Hanzo.AudioSystem.AudioSettings.MUSIC_STATE.ToString();
        string musicKey = PlayerPrefs.GetString(musicString);

        if (musicKey == "MUTE")
        {
            AudioManager.Instance.musicSource.mute = true;
            musicDisabledICO.SetActive(true);
        }
        else
        {
            AudioManager.Instance.musicSource.mute = false;
            musicDisabledICO.SetActive(false);
        }

        // SFX State Method()
        sfxDisabledICO.SetActive(false);

        string sfxString = Hanzo.AudioSystem.AudioSettings.SFX_STATE.ToString();
        string sfxKey = PlayerPrefs.GetString(sfxString);

        if (sfxKey == "MUTE")
        {
            AudioManager.Instance.sfxSource.mute = true;
            sfxDisabledICO.SetActive(true);
        }
        else
        {
            AudioManager.Instance.sfxSource.mute = false;
            sfxDisabledICO.SetActive(false);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (isPaused == false)
            Resume();

        else
            Pause();

    }

    public void Pause()
    {

        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        // QuizContainer.SetActive(false);
        powerUpCanvas.SetActive(false);
        GetDistance(currentDistance.name);
        GetTopDistance(topDistance.name);


    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(!true);
        pauseButton.SetActive(!false);
        // QuizContainer.SetActive(true);
        powerUpCanvas.SetActive(true);

    }
    public void Sound()
    {
        // Toggle On or Off
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void GetDistance(string CurrentDistance)
    {
        currentDistance.text = GameObject.Find(CurrentDistance).GetComponent<TextMeshProUGUI>().text;
        currentDistance.text = GameObject.Find("distanceText (TMP)").GetComponent<TextMeshProUGUI>().text;

    }

    void GetTopDistance(string HighestScoreText)
    {
        topDistance.text = GameObject.Find(HighestScoreText).GetComponent<TextMeshProUGUI>().text;
        string TopDistanceText = Hanzo.ScoreParam.TOP_DISTANCE.ToString();
        topDistance.text = PlayerPrefs.GetInt(TopDistanceText).ToString();
    }

    public void ToggleMusic()
    {
        string musicState;

        AudioManager.Instance.ToggleMusic();
        if (AudioManager.Instance.musicSource.mute == true)
        {
            musicDisabledICO.SetActive(true);
            musicState = "MUTE";
            string Key = Hanzo.AudioSystem.AudioSettings.MUSIC_STATE.ToString();
            PlayerPrefs.SetString(Key, musicState);


        }
        else
        {
            musicDisabledICO.SetActive(false);
            musicState = " ";
            string Key = Hanzo.AudioSystem.AudioSettings.MUSIC_STATE.ToString();
            PlayerPrefs.SetString(Key, musicState);
        }
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
        if (AudioManager.Instance.sfxSource.mute == true)
        {
            sfxDisabledICO.SetActive(true);
        }
        else
        {
            sfxDisabledICO.SetActive(false);
        }
    }


    public void MusicVolume()
    {
        float musicSliderValueState;


        AudioManager.Instance.MusicVolume(musicSlider.value);
        if (musicSlider.value <= 0)
        {
            musicDisabledICO.SetActive(true);
            musicSliderValueState = musicSlider.value;
            string Key = Hanzo.AudioSystem.AudioSettings.MUSIC_SLIDER_STATE.ToString();
            PlayerPrefs.SetFloat(Key, musicSliderValueState);
        }
        else
        {
            musicDisabledICO.SetActive(false);
            musicSliderValueState = musicSlider.value;
            string Key = Hanzo.AudioSystem.AudioSettings.MUSIC_SLIDER_STATE.ToString();
            PlayerPrefs.SetFloat(Key, musicSliderValueState);
        }
    }

    public void SFXVolume()
    {
        float sfxSliderValueState;

        AudioManager.Instance.SFXVolume(sfxSlider.value);

        if (sfxSlider.value <= 0)
        {
            sfxDisabledICO.SetActive(true);
            sfxSliderValueState = sfxSlider.value;
            string Key = Hanzo.AudioSystem.AudioSettings.SFX_SLIDER_STATE.ToString();
            PlayerPrefs.SetFloat(Key, sfxSliderValueState);
        }
        else
        {
            sfxDisabledICO.SetActive(false);
            sfxSliderValueState = sfxSlider.value;
            string Key = Hanzo.AudioSystem.AudioSettings.SFX_SLIDER_STATE.ToString();
            PlayerPrefs.SetFloat(Key, sfxSliderValueState);
        }
    }

    private void SfxState()
    {


        string sfxString = Hanzo.AudioSystem.AudioSettings.SFX_STATE.ToString();
        string sfxKey = PlayerPrefs.GetString(sfxString);

        if (sfxKey == "MUTE")
        {
            AudioManager.Instance.sfxSource.mute = true;
            sfxDisabledICO.SetActive(true);
        }
        else
        {
            AudioManager.Instance.sfxSource.mute = false;
            sfxDisabledICO.SetActive(false);
        }
    }

    private void MusicState()
    {

        string musicString = Hanzo.AudioSystem.AudioSettings.MUSIC_STATE.ToString();
        string musicKey = PlayerPrefs.GetString(musicString);

        if (musicKey == "MUTE")
        {
            AudioManager.Instance.musicSource.mute = true;
            musicDisabledICO.SetActive(true);
        }
        else
        {
            AudioManager.Instance.musicSource.mute = false;
            musicDisabledICO.SetActive(false);
        }
    }

    private void MusicSliderState()
    {
        string musicSliderKey = Hanzo.AudioSystem.AudioSettings.MUSIC_SLIDER_STATE.ToString();
        float musicSliderValue;

        if (PlayerPrefs.HasKey(musicSliderKey))
        {
            musicSliderValue = PlayerPrefs.GetFloat(musicSliderKey);
            AudioManager.Instance.MusicVolume(musicSliderValue);
            musicSlider.value = musicSliderValue;
            if (musicSliderValue <= 0)
            {
                musicDisabledICO.SetActive(true);
            }
            else
            {
                musicDisabledICO.SetActive(true);
            }
            Debug.Log("music volume " + musicSliderValue);
        }

    }

    private void SFXSliderState()
    {
        string sfxSliderKey = Hanzo.AudioSystem.AudioSettings.SFX_SLIDER_STATE.ToString();
        float sfxSliderValue;

        if (PlayerPrefs.HasKey(sfxSliderKey))
        {
            sfxSliderValue = PlayerPrefs.GetFloat(sfxSliderKey);
            AudioManager.Instance.SFXVolume(sfxSliderValue);
            sfxSlider.value = sfxSliderValue;
            if (sfxSliderValue <= 0)
            {
                sfxDisabledICO.SetActive(true);
            }
            else
            {
                sfxDisabledICO.SetActive(false);
            }

            Debug.Log("music volume " + sfxSliderValue);

        }
    }







}
