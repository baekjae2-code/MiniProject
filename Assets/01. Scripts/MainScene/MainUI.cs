using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    public void MainToSetBtn()
    {
        mainScreenImg.DOMoveX(-960, 0.5f).SetEase(Ease.OutQuad);
        setScreenImg.DOMoveX(960, 0.5f).SetEase(Ease.OutQuad);
    }
    public void SetToMainBtn()
    {
        mainScreenImg.DOMoveX(960, 0.5f).SetEase(Ease.OutCubic);
        setScreenImg.DOMoveX(960 + 1920, 0.5f).SetEase(Ease.OutCubic);
    }

    public void GameStartBtn()
    {
        SceneManager.LoadScene("GameScene");    
    }
}
