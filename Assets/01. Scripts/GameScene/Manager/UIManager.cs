using TMPro;
using UnityEngine;
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

    public Slider teamHPBar;
    public Slider enemyHPBar;

    public Transform teamBase;
    public Transform enemyBase;

    private void Start()
    {
        stageText.text = $"Stage {GameManager.instance.NowStage.ToString()}";
    }
    void LateUpdate()
    {
        float manaNow = BattleManager.instance.GetManaNow();
        float manaMax = BattleManager.instance.GetManaMax();
        manaSlider.value = manaNow / manaMax;
        manaText.text = $"{(int)manaNow}/{manaMax}";

        if (teamBase != null)
        {
            teamHPBar.transform.position = Camera.main.WorldToScreenPoint(teamBase.position + Vector3.up * 3 + Vector3.right * 2.5f);
            teamHPBar.value = teamBase.GetComponent<Base>().nowHP / 500f;
        }
        if (enemyBase != null)
        {
            enemyHPBar.transform.position = Camera.main.WorldToScreenPoint(enemyBase.position + Vector3.up * 3 + Vector3.right * -2.5f);
            enemyHPBar.value = enemyBase.GetComponent<Base>().nowHP / 500f;
        }    
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
                gameOverText.text = "Victory!!";
                rewardGold = 200 + Random.Range(200 / 5, 200 / 10);

                GameManager.instance.SetClearStage();
            }
        }
        BattleManager.instance.GameOver();
        rewardText.text = "+ " + rewardGold.ToString();
        GameManager.instance.RewardGold(rewardGold);

    }
}
