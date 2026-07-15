using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPopUp : PrintText
{
    public GameObject optionPopUp;
    public GameObject settingPopUp;

    public void OnClickOptionPopUp()
    {
        optionPopUp.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickCloseOptionPopUp()
    {
        optionPopUp.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickMainMenu()
    {
        optionPopUp.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickDataClear()
    {
        GameManager.instance.ClearData();

        PrintTexts();
    }

    public void OnClickSettingPopUp()
    {
        settingPopUp.SetActive(true);
    }

    public void OnClickCloseSettingPopUp()
    {
        settingPopUp.SetActive(false);
    }
}
