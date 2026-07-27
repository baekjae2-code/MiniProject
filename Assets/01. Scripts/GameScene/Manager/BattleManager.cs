using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    float manaNow;
    float manaMax;

    public GameObject UIPanel;
    Rigidbody2D[] UIPanelChildrens;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        manaMax = 100f;
        manaNow = 0f;

        UIPanelChildrens = UIPanel.transform.GetComponentsInChildren<Rigidbody2D>();
    }

    void Update()
    {
        manaNow += Time.deltaTime * 1.5f;
        if (manaNow > manaMax)
        {
            manaNow = manaMax;
        }
    }

    public void UseMana(float mana)
    {
        if (manaNow > mana)
        {
            manaNow -= mana;
        }
    }

    public void GameOver()  //UIManager
    {
        TeamSpawnManager.instance.GameOver();
        EnemySpawnManager.instance.GameOver();

        foreach (Rigidbody2D rb in UIPanelChildrens)
        {
            if (rb.GetComponent<RectMask2D>() != null)
                rb.GetComponent<RectMask2D>().enabled = false;
            rb.GetComponent<Collider2D>().enabled = true;
            rb.gravityScale = 50;
            rb.linearVelocity = new Vector2(Random.Range(100f, 1000f), Random.Range(500f, 2000f));
            rb.angularVelocity = Random.Range(-1000, 1000);
        }
    }

    public float GetManaNow()
    {
        return manaNow;
    }
    public float GetManaMax()
    {
        return manaMax;
    }
}
