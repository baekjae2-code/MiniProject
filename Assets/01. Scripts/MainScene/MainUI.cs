using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
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
    [SerializeField] Transform stageToSetBtn;

    float stageLeftBtnPosition;
    float stageRightBtnPosition;

    [SerializeField] GameObject warningImage;
    [SerializeField] GameObject warningText;
    [SerializeField] Image holdWarningImage;
    [SerializeField] TextMeshProUGUI holdWarningText;
    [SerializeField] Image deckSettingImage;

    [SerializeField] Volume volume;

    [SerializeField] Image[] stageBtns;

    void Start()
    {
        float gameNameP = gameName.localPosition.y;
        float gameStartBtnP = gameStartBtn.localPosition.y;
        float gameSettingBtnP = gameSettingBtn.localPosition.y;
        float gameQuitBtnP = gameQuitBtn.localPosition.y;

        gameName.localPosition = new Vector2(gameName.localPosition.x, 1500);
        gameStartBtn.localPosition = new Vector2(gameStartBtn.localPosition.x, -1000);
        gameSettingBtn.localPosition = new Vector2(gameSettingBtn.localPosition.x, -900);
        gameQuitBtn.localPosition = new Vector2(gameQuitBtn.localPosition.x, -800);

        gameName.DOLocalMoveY(gameNameP, 1);
        gameStartBtn.DOLocalMoveY(gameStartBtnP, 2f);
        gameSettingBtn.DOLocalMoveY(gameSettingBtnP, 2f);
        gameQuitBtn.DOLocalMoveY(gameQuitBtnP, 3);

        gameStartBtn.GetComponent<Button>().onClick.AddListener(OnClickMainToSet);

        stageLeftBtnPosition = stageLeftBtn.localPosition.x;
        stageRightBtnPosition = stageRightBtn.localPosition.x;

        StageUI();

        if (GameManager.instance.ClearStage >= 5)
        {
            stageBtns[4].name = (GameManager.instance.ClearStage + 1).ToString();
            stageBtns[4].GetComponentInChildren<TextMeshProUGUI>().text = $"Stage {(GameManager.instance.ClearStage + 1).ToString()}";
        }
    }

    public void OnClickMainToSet()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        screenImgManager.DOLocalMoveX(-1920, 0.5f).SetEase(Ease.OutQuad);    //0에서 왼쪽으로 -960만큼 이동

    }
    public void OnClickSetToMain()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        screenImgManager.DOLocalMoveX(0, 0.5f).SetEase(Ease.OutCubic);    //오른쪽으로 1920만큼 이동(0으로)

        infoImage.SetParent(settingScreenImg); //메인화면으로 갈때 자원 창 이동X
    }
    public void OnClickSetToStage()
    {
        for (int i = 0; i < 5; i++)
        {
            int num = 0;
            if (GameManager.instance.deckUnitNumber[i] == -1)   //덱이 꽉찼는지 체크
            {
                num++;
            }
            if (num > 0)
            {
                Warning();
                return;
            }
        }
        SoundManager.instance.PlaySFX((SFXType)7);
        screenImgManager.DOLocalMoveX(-1920 - 1920, 0.5f).SetEase(Ease.OutQuad);

        infoImage.SetParent(canvas);    //자원 창 -> 세팅+스테이지 창 둘다 보임

        stageLeftBtn.localPosition = new Vector2(stageLeftBtnPosition, stageLeftBtn.localPosition.y);
        stageRightBtn.localPosition = new Vector2(stageRightBtnPosition, stageRightBtn.localPosition.y);
    }

    public void OnClickStageToSet()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        screenImgManager.DOLocalMoveX(-1920, 0.5f).SetEase(Ease.OutQuad);

        stageLeftBtn.SetParent(stageScreenImg); //스테이지에서 세팅 창으로 이동할때 다시 안보이게
        stageRightBtn.SetParent(stageScreenImg);
    }

    public void OnClickStageRight() //화면 오른쪽이동
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        stageLeftBtn.SetParent(canvas); //스테이지 이동 버튼 -> 이동할때 제자리
        stageRightBtn.SetParent(canvas);

        if (screenImgManager.localPosition.x < -3840 - 1920 + 499)
        {
            screenImgManager.DOLocalMoveX(-3840 - 1920, 0.2f).SetEase(Ease.OutQuad);
            return;
        }

        screenImgManager.DOLocalMoveX(screenImgManager.localPosition.x - 500, 0.2f).SetEase(Ease.OutQuad);
    }
    public void OnClickStageLeft()  //화면 왼쪽이동
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        stageLeftBtn.SetParent(canvas);
        stageRightBtn.SetParent(canvas);

        if (screenImgManager.localPosition.x > -3840 - 499)
        {
            screenImgManager.DOLocalMoveX(-3840, 0.2f).SetEase(Ease.OutQuad);
            return;
        }

        screenImgManager.DOLocalMoveX(screenImgManager.localPosition.x + 500, 0.2f).SetEase(Ease.OutQuad);
    }
    public void OnClickGameStart()
    {
        for (int i = 0; i < 5; i++)
        {
            int num = 0;
            if (GameManager.instance.deckUnitNumber[i] == -1)   //덱이 꽉찼는지 체크
            {
                num++;
            }
            if (num > 0)
            {
                Warning();
                OnClickStageToSet();
                return;
            }
        }
        SoundManager.instance.PlaySFX((SFXType)7);
        if (GameManager.instance.ClearStage + 1 < int.Parse(EventSystem.current.currentSelectedGameObject.name))    //현재 클리어 다음 스테이지만 가능
        {
            return;
        }

        GameManager.instance.SetStage(int.Parse(EventSystem.current.currentSelectedGameObject.name));    //선택한 버튼의 이름을 스테이지로 세팅
        SoundManager.instance.ChangeBgm();
        SceneManager.LoadScene("GameScene");
    }
    public void StageUI()    //스테이지 버튼들 색깔이 클리어하였으면 바뀜, 초기화할떄 호출
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < GameManager.instance.ClearStage + 1)
            {
                stageBtns[i].color = Color.white;
                stageBtns[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
            }
            else
            {
                stageBtns[i].color = new Color(100 / 255f, 100 / 255f, 100 / 255f);
                stageBtns[i].transform.GetChild(0).GetComponent<Image>().color = Color.blue;
            }
        }
    }
    void Warning()
    {
        SoundManager.instance.PlaySFX((SFXType)8);
        GameObject warningi = Instantiate(warningImage, new Vector3(0, -5), Quaternion.identity, canvas);
        GameObject warningt = Instantiate(warningText, new Vector3(0, -5), Quaternion.identity, canvas);

        warningi.SetActive(true);
        warningt.SetActive(true);

        warningi.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(Random.Range(-3, 3), Random.Range(8, 15));
        warningt.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(Random.Range(-3, 3), Random.Range(8, 15));

        warningi.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-200f, 200f);
        warningt.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-200f, 200f);

        Destroy(warningi, 5f);
        Destroy(warningt, 5f);

        StartCoroutine(WarningCoroutine());
    }

    IEnumerator WarningCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        Color32 col = new Color32(255, 0, 0, 255);
        volume.weight = 0.5f;

        for (byte i = 0; i < 255; i++)
        {
            holdWarningImage.color = col;
            holdWarningText.color = col;
            deckSettingImage.color = new Color32(255, i, i, 255);
            volume.weight -= 0.01f;
            col.a--;
            yield return wait;
        }
    }
}
