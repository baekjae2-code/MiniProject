using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    float manaNow;
    float manaMax;

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
    }

    void Update()
    {
        manaNow += Time.deltaTime * 1.5f;
        if( manaNow > manaMax )
        {
            manaNow = manaMax;
        }
    }
    
    public void UseMana(float mana)
    {
        if(manaNow > mana)
        {
            manaNow -= mana;
        }
    }

    public void GameOver()
    {
        TeamSpawnManager.instance.GameOver();
        EnemySpawnManager.instance.GameOver();        
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
