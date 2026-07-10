using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    void LateUpdate()
    {
        float manaNow = BattleManager.instance.GetManaNow();
        float manaMax = BattleManager.instance.GetManaMax();
        manaSlider.value = manaNow / manaMax;
        manaText.text = $"{(int)manaNow}/{manaMax}";
    }

    public void GameOverUI(string name)
    {
        gameOverPopUp.transform.position = Camera.main.transform.position + Vector3.right * 3 + Vector3.up * 5 + Vector3.forward * 10;
        gameOverPopUp.SetActive(true);
        if(name == "PlayerBase")
        {
            gameOverText.text = "Lose...";
        }
        else if(name == "EnemyBase")
        {
            gameOverText.text = "Victory!!";
        }
        BattleManager.instance.GameOver();
    }
}
