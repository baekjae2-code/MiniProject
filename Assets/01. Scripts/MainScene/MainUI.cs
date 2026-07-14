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

    [SerializeField] Transform screenImgManager;

    [SerializeField] Transform canvas;
    [SerializeField] Transform settingScreenImg;
    [SerializeField] Transform stageScreenImg;

    [SerializeField] Transform infoImage;
    [SerializeField] Transform stageLeftBtn;
    [SerializeField] Transform stageRightBtn;
    [SerializeField] Transform stageToSettinhBtn;

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
        screenImgManager.DOMoveX(-960, 0.5f).SetEase(Ease.OutQuad);    //0에서 왼쪽으로 -960만큼 이동

    }
    public void OnClickSetToMain()
    {
        screenImgManager.DOMoveX(960, 0.5f).SetEase(Ease.OutCubic);    //오른쪽으로 1920만큼 이동(0으로)

        infoImage.SetParent(settingScreenImg); //메인화면으로 갈때 자원 창 이동X
    }
    public void OnClickSetToStage()
    {
        screenImgManager.DOMoveX(-1920 - 960, 0.5f).SetEase(Ease.OutQuad);

        infoImage.SetParent(canvas);    //자원 창 -> 세팅+스테이지 창 둘다 보임
    }

    public void OnClickStageToSet()
    {
        screenImgManager.DOMoveX(-960, 0.5f).SetEase(Ease.OutQuad);

        stageLeftBtn.SetParent(stageScreenImg); //스테이지에서 세팅 창으로 이동할때 다시 안보이게
        stageRightBtn.SetParent(stageScreenImg);
    }
    public void OnClickStageRight()
    {
        stageLeftBtn.SetParent(canvas); //스테이지 이동 버튼 -> 이동할때 제자리
        stageRightBtn.SetParent(canvas);

        if (screenImgManager.position.x < -5760 + 960 + 199)
        {
            screenImgManager.DOMoveX(-5760 + 960, 0.5f).SetEase(Ease.OutQuad);
            return;
        }

        screenImgManager.DOMoveX(screenImgManager.position.x - 200, 0.5f).SetEase(Ease.OutQuad);
    }
    public void OnClickStageLeft()
    {
        stageLeftBtn.SetParent(canvas);
        stageRightBtn.SetParent(canvas);

        if (screenImgManager.position.x > -1920 - 960 - 199)
        {
            screenImgManager.DOMoveX(-1920 - 960, 0.5f).SetEase(Ease.OutQuad);
            return;
        }

        screenImgManager.DOMoveX(screenImgManager.position.x + 200, 0.5f).SetEase(Ease.OutQuad);
    }
    public void OnClickGameStart()
    {
        for (int i = 0; i < 5; i++)
        {
            if (GameManager.instance.deckUnitNumber[i] == -1)   //덱이 꽉찼는지 체크
                return;
        }
        SceneManager.LoadScene("GameScene");
    }
}
