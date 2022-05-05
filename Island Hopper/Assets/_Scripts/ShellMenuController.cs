using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShellMenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    void Start() {
        Cursor.visible = true;
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        volumeTextValue.text = PlayerPrefs.GetFloat("masterVolume").ToString("0.0");
        getScreenMode();
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitGameDialogYes()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }

    public void setFullScreenMode()
    {
        PlayerPrefs.SetInt("masterFullScreen", 1);
        Screen.fullScreen = true;
    }
    
    public void setWindowScreenMode()
    {
        PlayerPrefs.SetInt("masterFullScreen", 0);
        Screen.fullScreen = false;
    }

    private void getScreenMode()
    {
        int mode = PlayerPrefs.GetInt("masterFullScreen");

        if (mode == 1)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
    }
}
