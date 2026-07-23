using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TeamSpawnManager : MonoBehaviour
{
    public static TeamSpawnManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Spawn(0, 3);
    }

    public void Spawn(int spawnUnitNum, float spawnTime)
    {
        StartCoroutine(SpawnUnits(spawnUnitNum, spawnTime));
    }

    IEnumerator SpawnUnits(int spawnUnitNum, float spawnTime)
    {
        while (true)
        {
            //GameObject u = Instantiate(GameManager.instance.unitsPrefab[spawnUnitNum]);
            GameObject u = ObjectPoolManager.instance.GetObject(GameManager.instance.unitsPrefab[spawnUnitNum].name);
            u.GetComponent<Unit>().enabled = true;
            u.transform.position = transform.position;
            u.SetActive(true);
            u.GetComponent<Rigidbody2D>().linearVelocity += new Vector2(Random.Range(1f, 3f), Random.Range(1f, 3f));
            UIManager.instance.PrintUnitHPbar(u);

            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void GameOver()
    {
        StopAllCoroutines();
    }
}
