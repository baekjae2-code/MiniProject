using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPopUp : PrintText
{
    public GameObject optionPopUp;
    public GameObject settingPopUp;

    public GameObject hpbarShowBtn;
    public GameObject hpbarHideBtn;
    public GameObject hpbarObj;

    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        bgmSlider.value = GameManager.instance.bgmSoundVolume;
        sfxSlider.value = GameManager.instance.sfxSoundVolume;

        SoundManager.instance.OnBGMSettingChanged(bgmSlider.value);
        SoundManager.instance.OnSFXSettingChanged(sfxSlider.value);
    }

    public void OnClickOptionPopUp()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        optionPopUp.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickCloseOptionPopUp()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        optionPopUp.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickQuit()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        Application.Quit();
    }

    public void OnClickMainMenu()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        optionPopUp.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.ChangeBgm();
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickDataClear()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        GameManager.instance.ClearData();

        PrintTexts();
    }

    public void OnClickSettingPopUp()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        settingPopUp.SetActive(true);
    }

    public void OnClickCloseSettingPopUp()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        settingPopUp.SetActive(false);
    }

    public void OnClickHPBarShow()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        hpbarHideBtn.SetActive(true);
        hpbarShowBtn.SetActive(false);
        hpbarObj.SetActive(true);
    }
    public void OnClickHPBarHide()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        hpbarHideBtn.SetActive(false);
        hpbarShowBtn.SetActive(true);
        hpbarObj.SetActive(false);
    }

    public void OnBGMAudioChanged()
    {
        SoundManager.instance.OnBGMSettingChanged(bgmSlider.value);
    }

    public void OnSFXAudioChanged()
    {
        SoundManager.instance.OnSFXSettingChanged(sfxSlider.value);
    }
}
