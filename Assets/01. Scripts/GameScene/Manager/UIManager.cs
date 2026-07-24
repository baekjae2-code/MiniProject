using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public Slider manaSlider;
    public TextMeshProUGUI manaText;

    public GameObject gameOverPopUp;
    public TextMeshPro gameOverText;
    public TextMeshPro rewardText;

    public TextMeshProUGUI stageText;

    public GameObject unitHPBar;
    public Transform barParent;

    public TextMeshProUGUI timerText;
    float timer;

    public TextMeshProUGUI waveText;

    private void Start()
    {
        stageText.text = $"Stage {GameManager.instance.NowStage.ToString()}";
        timer = 0;
    }
    void LateUpdate()
    {
        float manaNow = BattleManager.instance.GetManaNow();
        float manaMax = BattleManager.instance.GetManaMax();
        manaSlider.value = manaNow / manaMax;
        manaText.text = $"{(int)manaNow}/{manaMax}";

        timer += Time.deltaTime;
        timerText.text = ((int)timer).ToString();
    }
    public void GameOverUI(string name)
    {
        int rewardGold = 0;
        if (gameOverPopUp != null)
        {
            gameOverPopUp.transform.position = Camera.main.transform.position + Vector3.right * 3 + Vector3.up * 5 + Vector3.forward * 10;
            gameOverPopUp.SetActive(true);
            if (name == "PlayerBase")
            {
                gameOverText.text = "Lose...";

            }
            else if (name == "EnemyBase")
            {
                SoundManager.instance.PlaySFX((SFXType)10);
                gameOverText.text = "Victory!!";
                rewardGold = (int)timer * 50 + Random.Range((int)timer / 5, (int)timer / 10);

                GameManager.instance.SetClearStage();
            }
        }
        BattleManager.instance.GameOver();
        rewardText.text = "+ " + rewardGold.ToString();
        GameManager.instance.RewardGold(rewardGold);

    }

    public void PrintUnitHPbar(GameObject parentObj)    //TeamSpawnManager, EnemySpaenManager
    {
        GameObject hpbar = Instantiate(unitHPBar);
        hpbar.GetComponent<UnitHPBar>().myUnit = parentObj;
        hpbar.transform.SetParent(barParent);
    }

    public void PrintWaveText(string text)  //EnemySpawnManager
    {
        waveText.text = text;
        StartCoroutine(WarningText());
    }

    IEnumerator WarningText()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        Color32 col = new Color32(255, 0, 0, 255);

        for (byte i = 0; i < 255; i++)
        {
            waveText.color = col;
            col.a--;
            yield return wait;
        }
    }
}
