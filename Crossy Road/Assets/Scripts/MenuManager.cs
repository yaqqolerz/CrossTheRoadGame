using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private const string MutePrefKey = "MutePref";

    public Sprite muteSprite;
    public Sprite unmuteSprite;
    public Image muteButtonImage;

    private float currentVolume;
    private bool isMuted;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        LoadSettings();
        /*ApplySettings();*/
        GameObject.Find("MuteToggle").GetComponent<UnityEngine.UI.Toggle>().isOn = isMuted;
    }

    public void SetMute(bool isMute)
    {
        isMuted = isMute;
        audioMixer.SetFloat("Volume", isMuted ? -80 : currentVolume);
        PlayerPrefs.SetInt(MutePrefKey, isMuted ? 1 : 0);
        muteButtonImage.sprite = isMuted ? muteSprite : unmuteSprite;
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(MutePrefKey))
        {
            isMuted = PlayerPrefs.GetInt(MutePrefKey) == 1 ? true : false;
        }
        else
        {
            isMuted = false;
        }
    }
    public void PlayGame()
    {
        /* ApplySettings();*/
        Debug.Log(PlayerPrefs.GetFloat("Volume"));
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }
}
