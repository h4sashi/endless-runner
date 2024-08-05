using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using Hanzo.AudioSystem;


public class HomeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinScore;

    [SerializeField] private List<string> Levels;

    [SerializeField] private TextMeshProUGUI TopDistanceText;

    public GameObject[] MenuComponents;
    bool clearData = false;

    public GameObject sfxDisabledICO, musicDisabledICO;
    public Slider musicSlider, sfxSlider;
    public CharacterSelect characterSelect;
    public GameObject characterContainer;
    public GameObject campaignContainer;
    public GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt(Hanzo.ScoreParam.TOP_COINSCORE.ToString(), 200);
        string loadCoin = Hanzo.ScoreParam.TOP_COINSCORE.ToString();
        coinScore.text = $"{PlayerPrefs.GetInt(loadCoin).ToString()}";

        string loadDistance = Hanzo.ScoreParam.TOP_DISTANCE.ToString();
        TopDistanceText.text = $"{PlayerPrefs.GetInt(loadDistance).ToString()}";

        //Saving Settings For Audio Manager
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



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        int randomIndex = Random.Range(0, Levels.Count);
        string level = Levels[randomIndex];

        SceneManager.LoadScene(level);
    }

    public void CharacterSelect()
    {
        for (int i = 1; i < MenuComponents.Length; i++)
        {
            MenuComponents[i].SetActive(false);
        }

        characterContainer.SetActive(true);
        characterSelect.enabled = true;
    }

    public void LeaderboardButton()
    {
        //    messageText.gameObject.SetActive(true);
    }

    public void SettingsButton()
    {

        MenuComponents[0].gameObject.SetActive(true);

        for (int i = 1; i < MenuComponents.Length; i++)
        {
            MenuComponents[i].gameObject.SetActive(false);
        }


    }

    public void CloseSettings()
    {
        if (clearData == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            for (int i = 1; i < MenuComponents.Length; i++)
            {
                MenuComponents[i].gameObject.SetActive(true);
            }
            MenuComponents[0].gameObject.SetActive(false);
        }


    }

    public void ClearData()
    {
        clearData = true;
        PlayerPrefs.DeleteAll();

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
            musicState = "";
            string Key = Hanzo.AudioSystem.AudioSettings.MUSIC_STATE.ToString();
            PlayerPrefs.SetString(Key, musicState);
        }
    }

    public void ToggleSFX()
    {
        string sfxState;

        AudioManager.Instance.ToggleSFX();
        if (AudioManager.Instance.sfxSource.mute == true)
        {

            sfxState = "MUTE";
            string Key = Hanzo.AudioSystem.AudioSettings.SFX_STATE.ToString();
            PlayerPrefs.SetString(Key, sfxState);
            sfxDisabledICO.SetActive(true);
        }
        else
        {

            sfxState = "";
            string Key = Hanzo.AudioSystem.AudioSettings.SFX_STATE.ToString();
            PlayerPrefs.SetString(Key, sfxState);
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

            Debug.Log("sfx volume " + sfxSliderValue);

        }
    }

    public void CampaignButton()
    {
        for (int i = 0; i < MenuComponents.Length; i++)
        {
            MenuComponents[i].gameObject.SetActive(false);
        }
        campaignContainer.SetActive(true);
        Player.SetActive(false);

    }


}



