using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] Transform gameName;
    [SerializeField] Transform gameStartBtn;
    [SerializeField] Transform gameSettingBtn;
    [SerializeField] Transform gameQuitBtn;

    [SerializeField] Transform mainScreenImg;
    [SerializeField] Transform setScreenImg;

    void Start()
    {
        float gameNameP = gameName.position.y;
        float gameStartBtnP = gameStartBtn.position.y;
        float gameSettingBtnP = gameSettingBtn.position.y;
        float gameQuitBtnP = gameQuitBtn.position.y;

        gameName.position = new Vector2(gameName.position.x, 1500);
        gameStartBtn.position = new Vector2(gameStartBtn.position.x, -200);
        gameSettingBtn.position = new Vector2(gameSettingBtn.position.x, -125);
        gameQuitBtn.position = new Vector2(gameQuitBtn.position.x, -100);

        gameName.DOMoveY(gameNameP, 1);
        gameStartBtn.DOMoveY(gameStartBtnP, 2f);
        gameSettingBtn.DOMoveY(gameSettingBtnP, 2f);
        gameQuitBtn.DOMoveY(gameQuitBtnP, 3);

        gameStartBtn.GetComponent<Button>().onClick.AddListener(OnClickMainToSet);
    }

    public void OnClickMainToSet()
    {
        mainScreenImg.DOMoveX(-960, 0.5f).SetEase(Ease.OutQuad);
        setScreenImg.DOMoveX(960, 0.5f).SetEase(Ease.OutQuad);
    }
    public void OnClickSetToMain()
    {
        mainScreenImg.DOMoveX(960, 0.5f).SetEase(Ease.OutCubic);
        setScreenImg.DOMoveX(960 + 1920, 0.5f).SetEase(Ease.OutCubic);
    }

    public void OnClickGameStart()
    {
        for (int i = 0; i < 5; i++)
        {
            if (GameManager.instance.deckUnits[i] == "")
                return;
        }
        SceneManager.LoadScene("GameScene");
    }
}
